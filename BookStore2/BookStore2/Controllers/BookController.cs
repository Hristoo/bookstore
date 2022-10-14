using System.Net;
using BookStore.BL.Interfaces;
using BookStore.BL.Kafka;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookService _bookServise;
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;
        private Consumer<int, Book> _consumer;

        public BookController(ILogger<BookController> logger, IBookService bookServise, IMediator mediator, Consumer<int, Book> consumer)
        {
            _logger = logger;
            _bookServise = bookServise;
            _mediator = mediator;
            _consumer = consumer;
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpGet(nameof(GetAllBooks))]
        public async Task<IActionResult> GetAllBooks()
        {
            //return Ok(await _mediator.Send(new GetAllBooksCommand()));
            return Ok(_consumer._cache._dictionary);
        }

        [HttpGet(nameof(GetById))]
        public async Task<Book?> GetById(int id)
        {
            //var book = await _bookServise.GetById(id);
            var book = await _mediator.Send(new GetBookByIdCommand(id));

            return book;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddBook))]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest bookRequest)
        {
            //var result = await _bookServise.AddBook(bookRequest);
            var result = await _mediator.Send(new AddBookCommand(bookRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateBook))]
        public async Task<IActionResult> UpdateBook([FromBody] AddBookRequest bookRequest)
        {
            //var result = await _bookServise.UpdateBook(bookRequest);
            var result = await _mediator.Send(new UpdateBookCommand(bookRequest));

            if (result != null)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost(nameof(DeleteBook))]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var result = await _mediator.Send(new DeleteBookComand(bookId));

            if (result == null)
            {
                return NotFound("Book not found");
            }

            return result != null ? Ok(result) : StatusCode(500);
        }
    }
}
