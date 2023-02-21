using hrapi.Data;
using hrapi.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using static Nest.JoinField;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hrapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly HRData _context;
        public DepartmentsController(HRData context)
        {
            _context = context;
        }
        // GET: api/<Departments>
        [HttpGet()]
        public async Task<IEnumerable<Departments>> GetDepartments()
        {
            return await _context.Departments.OrderBy(y => y.Id).ToListAsync();
        }
        // GET: api/<ParentDepartments>
        [HttpGet("ParentDepartments")]
        public async Task<IEnumerable<ParentDepartments>> GetParentDepartments()
        {
            return await _context.ParentDepartments.OrderBy(y => y.Id).ToListAsync();
        }

        // GET api/<Departments>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departments>> GetDepartments(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }
        // GET api/<ParentDepartments>/5
        [HttpGet("ParentDepartments/{id}")]
        public async Task<ActionResult<ParentDepartments>> GetParentDepartments(int id)
        {
            var dept = await _context.ParentDepartments.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return dept;
        }

        // POST api/<Departments>
        [HttpPost()]
        public async Task<ActionResult> PostDepartments(string deptcode,string deptname,int parentid)
        {
                var newdept = new Departments
                {
                    Id = 0,
                    DeptCode = deptcode,
                    DeptName = deptname,
                    ParentDepartmentsId = parentid,
                    DeleteStatus = false
                };
                await _context.AddAsync(newdept);
               var result= await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return new JsonResult(newdept) { StatusCode = 200 };
                }
            return BadRequest();
        }
        [HttpPost("ParentDepartments")]
        public async Task<ActionResult> PostParentDepartment(string deptcode, string deptname)
        {
            if (ModelState.IsValid)
            {
                var newdept = new ParentDepartments
                {
                    Id = 0,
                    DeptCode = deptcode,
                    DeptName = deptname,
                    DeleteStatus = false
                };
                await _context.AddAsync(newdept);
                await _context.SaveChangesAsync();
                return new JsonResult(newdept) { StatusCode = 200 };
            }
            return BadRequest(ModelState);
        }

        // PUT api/<Departments>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartments(int id, string code,string name,int parentid)
        {
            var dept=await _context.Departments.FindAsync(id);
            dept.DeptName = name;
            dept.DeptCode = code;
            dept.ParentDepartmentsId = parentid;
            if (id != dept.Id)
            {
                return NotFound(id);
            }
                _context.Entry(dept).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
        }
        // PUT api/<ParentDepartments>/5
        [HttpPut("ParentDepartments/{id}")]
        public async Task<ActionResult> PutParentDepartments(int id, string code, string name)
        {
            var dept = await _context.ParentDepartments.FindAsync(id);
            dept.DeptName = name;
            dept.DeptCode = code;
            if (id != dept.Id)
            {
                return NotFound(id);
            }
            _context.Entry(dept).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<Departments>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartments(int id)
        {
            var Departments = await _context.Departments.FindAsync(id);
            Departments.DeleteStatus = true;
            if (Departments != null)
            {
                _context.Entry(Departments).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
        // DELETE api/<ParentDepartments>/5
        [HttpDelete("ParentDeptments/{id}")]
        public async Task<ActionResult> DeleteParentDepartments(int id)
        {
            var Departments = await _context.ParentDepartments.FindAsync(id);
            Departments.DeleteStatus = true;
            if (Departments != null)
            {
                _context.Entry(Departments).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
