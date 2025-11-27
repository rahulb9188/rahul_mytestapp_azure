using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using myapp_customerwebapp_azure.Application.Interfaces.Services;
using myapp_customerwebapp_azure.Application.Models;
using myapp_customerwebapp_azure.Application.Models.Request;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace myapp_customerwebapp_azure.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Log.Information("Login action called");
            Log.Information("Login attempt for user: {Email}", request.Email);
            TokenResponse token = await _authService.LoginAsync(request);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result)
                return BadRequest("Registration failed");

            return Ok("Registered successfully, pending approval");
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3f#8v!bZx9@Lp2&Nj7qRw6$YgTmD1FsUc"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/",
                audience: "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
