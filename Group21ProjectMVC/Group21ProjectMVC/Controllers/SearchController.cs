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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Flight(FlightSearchModel fsm)
        {
            fsm.DepartureFlights = GetFlights(fsm.FromLocation, fsm.ToLocation, fsm.SeatsRequired, fsm.DepartureDate);
            if (fsm.ReturnDate.HasValue)
            {
                fsm.ReturnFlights = GetFlights(fsm.ToLocation, fsm.FromLocation, fsm.SeatsRequired, fsm.ReturnDate);
            }
            return View(fsm);
        }

        public IEnumerable<FlightViewModel> GetFlights(string FromLocation, string ToLocation, int SeatsRequired, DateTime? DepartureDate)
        {
            List<FlightViewModel> flights = new();
            if (ModelState.IsValid)
            {
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("DefaultConnection"));
                sqlConnection.Open();
                SqlCommand sqlCmd = new("FlightSearch", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("FromLocation", FromLocation);
                sqlCmd.Parameters.AddWithValue("ToLocation", ToLocation);
                sqlCmd.Parameters.AddWithValue("SeatsRequired", SeatsRequired);
                sqlCmd.Parameters.AddWithValue("DepartureDate", DepartureDate);
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        flights.Add(new FlightViewModel
                        {
                            FlightId = Convert.ToInt32(reader["FlightID"]),
                            Airline = reader["Airline"].ToString(),
                            DepartureTime = Convert.ToDateTime(reader["DepartureTime"].ToString()),
                            ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"].ToString()),
                            Price = Convert.ToInt32(reader["Price"])
                        });
                    }
                }
                sqlConnection.Close();
            }
            return flights;
        }
    }
}
