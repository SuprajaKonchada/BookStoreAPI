using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAPI.Data;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(BookStoreDbContext context) : ControllerBase
{
    private readonly BookStoreDbContext _context = context;

    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <returns>Returns a list of all books.</returns>
    [HttpGet]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific book by its ID.
    /// </summary>
    /// <param name="id">The ID of the book to retrieve.</param>
    /// <returns>Returns the book with the specified ID, or 404 Not Found if no such book exists.</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="book">The book object representing the book to create.</param>
    /// <returns>Returns the newly created book, along with its location, or 400 Bad Request if the request is invalid.</returns>
    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    /// <summary>
    /// Updates an existing book.
    /// </summary>
    /// <param name="id">The ID of the book to update.</param>
    /// <param name="updatedBook">The updated book object.</param>
    /// <returns>Returns 200 OK with the updated book, 400 Bad Request if the request is invalid, or 404 Not Found if no such book exists.</returns>
    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> PutBook(int id, Book updatedBook)
    {
        if (id != updatedBook.Id)
        {
            return BadRequest();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;

        await _context.SaveChangesAsync();
        return Ok(book);
    }

    /// <summary>
    /// Deletes a book by its ID.
    /// </summary>
    /// <param name="id">The ID of the book to delete.</param>
    /// <returns>Returns 200 OK if the book is deleted successfully, along with a message and the deleted book. Returns 404 Not Found if no such book exists.</returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Book deleted successfully", Book = book });
    }
}
