using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models;

/// <summary>
/// Represents a user entity in the BookStore API.
/// </summary>
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Role { get; set; }
}
