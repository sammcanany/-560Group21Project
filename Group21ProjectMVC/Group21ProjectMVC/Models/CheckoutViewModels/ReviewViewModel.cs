
namespace Group21ProjectMVC.Models.CheckoutViewModels
{
    public class ReviewViewModel
    {
        public IList<ApplicationFlight> Flights { get; set; } = new List<ApplicationFlight>();

        public int DepartureFlightId { get; set; }

        public int ReturnFlightId { get; set; }

        public int SeatsRequired { get; set; }
    }
}
