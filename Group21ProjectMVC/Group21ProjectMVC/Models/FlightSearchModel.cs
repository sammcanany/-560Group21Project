using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group21ProjectMVC.Models
{
    public class FlightSearchModel
    {
        public IEnumerable<FlightViewModel> DepartureFlights { get; set; }
        public IEnumerable<FlightViewModel> ReturnFlights { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Destination location is required!")]
        public string ToLocation { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "Departure location is required!")]
        public string FromLocation { get; set; } = "";

        [BindProperty]
        public int SeatsRequired { get; set; } = 0;

        [BindProperty]
        [Required(ErrorMessage = "Departure date is required!")]
        public DateTime DepartureDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Departure date is required!")]
        public DateTime? ReturnDate { get; set; } = null;

        public int DepartureFlightID { get; set; } = 0;
    }
}
