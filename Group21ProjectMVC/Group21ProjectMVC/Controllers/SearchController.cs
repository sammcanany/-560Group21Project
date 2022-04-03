using Group21ProjectMVC.Models;
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

        public SearchController(ILogger<SearchController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Flight(string FromLocation,string ToLocation, int SeatsRequired, string DepartureDate, string ReturnDate)
        {
            FlightSearchModel flightSearchModel = new FlightSearchModel
            {
                FromLocation = FromLocation,
                ToLocation = ToLocation,
                SeatsRequired = SeatsRequired,
                DepartureDate = DateTime.Parse(DepartureDate),
                ReturnDate = DateTime.Parse(ReturnDate)
            };

            return View(flightSearchModel);
        }
    }
}
