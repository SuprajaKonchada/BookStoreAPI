using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using BookStoreAPI.Services;

namespace BookStoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly string _adminKey;

    public AuthController(BookStoreDbContext context, IJwtTokenGenerator jwtTokenGenerator, IConfiguration configuration)
    {
        _context = context;
        _jwtTokenGenerator = jwtTokenGenerator;
        _adminKey = configuration["AdminKey"];
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">The registration request containing username, password, role, and optional admin key.</param>
    /// <returns>Returns 200 OK if registration is successful, 400 Bad Request if the user already exists, or 401 Unauthorized if the provided admin key is invalid.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // Check if the role is Admin and the AdminKey is provided
        if (request.Role == "Admin" && request.AdminKey != _adminKey)
        {
            return Unauthorized("Invalid admin key");
        }

        var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);
        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        var user = new User
        {
            Username = request.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Account created successfully" });
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The login request containing username and password.</param>
    /// <returns>Returns 200 OK with a JWT token if login is successful, or 401 Unauthorized if the credentials are invalid.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Unauthorized();
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Username, user.Role);
        return Ok(new { Token = token });
    }
}
