using System.ComponentModel.DataAnnotations;

namespace BengoBoxAuth.Models.Requests
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Email is required")]

        [EmailAddress]
        public String Email { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        public String CurrentPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public String NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        public String ConfirmPassword { get; set; }
    }
}
