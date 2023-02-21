using System.ComponentModel.DataAnnotations;

namespace BengoBoxAuth.Models.Requests
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email is required")]

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public String NewPassword { get; set; }
    }
}
