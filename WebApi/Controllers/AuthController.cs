using Microsoft.AspNetCore.Mvc;
using web_api.Services;
using web_api.Services.DTO;
using Entities;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            // TODO: Replace with actual user authentication
            if (request.Username == "admin" && request.Password == "password")
            {
                var user = new User
                {
                    FullName = "Administrator",
                    Email = request.Username,
                    PermissionLevel = "Admin" // You can set different permission levels
                };

                var token = _jwtService.GenerateToken(user);
                return Ok(new { Token = token });
            }

             if (request.Username == "user" && request.Password == "password")
            {
                var user = new User
                {
                    FullName = "user",
                    Email = request.Username,
                    PermissionLevel = "User" // You can set different permission levels
                };

                var token = _jwtService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}