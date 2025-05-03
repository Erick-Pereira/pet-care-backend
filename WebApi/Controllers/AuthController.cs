using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using web_api.Services;
using web_api.Services.DTO;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        private readonly IUserService _userService;
        private readonly IHashService _hashService;

        public AuthController(JwtService jwtService, IUserService userService, IHashService hashService)
        {
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {

            var userResponse = await _userService.GetByEmail(request.Username);
            if (!userResponse.Success.GetValueOrDefault() || userResponse.Item == null)
                return Unauthorized("Invalid username or password.");

            var user = userResponse.Item;

            if (!await _hashService.VerifyPasswordAsync(request.Password, user.Password))
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto request)
        //{
        //    if (request.Username == "admin" && request.Password == "password")
        //    {
        //        var user = new User
        //        {
        //            FullName = "Administrator",
        //            Email = request.Username,
        //            PermissionLevel = "Admin" // You can set different permission levels
        //        };

        //        var token = _jwtService.GenerateToken(user);
        //        return Ok(new { Token = token });
        //    }

        //    if (request.Username == "user" && request.Password == "password")
        //    {
        //        var user = new User
        //        {
        //            FullName = "user",
        //            Email = request.Username,
        //            PermissionLevel = "User"
        //        };

        //        var token = _jwtService.GenerateToken(user);
        //        return Ok(new { Token = token });
        //    }

        //    return Unauthorized("Invalid username or password.");
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            try
            {
                var response = await _userService.Insert(request);

                if (!response.Success == true)
                {
                    return BadRequest(new { response.Success, response.Message });
                }

                // Generate token for the newly registered user
                var token = _jwtService.GenerateToken(request);

                return Ok(new
                {
                    response.Success,
                    response.Message,
                    Token = token
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }
    }
}