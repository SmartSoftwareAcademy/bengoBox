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
    public class RegionsController : ControllerBase
    {
        private readonly HRData _context;
        public RegionsController(HRData context)
        {
            _context = context;
        }
        // GET: api/<Regions>
        [HttpGet()]
        public async Task<IEnumerable<Regions>> GetRegions()
        {
            return await _context.Regions.OrderBy(y => y.Id).ToListAsync();
        }
        //
        // GET: api/<ParentRegions>
        [HttpGet("ParentRegions")]
        public async Task<IEnumerable<ParentRegions>> GetParentRegions()
        {
            return await _context.ParentRegions.OrderBy(y => y.Id).ToListAsync();
        }

        // GET api/<Regions>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Regions>> GetRegions(int id)
        {
            var region=await _context.Regions.FindAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            return region;
        }
        // GET api/<ParentRegions>/5
        [HttpGet("ParentRegions/{id}")]
        public async Task<ActionResult<ParentRegions>> GetParentRegions(int id)
        {
            var region = await _context.ParentRegions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return region;
        }

        // POST api/<Regions>
        [HttpPost()]
        public async Task<ActionResult> PostRegions(string code, string name, int parentid)
        {
            var newregion = new Regions
            {
                Id=0,
                RegionName=name,
                RegionCode=code,
                ParentRegionsId=parentid,
                DeleteStatus=false
            };
                await _context.AddAsync(newregion);
               var result= await _context.SaveChangesAsync();
            if (result == 1)
            {
                return new JsonResult(newregion) { StatusCode = 200 };
            }
            return BadRequest();  
        }
        // POST api/<ParentRegions>
        [HttpPost("ParentRegions")]
        public async Task<ActionResult> PostParentRegions(string code, string name)
        {
            var newregion = new ParentRegions
            {
                Id = 0,
                RegionName = name,
                RegionCode = code,
                DeleteStatus = false
            };
            await _context.AddAsync(newregion);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return new JsonResult(newregion) { StatusCode = 200 };
            }
            return BadRequest();
        }

        // PUT api/<Regions>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutRegions(int id, string code,string name, int parentid)
        {
            var region = await _context.Regions.FindAsync(id);
            region.RegionCode=code;
            region.RegionName=name;
            region.ParentRegionsId=parentid;
            if (id != region.Id)
            {
                return NotFound(id);
            }
            if (ModelState.IsValid)
            {
                _context.Entry(region).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
        // PUT api/<ParentRegions>/5
        [HttpPut("ParentRegions/{id}")]
        public async Task<ActionResult> PutParentRegions(int id, string code,string name)
        {
            var region=await _context.ParentRegions.FindAsync(id);
            if (id != region.Id)
            {
                return NotFound(id);
            }
            else
            {
                region.RegionCode=code;
                region.RegionCode=name;
                _context.Entry(region).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        // DELETE api/<Regions>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRegions(int id)
        {
            var Regions = await _context.Regions.FindAsync(id);
            Regions.DeleteStatus = true;
            if (Regions != null)
            {
                _context.Entry(Regions).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
        // DELETE api/<ParentRegions>/5
        [HttpDelete("ParentRegions/{id}")]
        public async Task<ActionResult> DeleteParentRegions(int id)
        {
            var Regions = await _context.ParentRegions.FindAsync(id);
            Regions.DeleteStatus = true;
            if (Regions != null)
            {
                _context.Entry(Regions).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
