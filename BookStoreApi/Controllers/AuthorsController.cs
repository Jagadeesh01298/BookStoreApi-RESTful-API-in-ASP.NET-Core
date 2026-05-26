using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using BookStoreApi.DTOs;
using BookStoreApi.Models;

namespace BookStoreApi.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();

            return Ok(authors);
        }

        // GET: api/authors/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound(new
                {
                    message = $"Author with ID {id} was not found"
                });
            }

            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Country = authorDto.Country
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAuthorById),
                new { id = author.Id },
                author
            );
        }

        // PUT: api/authors/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDto authorDto)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound(new
                {
                    message = $"Author with ID {id} was not found"
                });
            }

            author.Name = authorDto.Name;
            author.Country = authorDto.Country;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Author updated successfully",
                author = author
            });
        }

        // DELETE: api/authors/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound(new
                {
                    message = $"Author with ID {id} was not found"
                });
            }

            if (author.Books.Any())
            {
                return BadRequest(new
                {
                    message = "Cannot delete author because books are assigned to this author"
                });
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Author deleted successfully"
            });
        }

        // GET: api/authors/1/books
        [HttpGet("{authorId}/books")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);

            if (author == null)
            {
                return NotFound(new
                {
                    message = $"Author with ID {authorId} was not found"
                });
            }

            var books = await _context.Books
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();

            return Ok(books);
        }
    }
}
