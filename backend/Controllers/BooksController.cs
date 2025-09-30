using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCrudApi.Data;
using BookCrudApi.Models;
using BookCrudApi.Services;

namespace BookCrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmbeddingService _embeddingService;

        public BooksController(AppDbContext context, IEmbeddingService embeddingService)
        {
            _context = context;
            _embeddingService = embeddingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Query parameter 'q' is required");
            }

            try
            {
                var books = await _embeddingService.SearchSimilarBooksAsync(q, 5);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching books: {ex.Message}");
            }
        }

        [HttpPost("generate-embeddings")]
        public async Task<IActionResult> GenerateEmbeddings()
        {
            try
            {
                await _embeddingService.GenerateAndStoreEmbeddingsAsync();
                return Ok("Embeddings generated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating embeddings: {ex.Message}");
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}