using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BengoBoxAuth.Models
{
    public class AuditLogs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string ComputerIP { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
