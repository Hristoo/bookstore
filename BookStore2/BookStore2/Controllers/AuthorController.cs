using System.Net;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMediator _mediator;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService, IMediator mediator)
        {
            _logger = logger;
            _authorService = authorService;
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(GetAllAuthors))]

        public async Task<IActionResult> GetAllAuthors()
        {
            _logger.LogInformation("");
            return Ok(await _mediator.Send(new GetAllAuthorsCommand()));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]

        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            //var result = await _authorService.AddAuthor(authorRequest);
            var result = await _mediator.Send(new AddAuthorCommand(authorRequest));

            if (result == null)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]

        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest updateAuthorRequest)
        {
            //var result = await _authorService.UpdateAuthor(updateAuthorRequest);
            var result = await _mediator.Send(new UpdateAuthorCommand(updateAuthorRequest));

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(GetAuthorById))]

        public async Task<IActionResult> GetAuthorById(int id)
        {
            if (id <= 0) return BadRequest($"Id must be greater");
            
            var result = await _authorService.GetById(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpGet(nameof(GetById))]
        public async Task<IActionResult?> GetById(int id)
        {

            //var author = await _authorService.GetById(id);
            var author = await _mediator.Send(new GetAuthorByIdCommand(id));

            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost(nameof(Add))]
        public async Task<IActionResult> Add(AddAuthorRequest author)
        {
            var result = await _authorService.AddAuthor(author);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost(nameof(DeleteAuthor))]
        public async Task<IActionResult> DeleteAuthor(int authorId)
        {
            //var author = await _authorService.DeleteAutor(authorId);

            var author = await _mediator.Send(new DeleteAuthorCommand(authorId));

            if (author == null || author.Id == 0)
            {
                return BadRequest(author);
            }

            return Ok(author);
        }

        [HttpGet(nameof(GetAuthorByName))]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            //var author = await _authorService.GetAuthorByName(name);
            var author = await _mediator.Send(new GetAuthorByNameCommand(name));

            if (author == null)
            {
                return BadRequest(author);
            }
            return Ok(author);
        }

    }
}
