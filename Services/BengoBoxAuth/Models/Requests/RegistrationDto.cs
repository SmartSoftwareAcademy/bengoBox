using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BengoBoxAuth.Models.Requests
{
    public class RegistrationDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
