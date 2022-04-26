namespace Group21ProjectMVC.Models
{
    public class ApplicationCart
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public IList<ApplicationFlight> Flights { get; set; }

        public IList<ApplicationTicket> Tickets { get; set; }

        public List<string> FirstNames { get; set; }

        public List<string> LastNames { get; set; }

        public List<int> Seats { get; set; }
    }
}
