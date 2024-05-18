namespace BookStoreAPI.Services;

/// <summary>
/// Represents a service for generating JWT tokens.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token based on the provided username and role.
    /// </summary>
    /// <param name="username">The username to include in the token.</param>
    /// <param name="role">The role to include in the token.</param>
    /// <returns>The generated JWT token.</returns>
    string GenerateToken(string username, string role);
}
