using System.Net;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(GetAllAuthors))]

        public async Task<IActionResult> GetAllAuthors()
        {
            _logger.LogInformation("");
            return Ok(await _authorService.GetAllAuthors());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]

        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = await _authorService.AddAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]

        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest updateAuthorRequest, int id)
        {
            var result = await _authorService.UpdateAuthor(updateAuthorRequest);

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
        public async Task<Author?> GetById(int id)
        {
            var user = await _authorService.GetById(id);
            return user;
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
        public async Task<Person> DeleteAuthor(int authorId)
        {
            var author = await _authorService.DeleteAutor(authorId);

            _authorService.DeleteAutor(authorId);

            return author;
        }

        [HttpGet(nameof(GetAuthorByName))]
        public async Task<Author?> GetAuthorByName(string name)
        {
            var author = await _authorService.GetAuthorByName(name);
            return author;
        }

    }
}
