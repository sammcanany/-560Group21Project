using Group21ProjectMVC.Models;
using Group21ProjectMVC.Models.FlightViewModels;
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

namespace Group21ProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        List<string> airports = new List<string> { "ATL - Hartsfield-Jackson International Airport, GA", "DFW - Dallas/Fort Worth International Airport, TX", "DEN - Denver International Airport, CO", "ORD - O'Hare International Airport, IL", "LAX - Los Angeles International Airport, CA", "CLT - Charlotte Douglas International Airport, NC", "LAS - Harry Reid International Airport, NV", "PHX - Phoenix Sky Harbor International Airport, AZ", "MCO - Orlando International Airport, FL", "MHK - Manhattan Regional Airport, KS" };
        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}