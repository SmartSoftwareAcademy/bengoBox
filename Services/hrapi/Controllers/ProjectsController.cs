using hrapi.Data;
using hrapi.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Nest.JoinField;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hrapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly HRData _context;
        public ProjectsController(HRData context)
        {
            _context = context;
        }
        // GET: api/<Projects>
        [HttpGet()]
        public async Task<IEnumerable<Projects>> GetProjects()
        {
            return await _context.Projects.OrderBy(y => y.Id).ToListAsync();
        }

        // GET api/<Projects>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projects>> GetProjects(int id)
        {
            var region = await _context.Projects.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return region;
        }

        // POST api/<Projects>
        [HttpPost()]
        public async Task<ActionResult> PostProjects(string code,string title)
        {
            var proj = new Projects
            {
                Id = 0,
                Code = code,
                Title = title,
                DeleteStatus = false
            };
            await _context.AddAsync(proj);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return new JsonResult(proj) { StatusCode = 200 };
            }
            return BadRequest();
        }

        // PUT api/<Projects>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProjects(int id, string code , string title)
        {
            var proj = await _context.Projects.FindAsync(id);
            if (id != proj.Id)
            {
                return NotFound(id);
            }
            else
            {
                proj.Title = title;
                proj.Code = code;
                _context.Entry(proj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        // DELETE api/<Projects>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjects(int id)
        {
            var Projects = await _context.Projects.FindAsync(id);
            Projects.DeleteStatus = true;
            if (Projects != null)
            {
                _context.Entry(Projects).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
