using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get() =>
            await _bookService.GetAsync();

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            Console.WriteLine($"GET request received for book ID: {id}");

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Invalid book ID: ID is empty or null");
                return BadRequest("Book ID cannot be null or empty");
            }

            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                Console.WriteLine($"Book with ID {id} was not found in the database");
                return NotFound($"Book with ID {id} was not found");
            }

            Console.WriteLine($"Book found: {book.Name} (ID: {book.Id})");
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create(Book book)
        {
            await _bookService.CreateAsync(book);

            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Book bookIn)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Book ID cannot be null or empty");
            }

            Console.WriteLine($"Attempting to update book with ID: {id}");

            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} was not found");
            }

            bookIn.Id = id;

            await _bookService.UpdateAsync(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _bookService.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _bookService.RemoveAsync(id);

            return NoContent();
        }
    }
}