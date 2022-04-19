namespace Group21ProjectMVC.Models
{
    public class FlightViewModel
    {
        public int FlightId { get; set; }

        public string Airline { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Decimal Price { get; set; }
    }
}
