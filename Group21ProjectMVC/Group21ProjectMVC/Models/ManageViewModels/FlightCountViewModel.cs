using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Group21ProjectMVC.Models.ManageViewModels
{
    public class FlightCountViewModel
    {
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; } = DateTime.Today;

        public DataSet ds { get; set; }
    }
}
