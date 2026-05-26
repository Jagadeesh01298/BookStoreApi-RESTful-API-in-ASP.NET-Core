using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using BookStoreApi.DTOs;
using BookStoreApi.Models;

namespace BookStoreApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .ToListAsync();

            return Ok(books);
        }

        // GET: api/books/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound(new
                {
                    message = $"Book with ID {id} was not found"
                });
            }

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(BookDto bookDto)
        {
            var authorExists = await _context.Authors
                .AnyAsync(a => a.Id == bookDto.AuthorId);

            if (!authorExists)
            {
                return BadRequest(new
                {
                    message = "Invalid AuthorId. Author does not exist."
                });
            }

            var book = new Book
            {
                Title = bookDto.Title,
                PublicationYear = bookDto.PublicationYear,
                AuthorId = bookDto.AuthorId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBookById),
                new { id = book.Id },
                book
            );
        }

        // PUT: api/books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new
                {
                    message = $"Book with ID {id} was not found"
                });
            }

            var authorExists = await _context.Authors
                .AnyAsync(a => a.Id == bookDto.AuthorId);

            if (!authorExists)
            {
                return BadRequest(new
                {
                    message = "Invalid AuthorId. Author does not exist."
                });
            }

            book.Title = bookDto.Title;
            book.PublicationYear = bookDto.PublicationYear;
            book.AuthorId = bookDto.AuthorId;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Book updated successfully",
                book = book
            });
        }

        // DELETE: api/books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new
                {
                    message = $"Book with ID {id} was not found"
                });
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Book deleted successfully"
            });
        }
    }
}
