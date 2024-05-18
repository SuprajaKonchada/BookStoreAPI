using Microsoft.EntityFrameworkCore;
using BookStoreAPI.Models;

namespace BookStoreAPI.Data;

/// <summary>
/// Represents the database context for the BookStore API.
/// </summary>
public class BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : DbContext(options)
{

    /// <summary>
    /// Gets or sets the DbSet of users in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of books in the database.
    /// </summary>
    public DbSet<Book> Books { get; set; }
}
