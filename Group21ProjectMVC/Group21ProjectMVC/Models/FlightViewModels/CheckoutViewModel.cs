using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.Models.FlightViewModels
{
    public class CheckoutViewModel
    {
        public int DepartureFlightID { get; set; }

        public int ReturnFlightID { get; set; } = 0;

        public int SeatsRequired { get; set; }

        public IEnumerable<ApplicationFlight> Flights { get; set; }

        public IEnumerable<string> FirstNames { get; set; }

        public IEnumerable<string> LastNames { get; set; }

        [CreditCard]
        public string CardNumber { get; set; }
        
        public string CCFullName { get; set; }

        public string CCExpiration { get; set; }

        public string CCCVV { get; set; }

        public string BillingAddress { get; set; }

        public string BillingCity { get; set; }

        public string BillingState { get; set; }

        public string BillingZip { get; set; }

    }
}
