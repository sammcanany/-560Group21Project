using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.Models.CheckoutViewModels
{
    public class PassengerDetailsViewModel
    {
        public int DepartureFlightId { get; set; }

        public int ReturnFlightId { get; set; }

        public int SeatsRequired { get; set; } = 1;

        [Display(Name =nameof(FirstNames))]
        [EnsureMinimumElements(nameof(SeatsRequired), ErrorMessage = "Please enter first names for all passengers")]
        public List<string> FirstNames { get; set; } = new List<string>();

        [Display(Name = nameof(LastNames))]
        [EnsureMinimumElements(nameof(SeatsRequired), ErrorMessage = "Please enter last names for all passengers")]
        public List<string> LastNames { get; set; } = new List<string>();
    }
}
