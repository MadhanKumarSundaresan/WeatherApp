using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banyan.Test.WeatherAPI
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
    private readonly WeatherDbContext _context;    // Injected DbContext
    private readonly JWTService _jwtService;   // JWT service
    private readonly PasswordHasher _passwordHasherService;   // JWT service

    public AuthController(WeatherDbContext context, JWTService jwtService, PasswordHasher passwordHasherService)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasherService = passwordHasherService;

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        if (user == null || string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Invalid request.");
        }

        // Fetch user from DB
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        if (dbUser == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        // Verify password hash
        if (!_passwordHasherService.VerifyPassword(user.Password, dbUser.PasswordHash))
        {
            return Unauthorized("Invalid credentials.");
        }

        // Generate JWT token
        var token = _jwtService.GenerateToken(dbUser.Username);
        return Ok(new { Token = token });
    }
}
}
