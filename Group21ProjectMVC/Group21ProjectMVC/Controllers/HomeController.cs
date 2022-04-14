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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        private List<FlightSearchModel> GetFlights()
        {
            string constr = _configuration.GetConnectionString("DefaultConnection");
            List<FlightSearchModel> flights = new List<FlightSearchModel>();
            using (SqlConnection con = new SqlConnection(constr))
            {
                //FileInfo file = new FileInfo(Server.MapPath("App_Datageneratescript.sql"));
                //string query = file.OpenText().ReadToEnd();
                //string prequery = $"DECLARE @FromLocation AS NVARCHAR(30) = '{model.FromLocation}';\nDECLARE @ToLocation AS NVARCHAR(30) = '{model.ToLocation}';\nDECLARE @SeatsRequired AS INT = {model.SeatsRequired};\nDECLARE @DepartureDate AS DATE = '{model.DepartureDate}';\nDECLARE @ReturnDate AS DATE = '{model.ReturnDate}';\n";
                string query = "SELECT [ToLocation],[FromLocation] FROM Flight";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            flights.Add(new FlightSearchModel
                            {
                                //Airline = sdr["Airline"].ToString(),
                                ToLocation = sdr["ToLocation"].ToString(),
                                FromLocation = sdr["FromLocation"].ToString(),
                                //DepartureTime = DateTime.Parse(sdr["DepartureTime"].ToString()),
                                //ArrivalTime = DateTime.Parse(sdr["ArrivalTime"].ToString()),
                                //Price = Convert.ToDecimal(sdr["Price"])

                            });
                        }
                    }
                    con.Close();
                }
            }
            return flights;
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