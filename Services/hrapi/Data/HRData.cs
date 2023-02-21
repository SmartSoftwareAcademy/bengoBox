using hrapi.Models.Employee;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace hrapi.Data
{
    public class HRData:DbContext
    {
        public HRData(DbContextOptions options) : base(options) {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Entity<Employee>()
           //.HasOne(b => b.HRDetails)
           //.WithOne(i => i.Employee)
           //.HasForeignKey<HRDetails>(b => b.EmployeeForeignKey).OnDelete(DeleteBehavior.ClientCascade);
           // modelBuilder.Entity<Employee>()
           //.HasOne(b => b.SalaryDetails)
           //.WithOne(i => i.Employee)
           //.HasForeignKey<SalaryDetails>(b => b.EmployeeForeignKey).OnDelete(DeleteBehavior.ClientCascade);
           // modelBuilder.Entity<Employee>()
           //.HasOne(b => b.ContactDetails)
           //.WithOne(i => i.Employee)
           //.HasForeignKey<ContactDetails>(b => b.EmployeeForeignKey).OnDelete(DeleteBehavior.ClientCascade);
          //  modelBuilder.Entity<WorkShift>()
          // .HasOne(b => b.ShiftRotation)
          // .WithOne(i => i.workShifts)
          // .HasForeignKey<ShiftRotation>(b => b.WorkShiftId).OnDelete(DeleteBehavior.ClientCascade);
          //  modelBuilder.Entity<WorkShift>()
          //.HasOne(b => b.SalaryDetails)
          //.WithOne(i => i.WorkShift)
          //.HasForeignKey<SalaryDetails>(b => b.WorkShiftId).OnDelete(DeleteBehavior.ClientCascade);

        }
        //employee data
        public DbSet<Benefits> Benefits { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Deductions> Deductions { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Earnings> Earnings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
        public DbSet<HRDetails> HRDetails { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<ParentRegions> ParentRegions { get; set; }
        public DbSet<ParentDepartments> ParentDepartments { get; set; }
        public DbSet<PaymentData> PaymentData { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<SalaryDetails> SalaryDetails { get; set; }
        public DbSet<ShiftRotation> ShiftRotations { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }

    }
}
