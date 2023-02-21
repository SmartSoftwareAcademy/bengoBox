using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BengoBoxAuth.Models
{
    public class userPasswords
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string password { get; set; }
        public DateTime lastUpdated { get; set; }
    }
}
