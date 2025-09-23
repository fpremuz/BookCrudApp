using Microsoft.AspNetCore.Mvc;

namespace BookCrudApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> Books = new List<Book>();

        [HttpGet]
        public IEnumerable<Book> Get() => Books;

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            book.Id = Books.Count > 0 ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var existing = Books.FirstOrDefault(b => b.Id == id);
            if (existing == null) return NotFound();
            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Pages = book.Pages;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            Books.Remove(book);
            return NoContent();
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int Pages { get; set; }
    }
}