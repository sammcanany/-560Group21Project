using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.Models.CheckoutViewModels
{
    public class SeatSelectionViewModel
    {
        public IList<ApplicationFlight> Flights { get; set; } = new List<ApplicationFlight>();

        public int DepartureFlightId { get; set; }

        public int ReturnFlightId { get; set; }

        public bool checkif { get; set; }
        public bool alwaysTrue { get; set; } = true;

        [Display(Name = nameof(DepartureFlightSeats))]
        [EnsureMinimumElements(nameof(SeatsRequired), nameof(alwaysTrue), ErrorMessage = "Please select seats for all passengers on the departure flight")]
        public List<int> DepartureFlightSeats { get; set; }

        [Display(Name = nameof(ReturnFlightSeats))]
        [EnsureMinimumElements(nameof(SeatsRequired), nameof(checkif), ErrorMessage = "Please select seats for all passengers on the return flight")]
        public List<int> ReturnFlightSeats { get; set; }

        public int SeatsRequired { get; set; }

        public List<string> FirstNames { get; set; }

        public List<string> LastNames { get; set; }
    }
}
