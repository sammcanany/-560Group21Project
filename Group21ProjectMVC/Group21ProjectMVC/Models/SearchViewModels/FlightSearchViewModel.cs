using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group21ProjectMVC.Models.FlightViewModels
{
    public class FlightSearchViewModel : IValidatableObject
    {
        public IEnumerable<ApplicationFlight> DepartureFlights { get; set; }

        public IEnumerable<ApplicationFlight> ReturnFlights { get; set; }

        [BindProperty]
        public string TabName { get; set; }

        [DisplayName("Destination Location")]
        [Required(ErrorMessage = "Destination location is required!")]
        [Unlike(nameof(FromLocation), "Departure Location")]
        public string ToLocation { get; set; }

        [DisplayName("Departure Location")]
        [Required(ErrorMessage = "Departure location is required!")]
        public string FromLocation { get; set; }

        public int SeatsRequired { get; set; } = 0;

        [DisplayName("Departure date")]
        [Required(ErrorMessage = "Departure date is required!")]
        public DateTime DepartureDate { get; set; }

        [DisplayName("Return date")]
        [Unlike(nameof(DepartureDate),"Departure date")]
        public DateTime? ReturnDate { get; set; } = null;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TabName == "roundtrip" && ReturnDate == null)
            {
                yield return new ValidationResult("Return date is required!", new[] { "ReturnDate" });
            }
        }
    }
}
