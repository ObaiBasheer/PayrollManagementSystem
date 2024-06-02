using Mango.Services.AuthAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollManagementSystem.Models;
using PayrollManagementSystem.Models.ViewModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PayrollManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AuthService _Service;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, AuthService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _Service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password!);

            if (result.Succeeded)
            {
                _Service.AssignRole(model.Email, SD.RoleAdmin);
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("AssignRole")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AssignRole(string email, string roleName)
        {
           var result = await _Service.AssignRole(email, roleName);

            if (result)
            {
                return Ok(new { Message = "User registered successfully" });
            }else
            {
                return BadRequest();
            }

           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email!);
                var role = await _userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user!, role);

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }



        private string GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:JwtOptions:key")!);

            var claimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("JWT:JwtOptions:Issuer"),
                Audience = _configuration.GetValue<string>("JWT:JwtOptions:Audience"),
                Expires = DateTime.UtcNow.AddDays(5),
                Subject = new ClaimsIdentity(claimList),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}
