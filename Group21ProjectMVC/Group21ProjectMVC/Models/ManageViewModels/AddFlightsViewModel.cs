using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Group21ProjectMVC.Models.ManageViewModels
{
    public class AddFlightsViewModel
    {
        public DateTime StartingDate {  get; set; }
        public DateTime EndingDate { get; set; }

        public string Airline { get; set; }
        public List<DateTime> Times { get; set; }
        public List<string> Airports { get; set; }
        public List<DateTime> TimesSelected { get; set; }
        public List<string> AirportsSelected { get; set; }
    }
}
