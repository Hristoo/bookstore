using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorService> _logger;

        public AuthorController(ILogger<AuthorService> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet(nameof(Get))]

        public IEnumerable<Author> Get()
        {
            return _authorService.GetAllAuthors();
        }

        [HttpGet(nameof(GetById))]
        public Author? GetById(int id)
        {
            var user = _authorService.GetById(id);
            return user;
        }

        [HttpPost(nameof(Add))]
        public bool Add(Author author)
        {
            _authorService.AddAuthor(author);
            return true;
        }

        [HttpPost(nameof(DeleteAuthor))]
        public Person DeleteAuthor(int authorId)
        {
            var author = _authorService.GetById(authorId);

            _authorService.DeleteAutor(authorId);

            return author;
        }


    }
}
