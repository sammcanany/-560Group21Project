namespace Group21ProjectMVC.Models.CheckoutViewModels
{
    public class PassengerDetailsViewModel
    {
        public int DepartureFlightId { get; set; }

        public int ReturnFlightId { get; set; }

        public int SeatsRequired { get; set; }

        public List<string> FirstNames { get; set; }

        public List<string> LastNames { get; set; }
    }
}
