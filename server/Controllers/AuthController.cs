using Microsoft.AspNetCore.Mvc;

namespace Banyan.Test.WeatherAPI
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService;

        public AuthController(JWTService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            // Mock authentication (replace with your user validation logic)
            if (user.Username == "admin" && user.Password == "password")
            {
                var token = _jwtService.GenerateToken(user.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials.");
        }
    }
}
