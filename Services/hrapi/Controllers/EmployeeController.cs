using hrapi.Data;
using hrapi.Models.Employee;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using YouZack.FromJsonBody;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hrapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly HRData _context;
        public EmployeeController(HRData context) {
            _context = context;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET api/<Employee>/5
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee=await _context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        // GET api/<EmployeSalaryDetails>/5
        [HttpGet("ContactDetails/{id}")]
        public async Task<ActionResult<ContactDetails>> GetEmployeeContactDetails(int id)
        {
            var details = await _context.ContactDetails.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return details;
        }

        // GET api/<EmployeSalaryDetails>/5
        [HttpGet("SalaryDetails/{id}")]
        public async Task<ActionResult<SalaryDetails>> GetEmployeeSalaryDetails(int id)
        {
            var details = await _context.SalaryDetails.FindAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return details;
        }

        // POST api/Employee
        [HttpPost]
        public async Task<ActionResult> PostEmployee(Employee employee)
        {
            var emp = employee;
            if (ModelState.IsValid)
            {
                //add personal details
                var newemp = new Employee
                {
                    Id = 0,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    Nationa_ID = employee.Nationa_ID,
                    Gender = employee.Gender,
                    DoB = employee.DoB,
                    Pin_NO = employee.Pin_NO,
                    ResidentialDetails = employee.ResidentialDetails,
                    NHIF_NO = employee.NHIF_NO,
                    NSSF_NO = employee.NSSF_NO,
                    deletestatus = false
                };
                await _context.Employees.AddAsync(newemp);
                await _context.SaveChangesAsync();
                var empid = newemp.Id;
                Console.WriteLine(empid);
                //add nhif , nssf defaults
                if (employee.SalaryDetails.DeductNHIF) {
                    var newdeductionsnhif = new Deductions
                    {
                        Id = 0,
                        RegistrationNumber = employee.NHIF_NO,
                        Status = employee.SalaryDetails.DeductNHIF,
                        Amount = 950.00,
                        EmployerAmount = 0.00,
                        Rate = 0.00,
                        EmployerRate = 0.00,
                        PaidToDate = 0.00,
                        EmployeeId = empid,
                        Quantity = 0
                    };
                    await _context.Deductions.AddAsync(newdeductionsnhif);
                    await _context.SaveChangesAsync();
                }
                if (employee.SalaryDetails.DeductNSSF)
                {
                    var newdeductionsnssf = new Deductions
                    {
                        Id = 0,
                        RegistrationNumber = employee.NSSF_NO,
                        Status = employee.SalaryDetails.DeductNSSF,
                        Amount = 1080.00,
                        EmployerAmount = 0.00,
                        Rate = 0.00,
                        EmployerRate = 0.00,
                        PaidToDate = 0.00,
                        EmployeeId = empid,
                        Quantity = 0
                    };
                    await _context.Deductions.AddAsync(newdeductionsnssf);
                    await _context.SaveChangesAsync();
                }
                //add paymentoptions
                var newpayoption = new PaymentData
                {
                    Id = 0,
                    PaymentOption = employee.SalaryDetails.SalaryProcessingOption.PaymentOption,
                    Status = employee.SalaryDetails.SalaryProcessingOption.Status,
                    BankName = employee.SalaryDetails.SalaryProcessingOption.BankName,
                    BankBranch = employee.SalaryDetails.SalaryProcessingOption.BankBranch,
                    BankAccountNo = employee.SalaryDetails.SalaryProcessingOption.BankAccountNo,
                    ShortCode = employee.SalaryDetails.SalaryProcessingOption.ShortCode,
                    EmployeeId = empid
                };
                await _context.PaymentData.AddAsync(newpayoption);
                await _context.SaveChangesAsync();
                ////add salary  details
                var newsalarydetails = new SalaryDetails
                {
                    Id = 0,
                    EmploymentType = employee.SalaryDetails.EmploymentType,
                    MonthlySalary = employee.SalaryDetails.MonthlySalary,
                    HourlyRate = employee.SalaryDetails.HourlyRate,
                    DailyRate = employee.SalaryDetails.DailyRate,
                    PaymentCurrency = employee.SalaryDetails.PaymentCurrency,
                    IncomeTax = employee.SalaryDetails.IncomeTax,
                    OffDays = employee.SalaryDetails.OffDays,
                    WorkShiftId = employee.SalaryDetails.WorkShiftId,
                    SalaryProcessingOption = newpayoption,
                    DeductNHIF = employee.SalaryDetails.DeductNHIF,
                    DeductNSSF = employee.SalaryDetails.DeductNHIF,
                    DisabilityExcemptionAmount = employee.SalaryDetails.DisabilityExcemptionAmount,
                    ExcemptionCertNo = employee.SalaryDetails.ExcemptionCertNo,
                    EmployeeId = empid,
                    DeleteStatus = false,
                };
                await _context.SalaryDetails.AddAsync(newsalarydetails);
                await _context.SaveChangesAsync();
                ////add contract details
                var newcontract = new Contracts
                {
                    Id = 0,
                    ContractStartDate = employee.HRDetails.Contracts.ContractStartDate,
                    ContractEndDate = employee.HRDetails.Contracts.ContractEndDate,
                    ContractDuration = employee.HRDetails.Contracts.ContractDuration,
                    MonthlySalary = employee.HRDetails.Contracts.MonthlySalary,
                    Notes = employee.HRDetails.Contracts.Notes,
                    EmployeeId = empid,
                };
                await _context.Contracts.AddAsync(newcontract);
                await _context.SaveChangesAsync();
                ////add hr details
                var newhrdetails = new HRDetails
                {
                    Id = 0,
                    JobNumber = employee.HRDetails.JobNumber,
                    JobTitle = employee.HRDetails.JobTitle,
                    DateofEmployment = employee.HRDetails.DateofEmployment,
                    Contracts= newcontract,
                    HeadOfDepartmentId = employee.HRDetails.HeadOfDepartmentId,
                    ReportsToEmployeeId = employee.HRDetails.ReportsToEmployeeId,
                    EmployeeId = empid,
                    DeleteStatus = false
                };
                await _context.HRDetails.AddAsync(newhrdetails);
                await _context.SaveChangesAsync();
                ////add contact details
                var newcontactdetails = new ContactDetails
                {
                    OfficialEmail = employee.ContactDetails.OfficialPhoneNumber,
                    PersonalEmail = employee.ContactDetails.PersonalEmail,
                    Country = employee.ContactDetails.Country,
                    Address = employee.ContactDetails.Address,
                    PhoneNumber = employee.ContactDetails.PhoneNumber,
                    OfficialPhoneNumber = employee.ContactDetails.OfficialPhoneNumber,
                    City = employee.ContactDetails.City,
                    State = employee.ContactDetails.State,
                    ZipCode = employee.ContactDetails.ZipCode,
                    EmployeeId = empid,
                    DeleteStatus = false
                };
                await _context.ContactDetails.AddAsync(newcontactdetails);
                await _context.SaveChangesAsync();
                //var syncedContact= CreatedAtAction("GetEmployeeContactDetails", new { Id = employee.ContactDetails.Id }, employee.ContactDetails);
                //Console.Write(syncedContact);
                //add next of kins
                List<NextOfKin> kins = new List<NextOfKin>(employee.nextOfKins);
                if (kins.Count > 0)
                {
                    for (int i = 0; i < kins.Count; i++)
                    {
                        NextOfKin? kin = kins[i];
                        await _context.NextOfKins.AddAsync(new NextOfKin
                        {
                            Id = 0,
                            Name = kin.Name,
                            Relation = kin.Relation,
                            Email= kin.Email,
                            PhoneNumber= kin.PhoneNumber,
                            EmployeeId = empid,
                            DeleteStatus= false
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
            }
            return BadRequest();
        }
        
        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,Employee employee)
        {
            if(id != employee.Id)
            {
                NotFound();
            }
             _context.Entry(employee).State= EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null) { return NotFound(); }
            employee.deletestatus = true;
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
