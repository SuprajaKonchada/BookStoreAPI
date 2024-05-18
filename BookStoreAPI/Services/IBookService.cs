using BookStoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Services
{
    /// <summary>
    /// Interface for handling book-related operations.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>Returns a list of all books.</returns>
        Task<IEnumerable<Book>> GetBooksAsync();

        /// <summary>
        /// Retrieves a specific book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <returns>Returns the book with the specified ID, or null if no such book exists.</returns>
        Task<Book> GetBookByIdAsync(int id);

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book object representing the book to create.</param>
        /// <returns>Returns the newly created book.</returns>
        Task<Book> CreateBookAsync(Book book);

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="updatedBook">The updated book object.</param>
        /// <returns>Returns the updated book, or null if no such book exists.</returns>
        Task<Book> UpdateBookAsync(int id, Book updatedBook);

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>Returns the deleted book, or null if no such book exists.</returns>
        Task<Book> DeleteBookAsync(int id);
    }
}
