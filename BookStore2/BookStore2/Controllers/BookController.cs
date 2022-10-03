using System.Net;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {

        private readonly IBookService _bookServise;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IBookService bookServise)
        {
            _logger = logger;
            _bookServise = bookServise;

        }


        [HttpGet(nameof(Get))]

        public IEnumerable<Book> Get()
        {
            return _bookServise.GetAllBooks();
        }

        [HttpGet(nameof(GetById))]
        public Book? GetById(int id)
        {
            var book = _bookServise.GetById(id);

            return book;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBook))]

        public IActionResult AddBook([FromBody] AddBookRequest bookRequest)
        {
            var result = _bookServise.AddBook(bookRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateBook))]

        public IActionResult UpdateBook([FromBody] AddBookRequest bookRequest)
        {
            var result = _bookServise.UpdateBook(bookRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(DeleteBook))]
        public Book DeleteBook(int bookId)
        {
            var book = _bookServise.GetById(bookId);

            _bookServise.DeleteBook(bookId);

            return book;
        }

    }
}
