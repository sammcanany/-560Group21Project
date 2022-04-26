namespace Group21ProjectMVC.Models
{
    public class ApplicationTicket
    {
        public int ProfileID { get; set; }

        public int FlightID { get; set; }

        public ApplicationUser Profile { get; set; }

        public ApplicationFlight Flight { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SeatNumber { get; set; }
    }
}
