namespace Group21ProjectMVC.Models.CheckoutViewModels
{
    public class SeatSelectionViewModel
    {
        public IList<ApplicationFlight> Flights { get; set; } = new List<ApplicationFlight>();

        public int DepartureFlightId { get; set; }

        public int ReturnFlightId { get; set; }

        public List<int> DepartureFlightSeats { get; set; }

        public List<int> ReturnFlightSeats { get; set; }

        public int SeatsRequired { get; set; }

        public List<string> FirstNames { get; set; }

        public List<string> LastNames { get; set; }
    }
}
