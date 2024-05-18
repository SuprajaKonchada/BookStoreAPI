namespace BookStoreAPI.Models;

/// <summary>
/// Represents a login request containing username and password.
/// </summary>
public class LoginRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
