using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
