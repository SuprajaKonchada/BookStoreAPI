using BookStoreAPI.Models;
using BookStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly string _adminKey = configuration["AdminKey"];

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The registration request containing username, password, role, and optional admin key.</param>
        /// <returns>Returns 200 OK if registration is successful, 400 Bad Request if the user already exists, or 401 Unauthorized if the provided admin key is invalid.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _authService.RegisterAsync(request, _adminKey);
                return Ok(new { Message = "Account created successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>Returns 200 OK with a JWT token if login is successful, or 401 Unauthorized if the credentials are invalid.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAsync(request);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
