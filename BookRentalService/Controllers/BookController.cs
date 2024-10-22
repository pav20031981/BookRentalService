using BookRentalService.Models;
using BookRentalService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRentalService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        // GET: api/book/search?name=bookName&genre=genreName
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string name, [FromQuery] string genre)
        {
            try
            {
                var books = await _bookService.SearchBooksAsync(name, genre);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for books");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/book/rent
        [HttpPost("rent")]
        public async Task<IActionResult> RentBook([FromBody] RentBookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _bookService.RentBookAsync(request.BookId, request.UserId);
                return success ? Ok("Book rented successfully") : BadRequest("Failed to rent the book");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while renting book");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/book/return
        [HttpPut("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _bookService.ReturnBookAsync(request.RentalId);
                return success ? Ok("Book returned successfully") : BadRequest("Failed to return the book");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while returning book");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}
