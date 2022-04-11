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

        public IActionResult Flight(string FromLocation, string ToLocation, int SeatsRequired, string DepartureDate)
        {
            //IEnumerable<FlightViewModel> departureFlights = GetFlights(FromLocation, ToLocation, SeatsRequired, DepartureDate);
            //IEnumerable<FlightViewModel> returnFlights = GetFlights(ToLocation, FromLocation, SeatsRequired, ReturnDate);
            FlightSearchModel flightSearchModel = new FlightSearchModel
            {
                //DepartureFlights = departureFlights,
                //ReturnFlights = returnFlights,
                FromLocation = FromLocation,
                ToLocation = ToLocation,
                SeatsRequired = SeatsRequired,
                DepartureDate = DateTime.Parse(DepartureDate)
            };

            return View(flightSearchModel);
        }

        public IActionResult Flight(string FromLocation,string ToLocation, int SeatsRequired, string DepartureDate, string ReturnDate)
        {
            //IEnumerable<FlightViewModel> departureFlights = GetFlights(FromLocation, ToLocation, SeatsRequired, DepartureDate);
            //IEnumerable<FlightViewModel> returnFlights = GetFlights(ToLocation, FromLocation, SeatsRequired, ReturnDate);
                FlightSearchModel flightSearchModel = new FlightSearchModel
                {
                    //DepartureFlights = departureFlights,
                    //ReturnFlights = returnFlights,
                    FromLocation = FromLocation,
                    ToLocation = ToLocation,
                    SeatsRequired = SeatsRequired,
                    DepartureDate = DateTime.Parse(DepartureDate),
                    ReturnDate = DateTime.Parse(ReturnDate)
                };

                return View(flightSearchModel);
        }

        public IEnumerable<FlightViewModel> GetFlights(string FromLocation, string ToLocation, int SeatsRequired, string DepartureDate)
        {
            List<FlightViewModel> flights = new List<FlightViewModel>();
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("FlightSearch", sqlConnection);
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
                                DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString()),
                                ArrivalTime = DateTime.Parse(reader["ArrivalTime"].ToString()),
                                Price = Convert.ToInt32(reader["Price"])
                            });
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return flights;
        }
    }
}
