using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookStoreAPI.Services
{
    /// <summary>
    /// Service class for handling authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly BookStoreDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing user data.</param>
        /// <param name="jwtTokenGenerator">The JWT token generator for creating authentication tokens.</param>
        public AuthService(BookStoreDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The registration request containing username, password, role, and optional admin key.</param>
        /// <param name="adminKey">The admin key for authorizing admin role registration.</param>
        /// <returns>Returns the newly created user.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the provided admin key is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown when a user with the same username already exists.</exception>
        public async Task<User> RegisterAsync(RegisterRequest request, string adminKey)
        {
            if (request.Role == "Admin" && request.AdminKey != adminKey)
            {
                throw new UnauthorizedAccessException("Invalid admin key");
            }

            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);
            if (existingUser != null)
            {
                throw new ArgumentException("User already exists");
            }

            var user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>Returns a JWT token if login is successful.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the credentials are invalid.</exception>
        public async Task<string> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            return _jwtTokenGenerator.GenerateToken(user.Username, user.Role);
        }
    }
}
