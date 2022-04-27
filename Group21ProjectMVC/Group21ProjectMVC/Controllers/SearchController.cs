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

namespace Group21ProjectMVC.Controllers
{
    public class SearchController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IFlightStore<ApplicationFlight> _flightStore;
        private readonly ITicketStore<ApplicationTicket> _ticketStore;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly CancellationTokenSource source = new();

        public SearchController(ILogger<SearchController> logger, IConfiguration Configuration, IFlightStore<ApplicationFlight> flightStore, ITicketStore<ApplicationTicket> ticketStore, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _configuration = Configuration;
            _flightStore = flightStore;
            _ticketStore = ticketStore;
            _userManager = userManager;
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
        public async Task<IActionResult> TicketsByUserId(TicketSearchViewModel tsm)
        {
            if (ModelState.IsValid)
            {
                tsm.Tickets = await _ticketStore.GetTicketsByUserIdAsync(_userManager.GetUserAsync(User).Result.Id, source.Token);
                foreach(var t in tsm.Tickets)
                {
                    t.Flight = await _flightStore.GetFlightByIdAsync(t.FlightID, source.Token);
                    t.Profile = _userManager.GetUserAsync(User).Result;
                }
                return View(tsm);
            }
            return View("../Home/Index", tsm);
        }
    }
}
