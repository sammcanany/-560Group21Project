using Group21ProjectMVC.Models;
using Group21ProjectMVC.Models.FlightViewModels;
using Group21ProjectMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Group21ProjectMVC.Models.CheckoutViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Group21ProjectMVC.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        private readonly ILogger<SearchController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFlightStore<ApplicationFlight> _flightStore;
        private readonly ITicketStore<ApplicationTicket> _ticketStore;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly CancellationTokenSource source = new();

        public CheckoutController(
            ILogger<SearchController> logger,
            IConfiguration Configuration,
            IFlightStore<ApplicationFlight> flightStore,
            ITicketStore<ApplicationTicket> ticketStore,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _configuration = Configuration;
            _flightStore = flightStore;
            _ticketStore = ticketStore;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Review(int DepartureFlightID, int ReturnFlightID, int SeatsRequired)
        {
            IList<ApplicationFlight> flights = new List<ApplicationFlight>();
            ApplicationFlight departureFlights = await _flightStore.GetFlightByIdAsync(DepartureFlightID, source.Token);
            departureFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(DepartureFlightID, source.Token);
            flights.Add(departureFlights);
            if (ReturnFlightID != 0)
            {
                var returnFlights = await _flightStore.GetFlightByIdAsync(ReturnFlightID, source.Token);
                returnFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(ReturnFlightID, source.Token);
                flights.Add(returnFlights);
            }
            return View(new ReviewViewModel
            {
                Flights = flights,
                SeatsRequired = SeatsRequired,
                DepartureFlightId = DepartureFlightID,
                ReturnFlightId = ReturnFlightID,
            });
        }

        [HttpGet]
        public IActionResult PassengerDetails(int DepartureFlightID, int ReturnFlightID, int SeatsRequired)
        {
            return View(new PassengerDetailsViewModel
            {
                SeatsRequired = SeatsRequired,
                DepartureFlightId = DepartureFlightID,
                ReturnFlightId = ReturnFlightID
            });
        }

        [HttpPost]
        public IActionResult PassengerDetails(PassengerDetailsViewModel pdvm)
        {
            if (!ModelState.IsValid)
            {
                return View(pdvm);
            }
            StringBuilder sb = new(Request.QueryString.ToString());
            foreach (var name in pdvm.FirstNames)
            {
                sb.Append("&FirstNames=" + name);
            }
            foreach (var name in pdvm.LastNames)
            {
                sb.Append("&LastNames=" + name);
            }
            return Redirect(Url.Action("SeatSelection") + sb.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> SeatSelection(int DepartureFlightID, int ReturnFlightID, int SeatsRequired, List<string> FirstNames, List<string> LastNames)
        {
            IList<ApplicationFlight> flights = new List<ApplicationFlight>();
            ApplicationFlight departureFlights = await _flightStore.GetFlightByIdAsync(DepartureFlightID, source.Token);
            departureFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(DepartureFlightID, source.Token);
            flights.Add(departureFlights);
            if (ReturnFlightID != 0)
            {
                var returnFlights = await _flightStore.GetFlightByIdAsync(ReturnFlightID, source.Token);
                returnFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(ReturnFlightID, source.Token);
                flights.Add(returnFlights);
            }
            return View(new SeatSelectionViewModel
            {
                Flights = flights,
                SeatsRequired = SeatsRequired,
                DepartureFlightId = DepartureFlightID,
                ReturnFlightId = ReturnFlightID,
                checkif = ReturnFlightID != 0,
                FirstNames = FirstNames,
                LastNames = LastNames
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SeatSelection(SeatSelectionViewModel ssvm)
        {
            if (!ModelState.IsValid)
            {
                //return Redirect(Url.Action("SeatSelection") + Request.QueryString.ToString());
                IList<ApplicationFlight> flights = new List<ApplicationFlight>();
                ApplicationFlight departureFlights = await _flightStore.GetFlightByIdAsync(ssvm.DepartureFlightId, source.Token);
                departureFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(ssvm.DepartureFlightId, source.Token);
                flights.Add(departureFlights);
                if (ssvm.ReturnFlightId != 0)
                {
                    var returnFlights = await _flightStore.GetFlightByIdAsync(ssvm.ReturnFlightId, source.Token);
                    returnFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(ssvm.ReturnFlightId, source.Token);
                    flights.Add(returnFlights);
                }
                ssvm.Flights = flights;
                return View(ssvm);
            }
            StringBuilder sb = new(Request.QueryString.ToString());
            foreach (var seat in ssvm.DepartureFlightSeats)
            {
                sb.Append("&DepartureFlightSeats=" + seat);
            }
            if (ssvm.ReturnFlightId != 0)
            {
                foreach (var seat in ssvm.ReturnFlightSeats)
                {
                    sb.Append("&ReturnFlightSeats=" + seat);
                }
            }
            return Redirect(Url.Action("Checkout") + sb.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(List<int> DepartureFlightSeats, List<int> ReturnFlightSeats, List<string> FirstNames, List<string> LastNames, int DepartureFlightID, int ReturnFlightID, int SeatsRequired)
        {
            List<ApplicationTicket> tickets = new();
            ApplicationFlight departureFlights = await _flightStore.GetFlightByIdAsync(DepartureFlightID, source.Token);
            ApplicationFlight returnFlights;
            bool AreReturnFlights = false;
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            departureFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(DepartureFlightID, source.Token);
            for (int i = 0; i < SeatsRequired; i++)
            {
                tickets.Add(new ApplicationTicket
                {
                    ProfileID = user.Id,
                    FlightID = departureFlights.Id,
                    Profile = user,
                    Flight = departureFlights,
                    FirstName = FirstNames[i],
                    LastName = LastNames[i],
                    SeatNumber = DepartureFlightSeats[i]
                });
            }
            if (ReturnFlightID != 0)
            {
                returnFlights = await _flightStore.GetFlightByIdAsync(ReturnFlightID, source.Token);
                returnFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(ReturnFlightID, source.Token);
                AreReturnFlights = true;
                for (int i = 0; i < SeatsRequired; i++)
                {
                    tickets.Add(new ApplicationTicket
                    {
                        ProfileID = user.Id,
                        FlightID = returnFlights.Id,
                        Profile = user,
                        Flight = returnFlights,
                        FirstName = FirstNames[i],
                        LastName = LastNames[i],
                        SeatNumber = ReturnFlightSeats[i]
                    });
                }
            }
            return View(new CheckoutViewModel
            {
                Tickets = tickets,
                ReturnFlights = AreReturnFlights
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(CheckoutViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            var response = "";
            foreach (var ticket in cvm.Tickets)
            {
                response = await _flightStore.SetSeatsTakenByFlightIdAsync(ticket.FlightID, source.Token);
                if (response != "success")
                {
                    throw new ApplicationException($"Unable to add tickets.");
                }
            }
            response = await _ticketStore.AddTicketsAsync(cvm.Tickets, source.Token);
            if (response != "success")
            {
                throw new ApplicationException($"Unable to add tickets.");
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
