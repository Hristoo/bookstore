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
        [HttpPost(nameof(Get))]

        public IActionResult Get()
        {
            _logger.LogInformation("");
            return Ok(_authorService.GetAllAuthors());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthor))]

        public IActionResult AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            var result = _authorService.AddAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(UpdateAuthor))]

        public IActionResult UpdateAuthor([FromBody] UpdateAuthorRequest updateAuthorRequest)
        {
            var result = _authorService.UpdateAuthor(updateAuthorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(GetAuthorById))]

        public IActionResult GetAuthorById(int id)
        {
            if (id <= 0) return BadRequest($"Id must be greater");
            
            var result = _authorService.GetById(id);

            if (result == null) return NotFound(id);

            return Ok(result);
        }

        [HttpGet(nameof(GetById))]
        public Author? GetById(int id)
        {
            var user = _authorService.GetById(id);
            return user;
        }

        [HttpPost(nameof(Add))]
        public IActionResult Add(AddAuthorRequest author)
        {
            var result = _authorService.AddAuthor(author);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost(nameof(DeleteAuthor))]
        public Person DeleteAuthor(int authorId)
        {
            var author = _authorService.GetById(authorId);

            _authorService.DeleteAutor(authorId);

            return author;
        }

        [HttpGet(nameof(GetAuthorByName))]
        public Author? GetAuthorByName(string name)
        {
            var author = _authorService.GetAuthorByName(name);
            return author;
        }

    }
}
