using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace hrapi.Models.Employee
{
    //personal details
    public class EmployeeDetails
    {
        public Employee Employee { get; set; }
        public SalaryDetails SalaryDetails { get; set; }
    }
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string Gender { get; set; }
        [Required, StringLength(50)]
        public string DoB { get; set; }
        public string ResidentialDetails { get; set; }
        [Required, StringLength(10)]
        public string Nationa_ID { get; set; }
        [Required, StringLength(20)]
        public string Pin_NO { get; set; }
        [StringLength(20)]
        public string? NHIF_NO { get; set; }
        [StringLength(20)]
        public string? NSSF_NO { get; set; }
        public int? WorkShiftId { get; set; }
        public WorkShift? WorkShift { get; set; }
        public List<NextOfKin> nextOfKins { get; set; }
        public EmployeeImage? EmployeeImage { get; set; }
        public SalaryDetails? SalaryDetails { get; set; }
        public HRDetails? HRDetails { get; set; }
        public ContactDetails? ContactDetails { get; set; }
        public bool deletestatus { get; set; }
    }

    public class EmployeeImage
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string ImagePath { get; set; }
        public bool DeleteStatus { get; set; }
    }
    //salary details
    public class SalaryDetails
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(10)]
        public string PaymentCurrency { get; set; }
        public double MonthlySalary { get; set; }
        public PaymentData SalaryProcessingOption { get; set; }
        public int? WorkShiftId { get; set; }
        public WorkShift? WorkShift { get; set; }
        [StringLength(50)]
        public string? OffDays { get; set; }
        public int WorkHours { get; set; }
        public double HourlyRate { get; set; }
        public double DailyRate { get; set; }
        public string IncomeTax { get; set; }
        public bool DeductNHIF { get; set; }
        public bool DeductNSSF { get; set; }
        [Required, StringLength(50)]
        public string EmploymentType { get; set; }
        public double DisabilityExcemptionAmount { get; set; }
        public string ExcemptionCertNo { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class PaymentData
    {
        [Key, Required]
        public int Id { get; set; }
        public string PaymentOption { get; set; }
        public bool Status { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }
        public string? BankAccountNo { get; set; }
        [StringLength(50)]
        public string? BankBranch { get; set; }
        [Required, StringLength(20)]
        public string? ShortCode { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
    public class ShiftRotation
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; }
        public ICollection<WorkShift>? workShifts { get; set; }
        public int? CurrentWorkShiftId { get; set; }
        public int? PrevWorkShiftId { get; set; }
        public int RunforDays { get; set; }
        public int BreakForDays { get; set; }
        public DateTime NextChangeDate { get; set; }
        public bool deletestatus { get; set; }
    }
    public class WorkShift
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string ShiftTitle { get; set; }
        [Required, StringLength(10)]
        public TimeSpan Monday { get; set; }
        public TimeSpan TuesDay { get; set; }
        public TimeSpan Wednesday { get; set; }
        public TimeSpan Thursday { get; set; }
        public TimeSpan Friday { get; set; }
        public TimeSpan Saturday { get; set; }
        public TimeSpan Sunday { get; set; }
        public TimeSpan Duration { get; set; }
        public int TotalHours { get; set; }
        public int BreakHours { get; set; }
        public int WorkHours { get; set; }
        public TimeSpan HoursPerWeek { get; set; }
        public int ShiftRotationId { get; set; }
        public ShiftRotation? ShiftRotation { get; set; }
        public bool deletestatus { get; set; }
    }
    //HR details

    public class HRDetails
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string JobNumber { get; set; }
        [Required, StringLength(50)]
        public string JobTitle { get; set; }
        [Required]
        public DateTime DateofEmployment { get; set; }
        public Contracts? Contracts { get; set; }
        public int? HeadOfDepartmentId { get; set; }
        public int? ReportsToEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class Contracts
    {
        [Key, Required]
        public int Id { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public int? ContractDuration { get; set; }
        public double MonthlySalary { get; set; }
        [StringLength(255)]
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
    public class Projects
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Code { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; }
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class Regions
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
        public int? ParentRegionsId { get; set; }
        public ParentRegions? ParentRegions { get; set; }
        public int EmployeeId { get; set; }
        public Employee? employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class ParentRegions
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string RegionCode { get; set; }
        [Required]
        public string RegionName { get; set; }
        public Regions Regions { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class Departments
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string DeptCode { get; set; }
        [Required]
        public string DeptName { get; set; }
        public int? ParentDepartmentsId { get; set; }
        public ParentDepartments? ParentDepartments { get; set; }
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class ParentDepartments
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string DeptCode { get; set; }
        [Required]
        public string DeptName { get; set; }
        public Departments Departments { get; set; }
        public bool DeleteStatus
        {
            get; set;
        }
    }
    //contact details
    public class ContactDetails
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        [EmailAddress]
        public string OfficialEmail { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string? PersonalEmail { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(15)]
        public string OfficialPhoneNumber { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    public class NextOfKin
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Relation { get; set; }
        [Required, StringLength(100)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public int EmployeeId { get; set; }
        public Employee? employee { get; set; }
        public bool DeleteStatus { get; set; }
    }
    //payroll data
    public class Benefits
    {
        [Key, Required]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        public double Amount { get; set; }
        public double? Rate { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
    public class Earnings
    {
        [Key, Required]
        public int Id { get; set; }
        public double Amount { get; set; }
        public double? Quantity { get; set; }
        public double? Rate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
    public class Deductions
    {
        [Key, Required]
        public int Id { get; set; }
        public string? RegistrationNumber { get; set; }
        public double? Amount { get; set; }
        public double? Rate { get; set; }
        public double? EmployerAmount { get; set; }
        public double? EmployerRate { get; set; }
        public double? Quantity { get; set; }
        public double? PaidToDate { get; set; }
        public bool Status { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
    public class Loans
    {
        [Required, Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Issuedate { get; set; }
        public double Amount { get; set; }
        public double? Rate { get; set; }
        public double Installments { get; set; }
        public string InterestFormula { get; set; }
        public string FringeBenefitTax { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
