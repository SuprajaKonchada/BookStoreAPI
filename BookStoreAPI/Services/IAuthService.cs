using BookStoreAPI.Models;
using System.Threading.Tasks;

namespace BookStoreAPI.Services
{
    /// <summary>
    /// Interface for handling authentication-related operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The registration request containing username, password, role, and optional admin key.</param>
        /// <param name="adminKey">The admin key for authorizing admin role registration.</param>
        /// <returns>Returns the newly created user.</returns>
        Task<User> RegisterAsync(RegisterRequest request, string adminKey);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The login request containing username and password.</param>
        /// <returns>Returns a JWT token if login is successful.</returns>
        Task<string> LoginAsync(LoginRequest request);
    }
}
