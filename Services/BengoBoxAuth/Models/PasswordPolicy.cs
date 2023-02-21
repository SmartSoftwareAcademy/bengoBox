using System.ComponentModel.DataAnnotations;

namespace BengoBoxAuth.Models
{
    public class PasswordPolicy
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public bool lowercase { get; set; }
        [Required]
        public bool uppercase { get; set; }
        [Required]
        public bool special_char { get; set; }
        [Required]
        public int password_length { get; set; }
        [Required]
        public int expiry { get; set; }
    }
}
