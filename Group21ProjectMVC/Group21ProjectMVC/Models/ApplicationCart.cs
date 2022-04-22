using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.Models
{
    public class ApplicationCart
    {
        [Key]
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }
    }
}
