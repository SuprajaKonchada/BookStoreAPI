namespace BookStoreAPI.Models;

/// <summary>
/// Represents a registration request containing username, password, role, and an optional admin key.
/// </summary>
public class RegisterRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public string? AdminKey { get; set; }
}
