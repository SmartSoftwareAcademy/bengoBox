using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BengoBoxAuth.Models
{
    public class UserRoleScreens 
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string UserRoleId { get; set; }
        public IdentityRole UserRole { get; set; }
        public string Screens { get; set; }
        public bool deleted { get; set; }
    }
}
