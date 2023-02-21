using hrapi.Data;
using hrapi.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty NextOfKins, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hrapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NextOfKinsController : ControllerBase
    {
        private readonly HRData _context;
        public NextOfKinsController(HRData context)
        {
            _context = context;
        }
        // GET: api/<NextOfKins>
        [HttpGet()]
        public async Task<IEnumerable<NextOfKin>> GetNextOfKins()
        {
            return await _context.NextOfKins.OrderBy(y => y.Id).ToListAsync();
        }

        // GET api/<NextOfKins>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NextOfKin>> GetNextOfKins(int id)
        {
            var region = await _context.NextOfKins.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return region;
        }

        // POST api/<NextOfKins>
        [HttpPost()]
        public async Task<ActionResult> PostNextOfKins([FromBody] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(nextOfKin);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return new JsonResult(nextOfKin) { StatusCode = 200 };
                }
            }
            return BadRequest();
        }

        // PUT api/<NextOfKins>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutNextOfKins(int id, [FromBody] NextOfKin nextOfKin)
        {
            if (id !=nextOfKin.Id)
            {
                return NotFound(id);
            }
            if (ModelState.IsValid)
            {
                _context.Entry(nextOfKin).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        // DELETE api/<NextOfKins>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNextOfKins(int id)
        {
            var NextOfKins = await _context.NextOfKins.FindAsync(id);
            NextOfKins.DeleteStatus = true;
            if (NextOfKins != null)
            {
                _context.Entry(NextOfKins).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
