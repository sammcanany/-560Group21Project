namespace Group21ProjectMVC.Models
{
    public class ApplicationTicket
    {
        public ApplicationUser ProfileID { get; set; }

        public ApplicationFlight FlightID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SeatNumber { get; set; }
    }
}
