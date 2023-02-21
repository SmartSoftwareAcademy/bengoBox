using BengoBoxAuth.Data;
using BengoBoxAuth.Models.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BengoBoxAuth.Controllers
{
    [EnableCors("ServerPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly JwtConfig _jwtconfig;
        //private readonly IMailSender _mailSender;

        private readonly UserData _context;

        public UsersController(UserManager<IdentityUser> userManager,UserData userData) {
            _userManager = userManager;
            //_jwtconfig = jwtConfig;
            //_mailSender = emailSender;
            _context = userData;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        [HttpGet("byMail/{email}")]
        public async Task<ActionResult<IdentityUser>> GetUsersByEmail(string email)
        {
            IdentityUser user= await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("assignRoles/{email}")]
        public async Task<ActionResult<IdentityUser>> AssignRoles(string email,List<string> roleids)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            IdentityResult result = await _userManager.AddToRolesAsync(user, roleids);
            return Ok(result);
        }

        [HttpPost("updateRoles/{email}")]
        public async Task<ActionResult<IdentityUser>> UpdateRoles(string email, List<string> roleids)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
           var roles = await _userManager.GetRolesAsync(user);
            return Ok();
        }


        // POST: api/Users/5
        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateUser(string id,string email,string userName)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    user.Email = email;
                    user.UserName= userName;
                }
                else
                {
                    ModelState.AddModelError("", "Email cannot be empty");
                }
                if (!string.IsNullOrEmpty(email))
                {
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        new JsonResult("User updated successfully!") { StatusCode = 200 };
                    }
                    else
                    {
                        new JsonResult("Something went wrong!") { StatusCode = 400 };
                    }
                }
            }
            return new JsonResult("User updated successfully!") { StatusCode = 200 };
        }


        [HttpPost("lockout/{id}")]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (id != user.Id)
            {
                return BadRequest();
            }
            user.LockoutEnabled = true;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponse()
                {
                    Errors = new List<string>()
                    {
                        "Update failed!"
                    },
                    Success = false
                });
            }
            return NoContent();
        }
        // ENABLE: api/Users/enable/5
        [HttpPost("enable/{id}")]
        public async Task<IActionResult> EnableUsers(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (id != user.Id)
            {
                return BadRequest();
            }
            user.LockoutEnabled = false;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new AuthResponse()
                {
                    Errors = new List<string>()
                    {
                        "Update failed!"
                    },
                    Success = false
                });
            }
            return NoContent();
        }

    }
}
