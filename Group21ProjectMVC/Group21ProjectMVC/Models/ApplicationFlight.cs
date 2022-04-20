namespace Group21ProjectMVC.Models
{
    public class ApplicationFlight
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; }

        public string DepartingAirportCode { get; set; }

        public string DestinationAirportCode { get; set; }

        public string Airline { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int Capacity { get; set; }

        public int SeatsTaken { get; set; }

        public decimal Price { get; set; }
    }
}
