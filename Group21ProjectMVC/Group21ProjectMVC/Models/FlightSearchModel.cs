using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group21ProjectMVC.Models
{
    public class FlightSearchModel:PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Airline { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string ToLocation { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string FromLocation { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public int SeatsRequired { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public DateTime DepartureDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime ReturnDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime DepartureTime { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime ArrivalTime { get; set; }

        [BindProperty(SupportsGet = true)]
        public Decimal Price { get; set; }
    }
}
