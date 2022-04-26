using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Group21ProjectMVC.Models.ManageViewModels
{
    public class AddFlightsViewModel
    {
        [DataType(DataType.Date)]
        public DateTime StartingDate {  get; set; } = DateTime.Today;

        [DataType(DataType.Date)]
        public DateTime EndingDate { get; set; } = DateTime.Today.AddDays(5);

        public string Airline { get; set; }

        public IEnumerable<SelectListItem> Times { get; set; }

        public IEnumerable<SelectListItem> Airports { get; set; }

        public List<string> Airlines { get; set; }

        public int[] TimesIdSelected { get; set; }

        public IEnumerable<DateTime> TimesSelected { get; set; }

        public int[] AirportsIdSelected { get; set; }

        public IEnumerable<string> AirportsSelected { get; set; }

        public string StatusMessage { get; set; }
    }
}
