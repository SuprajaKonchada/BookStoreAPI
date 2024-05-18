using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Services
{
    /// <summary>
    /// Service class for handling book-related operations.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly BookStoreDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing book data.</param>
        public BookService(BookStoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>Returns a list of all books.</returns>
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>Returns the book with the specified ID, or null if no such book exists.</returns>
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book object representing the book to create.</param>
        /// <returns>Returns the newly created book.</returns>
        public async Task<Book> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="updatedBook">The updated book object.</param>
        /// <returns>Returns the updated book, or null if no such book exists.</returns>
        public async Task<Book> UpdateBookAsync(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;

            await _context.SaveChangesAsync();
            return book;
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>Returns the deleted book, or null if no such book exists.</returns>
        public async Task<Book> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
