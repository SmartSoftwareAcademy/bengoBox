using BengoBoxAuth.Configuration;
using BengoBoxAuth.Models.Requests;
using BengoBoxAuth.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Net;
using System.Security.Claims;
using System.Text;
using BengoBoxAuth.Services;
using System.Security.Cryptography;
using BengoBoxAuth.Models;
using BengoBoxAuth.Data;
using MimeKit;

namespace BengoBoxAuth.Controllers
{
    [EnableCors("ServerPolicy")]
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly JwtConfig _jwtconfig;
        //private readonly IMailSender _mailSender;
        private IConfiguration _configuration;

        private readonly UserData _context;
        public AuthManagementController(UserManager<IdentityUser> userManager,/*JwtConfig jwtConfig,IMailSender emailSender,*/IConfiguration configuration,UserData userData) { 
            _userManager = userManager;
            //_jwtconfig = jwtConfig;
            //_mailSender = emailSender;
            _configuration = configuration;
            _context = userData;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        [HttpPost]
        [Route("Register")]
        [AllowAnonymous, EnableCors("ServerPolicy")]


        public async Task<IActionResult> Register([FromBody] RegistrationDto user)
        {

            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new AuthResponse()
                    {
                        Errors = new List<string>() {
                                "Email already in use"
                            },
                        Success = false
                    });
                }
                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username ,LockoutEnabled=false};
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                    var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    //var regmessage = new Message(new string[] { newUser.Email }, "Kenload v2 Auth - Account created successfully!", "An account with your email was created from the following Host:" + GetLocalIPAddress() + ".Please contact kenload admin if it wasn't you!", null);
                    //string url = $"{_configuration["AppUrl"]}/api/AuthManagement/ConfirmEmail?userid={newUser.Id}&token={validEmailToken}";
                    //var message = new Message(new string[] { newUser.Email.ToString() }, "Confirm your email", $"<h1>Kenload v2 Auth</h1>" +
                    //    $"<p>Please confirm your email by <a href='{url}'>Clicking here or Copy the link bellow  and paste on a browser tab\n</a></p>" + $"\n{url}", null);
                    await _context.userPasswords.AddAsync(new userPasswords { Id=0,UserId=newUser.Id,password=passHasher(user.Password),lastUpdated=DateTime.Now});
                    await _context.SaveChangesAsync();
                    //Thread t2 = new Thread(delegate ()
                    //{
                    //    _mailSender.SendEmail(regmessage);
                    //    _mailSender.SendEmail(message);
                    //});
                    //t2.Start();

                    return Ok(new AuthResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new AuthResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            }

            return BadRequest(new AuthResponse()
            {
                Errors = new List<string>() {
                        "Invalid payload"
                    },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        [EnableCors("ServerPolicy")]
        public async Task<IActionResult> Login([FromBody] LoginRequest user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (ModelState.IsValid)
            {
                if (existingUser == null)
                {
                    return BadRequest(new AuthResponse()
                    {
                        Errors = new List<string>() {
                                "Incorect Username!"
                            },
                        Success = false
                    });
                }
                if (existingUser.LockoutEnabled == true)
                {
                    return BadRequest(new AuthResponse()
                    {
                        Errors = new List<string>() {
                                "User account has been disabled!"
                            },
                        Success = false
                    });
                }
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new AuthResponse()
                    {
                        Errors = new List<string>() {
                                "Incorrect Password!"
                            },
                        Success = false
                    });

                }
                
                var jwtToken = GenerateJwtToken(existingUser);
                //var message = new Message(new string[] { user.Email }, "Kenload v2 Security - New user login from:" + GetLocalIPAddress(), "<h4>Hi " + user.Email.Split("@")[0] + ",<br>Did you just logged into your account from this IP Address " + GetLocalIPAddress() + "<br />Time of login " + DateTime.Now + "<br /><br />Check the activity if it wasn't you!</h4><br />.<p style='color:blue;'>This is an automated mail from kenloadv2. Please do not reply to this mail</p>", null);
                //Thread t1 = new Thread(delegate ()
                //{
                //    _mailSender.SendEmail(message);
                //});
                //t1.Start();

                return Ok(new AuthResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new AuthResponse()
            {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });

        }


        [HttpPost("ConfirmEmail/{userId}/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest(new AuthResponse()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                });

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return Ok(new AuthResponse()
                {
                    Success = true,
                });

            return BadRequest(new AuthResponse()
            {
                Success = false,
                Errors = new List<string>() {
                        "Email confirmation Failed!"
                    },
            });
        }

        [HttpPost("ForgotPassword/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new AuthResponse()
                {
                    Success = false,
                    Errors = new List<string>() {
                        "User with such email not found in our databases!"
                    },
                });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            //string url = $"{_configuration["AppUrl"]}/AuthManagement/ResetPassword?email={email}&token={validToken}";
            //var message = new Message(new string[] { email }, "Kenload v2 Auth - Reset Password", "Follow the instructions to reset your password\n" +
            //    "Click on the password reset link bellow to reset your password.\n" +
            //    new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("\n<a href={0}>click here</a>\nOr copy and paste the link below on a browser\n{0}", $"\n{url}\n") }, null);
            //Thread t3 = new Thread(delegate ()
            //{
            //    _mailSender.SendEmail(message);
            //});
            //t3.Start();

            return Ok("Success! Check your email for a password reset link!");
        }

        [HttpPost]
        [Route("ChangePassword")]
        [EnableCors("ServerPolicy")]
        public async Task<IActionResult> changePassword([FromBody] ChangePassword cp_user)
        {
            var user = await _userManager.FindByEmailAsync(cp_user.Email);
            bool passIsExists = false;
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, "Used does not exist!");

            if (String.Compare(cp_user.NewPassword, cp_user.ConfirmPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Passwords Don't Match!");
            /////
            List<userPasswords> oldpasswords = _context.userPasswords.Where(x => x.UserId == user.Id).ToList();
            var passmatch = await _userManager.CheckPasswordAsync(user, cp_user.NewPassword);
            if (passmatch)
                return new JsonResult("Password has been used with this account before.Please choose a new password!") { StatusCode = 400 };
            if (oldpasswords != null)
            {
                foreach (var item in oldpasswords)
                {
                    string oldpass = item.password;
                    if (decryptPassHasher(oldpass, cp_user.NewPassword)/*oldpass.ToString() == cp_user.NewPassword.ToString()*/)
                    {
                        passIsExists = true;
                        break;
                    }
                }
            }
            if (passIsExists)
                return new JsonResult("Password has been used with this account before.Please choose a new password!") { StatusCode = 400 };
            ///
            var result = await _userManager.ChangePasswordAsync(user, cp_user.CurrentPassword, cp_user.NewPassword);

            if (!result.Succeeded)
            {
                var errors = new List<String>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error:" + errors);
            }
            await _context.userPasswords.AddAsync(new userPasswords { Id = 0, UserId = user.Id, password = passHasher(cp_user.NewPassword),lastUpdated=DateTime.Now });
            await _context.SaveChangesAsync();
            //var message = new Message(new string[] { user.Email }, "Kenload v2 Security - Password Changed", "Hi " + user.Email + ",Your password was changed.Host Machine:" + GetLocalIPAddress() + ".Please contact system admin if it wasn't you.</p><br /><br /><p style='color:blue;'>This is an automated mail from kenloadv2. Please do not reply to this mail</p>", null);
            //Thread t4 = new Thread(delegate ()
            //{
            //    _mailSender.SendEmail(message);
            //});
            //t4.Start();
            return Ok("Password Changed successfully!");
        }
        //admin forgot password
        [HttpPost]
        [Route("AdminUserForgotPassword")]
        [EnableCors("ServerPolicy")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword pass)
        {
            var user = await _userManager.FindByEmailAsync(pass.Email);
            bool passIsExists = false;
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, "Used does not exist!");
            ///
            var oldpasswords = _context.userPasswords.Where(x => x.UserId == user.Id).ToArray();
            var passmatch = await _userManager.CheckPasswordAsync(user, pass.NewPassword);
            if (passmatch)
                return new JsonResult("Password has been used with this account before.Please choose a new password!") { StatusCode = 400 };
            if (oldpasswords != null)
            {
                foreach (var item in oldpasswords)
                {
                    string oldpass = item.password;
                    if (decryptPassHasher(oldpass, pass.NewPassword)/*oldpass.ToString() == cp_user.NewPassword.ToString()*/)
                    {
                        passIsExists = true;
                        break;
                    }
                }
            }
            if (passIsExists)
                return new JsonResult("Password has been used with this account before.Please choose a new password!") { StatusCode = 400 };
            ///

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(user, resetToken, pass.NewPassword);

            if (!passwordChangeResult.Succeeded)
            {
                var errors = new List<String>();

                foreach (var error in passwordChangeResult.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error:" + errors);
            }
            await _context.userPasswords.AddAsync(new userPasswords { Id = 0, UserId = user.Id, password = passHasher(pass.NewPassword),lastUpdated=DateTime.Now });
            await _context.SaveChangesAsync();
            //var message = new Message(new string[] { user.Email }, "Kenload v2 Security - Password Changed.", "<p>Hi " + user.Email + ",Your password was changed by " + pass.modified_by + " From Host Machine:" + GetLocalIPAddress() + ".<br/>Please contact your administrator if you're not aware of this activity!</p><br /><br /><p style='color:blue;'>This is an automated mail from kenloadv2. Please do not reply to this mail</p>", null);
            //Thread t4 = new Thread(delegate ()
            //{
            //    _mailSender.SendEmail(message);
            //});
            //t4.Start();
            return Ok("User Password Reset successful!");
        }




        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("53BDH@GE7*llpO&z*56777HGNVov~rq86ETBFUG#*@E@xXA&TE71n9Bhdh57g");//Encoding.ASCII.GetBytes(_jwtconfig.Secret);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        private string passHasher(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string PasswordHash = Convert.ToBase64String(hashBytes);
            return PasswordHash;
        }
        private bool decryptPassHasher(string passhash, string password)
        {
            /* Fetch the stored value */
            string PasswordHash = passhash;
            bool matched = false;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(PasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] == hash[i])
                {
                    matched = true;
                    break;
                }
                else
                {
                    matched = false;
                }
            return matched;
        }


    }
}
