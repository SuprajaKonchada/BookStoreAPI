using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BookStoreAPI.Models;

namespace BookStoreAPI.Services;

/// <summary>
/// Service for generating JWT tokens.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
/// </remarks>
/// <param name="jwtSettings">The JWT settings used for token generation.</param>
public class JwtTokenGenerator(JwtSettings jwtSettings) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings;

    /// <summary>
    /// Generates a JWT token based on the provided username and role.
    /// </summary>
    /// <param name="username">The username to include in the token.</param>
    /// <param name="role">The role to include in the token.</param>
    /// <returns>The generated JWT token.</returns>
    public string GenerateToken(string username, string role)
    {
        // Define the claims for the token, including username and role
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role)
        };

        // Create the symmetric security key using the secret key from JWT settings
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        // Create signing credentials using the security key and HMAC-SHA256 algorithm
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Set the expiration time for the token
        var expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes);

        // Create a new JWT token with issuer, audience, claims, expiration, and signing credentials
        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
