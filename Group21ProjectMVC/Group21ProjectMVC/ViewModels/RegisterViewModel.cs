using System.ComponentModel.DataAnnotations;

namespace Group21ProjectMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 7 and 255 characters", MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
