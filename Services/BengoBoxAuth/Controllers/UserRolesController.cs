using BengoBoxAuth.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BengoBoxAuth.Controllers
{
    [EnableCors("ServerPolicy")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserData _context;
        private RoleManager<IdentityRole> _roleManager;
        public UserRolesController(UserData context, RoleManager<IdentityRole> roleManager) { 
            _context = context;
            _roleManager = roleManager;
        }
        // GET: api/<UserRolesController>
        [HttpGet]
        public async Task<IEnumerable<IdentityRole>> Get()
        {
            return _roleManager.Roles;
        }

        // GET api/<UserRolesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityRole>> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }
        // GET api/<UserRolesController>/5
        [HttpGet("{rolename}")]
        public async Task<ActionResult<IdentityRole>> SearchRoles(string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // POST api/<UserRolesController>
        [HttpPost]
        public async Task<ActionResult> Post(string roleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Payload");
            }
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                IdentityRole createdRole =await  _roleManager.FindByNameAsync(roleName);
                if (result.Succeeded)
                    return new JsonResult(createdRole) { StatusCode = 200 };
                else
                    new JsonResult(result) { StatusCode = 500 };
                return Ok(createdRole);
        }

        // PUT api/<UserRolesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] IdentityRole role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }
           IdentityResult result=await _roleManager.UpdateAsync(role);
         return Ok(result);
        }



        // DELETE api/<UserRolesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return Ok(result);
                else
                    BadRequest(result);
            }
            return BadRequest("Inavlid Request!");
        }

        private bool RoleExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}
