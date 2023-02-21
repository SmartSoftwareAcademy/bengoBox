using hrapi.Data;
using hrapi.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hrapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkShiftController : ControllerBase
    {

        private readonly HRData _context;
        public  WorkShiftController(HRData context)
        {
            _context = context;
        }
        // GET: api/<WorkShift>
        [HttpGet()]
        public async Task<IEnumerable<WorkShift>> GetWorkShits()
        {
            return await _context.WorkShifts.OrderBy(y=>y.Id).ToListAsync();
        }

        // GET api/<WorkShift>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WorkShift>
        [HttpPost()]
        public async Task<ActionResult> PostWorkShits([FromBody] WorkShift workShift)
        {
            if(ModelState.IsValid)
            {
                await _context.AddAsync(workShift);
                await _context.SaveChangesAsync();
                return new JsonResult(workShift) { StatusCode = 200 };
            }
            return BadRequest(ModelState);
        }

        // PUT api/<WorkShift>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutWorkShits(int id, [FromBody] WorkShift workShift)
        {
            if(id != workShift.Id)
            {
                return NotFound(id);
            }
            if(ModelState.IsValid) { 
            _context.Entry(workShift).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
            }
            return BadRequest();
        }

        // DELETE api/<WorkShift>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorkshift(int id)
        { 
            var workShift=await _context.WorkShifts.FindAsync(id);
            workShift.deletestatus = true;
            if (workShift != null)
            {
                _context.Entry(workShift).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
