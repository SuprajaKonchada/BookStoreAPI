using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models;

/// <summary>
/// Represents a book entity in the BookStore API.
/// </summary>
public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Author { get; set; }
}
