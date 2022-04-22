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
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Group21ProjectMVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFlightStore<ApplicationFlight> _flightStore;

        private CancellationTokenSource source = new CancellationTokenSource();

        public SearchController(ILogger<SearchController> logger, IConfiguration Configuration, IFlightStore<ApplicationFlight> flightStore)
        {
            _logger = logger;
            _configuration = Configuration;
            _flightStore = flightStore;
        }

        [HttpGet]
        public async Task<IActionResult> Flight(FlightSearchViewModel fsm)
        {
            if (ModelState.IsValid)
            {
                fsm.DepartureFlights = await _flightStore.GetFlightsAsync(fsm.FromLocation, fsm.ToLocation, fsm.SeatsRequired, fsm.DepartureDate, source.Token);
                if (fsm.TabName == "roundtrip")
                {
                    fsm.ReturnFlights = await _flightStore.GetFlightsAsync(fsm.ToLocation, fsm.FromLocation, fsm.SeatsRequired, fsm.ReturnDate, source.Token);
                }
                return View(fsm);
            }
            return View("../Home/Index", fsm);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Checkout(int DepartureFlightID, int ReturnFlightID, int SeatsRequired)
        {
            IList<ApplicationFlight> flights = new List<ApplicationFlight>();
            var departureFlights = await _flightStore.GetFlightByIdAsync(DepartureFlightID, source.Token);
            departureFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(DepartureFlightID, source.Token);
            flights.Add(departureFlights);
            if (ReturnFlightID != 0)
            {
                var returnFlights = await _flightStore.GetFlightByIdAsync(ReturnFlightID, source.Token);
                returnFlights.SeatsNotAvailable = await _flightStore.GetSeatsAvailableByFlightIdAsync(ReturnFlightID, source.Token);
                flights.Add(returnFlights);
            }
            return View(new CheckoutViewModel
            {
                DepartureFlightID = DepartureFlightID,
                ReturnFlightID = ReturnFlightID,
                SeatsRequired = SeatsRequired,
                Flights = flights
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CheckOut(CheckoutViewModel cvm)
        {
            
            return View(cvm);
        }
    }
}
