using BengoBoxAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BengoBoxAuth.Data
{
    public class UserData : IdentityDbContext
    {
        public UserData(DbContextOptions opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }
        public DbSet<userPasswords> userPasswords { get; set; }
        public DbSet<UserRoleScreens> UserRoleScreens { get; set; }
        public DbSet<AuditLogs> AuditLogs { get; set; }
        public DbSet<PasswordPolicy> PasswordPolicy { get; set; }
    }
}