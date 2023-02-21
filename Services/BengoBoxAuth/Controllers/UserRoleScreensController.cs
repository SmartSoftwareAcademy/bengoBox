

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using BengoBoxAuth.Data;
using BengoBoxAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BengoBoxAuth.Controllers
{
    [EnableCors("ServerPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleScreensController : ControllerBase
    {
        private readonly UserData _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleScreensController(UserData context, RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;

        }
        // GET: api/<UserRoleScreensController>
        [HttpGet]
        public async Task<IEnumerable<UserRoleScreens>> Get()
        {
            return _context.UserRoleScreens;
        }

        [HttpGet("byMail/{email}")]
        public async Task<Array> GetRoleByUserEmail(string email)
        {
            HashSet<string> uniqueScreens = new HashSet<string>();
            IdentityUser user=await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var roles=await _userManager.GetRolesAsync(user);
                if (roles.Count >1)
                {
                    foreach (var role in roles)
                    {
                        var roleObj = await _roleManager.FindByNameAsync(role);
                        var foundscreens = await _context.UserRoleScreens.Where(x => x.UserRoleId == roleObj.Id).FirstOrDefaultAsync();
                        string[]? screenlist = foundscreens.Screens.Split(",");
                        if (screenlist != null)
                        {
                            foreach (var screen in screenlist)
                            {
                                uniqueScreens.Add(screen);
                            }

                        }

                    }

                }
                else
                {
                    var roleObj = await _roleManager.FindByNameAsync(roles.First());
                    var foundRole =await _context.UserRoleScreens.Where(x => x.UserRoleId == roleObj.Id).Select(x => new { x.Screens }).FirstOrDefaultAsync();
                    if (foundRole != null)
                    {
                        foreach (var screen in foundRole.Screens.Split(","))
                        {
                            uniqueScreens.Add(screen);
                        }
                    }
                }

            }
            string cleanedscreens = string.Join(",", uniqueScreens);
            List<UserRoleScreens> userscreens = new List<UserRoleScreens>
            {
                new UserRoleScreens{ Screens = cleanedscreens },
                
            };
            Console.WriteLine(userscreens);
            return userscreens.ToArray();
        }
    

        // GET api/<UserRoleScreensController>/5
       [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleScreens>> GetScreens(int id)
        {
            var screens = await _context.UserRoleScreens.FindAsync(id);

            if (screens == null)
            {
                return NotFound();
            }

            return screens;
        }

        // POST api/<UserRoleScreensController>
        [HttpPost]
        public async Task<ActionResult<UserRoleScreens>> Post(string roleid,string screens)
        {
            IdentityRole role=await _roleManager.FindByIdAsync(roleid);
            if (role == null)
            {
                return NotFound();
            }
            await _context.UserRoleScreens.AddAsync(new UserRoleScreens { deleted=false,Screens=screens,UserRoleId=role.Id});
            await _context.SaveChangesAsync();
            return Ok("Success");
        }

        // PUT api/<UserRoleScreensController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserRoleScreens>> Put(int id, string screens)
        {
            var screen = await _context.UserRoleScreens.FindAsync(id);
            if (id != screen.Id)
            {
                return BadRequest();
            }
            var roleScreens = await _context.UserRoleScreens.FindAsync(id);
            roleScreens.Screens=screens;
            roleScreens.deleted=false;
            _context.Entry(roleScreens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenExists(id))
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

        // DELETE api/<UserRoleScreensController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserRoleScreens>> Delete(int id)
        {
            var screen = await _context.UserRoleScreens.FindAsync(id);
            if (id != screen.Id)
            {
                return BadRequest();
            }
            screen.deleted = true;
            _context.Entry(screen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenExists(id))
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
        private bool ScreenExists(int id)
        {
            return _context.UserRoleScreens.Any(e => e.Id == id);
        }
    }
}
