using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.Models.CheckoutViewModels
{
    public class CheckoutViewModel
    {
        [BindProperty]
        public List<ApplicationTicket> Tickets { get; set; }

        public bool ReturnFlights { get; set; }

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
