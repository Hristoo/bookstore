using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger, IAuthorRepository authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
        }

        [HttpGet(nameof(Get))]

        public IEnumerable<Author> Get()
        {
            return _authorRepository.GetAllAuthors();
        }

        [HttpGet(nameof(GetById))]
        public Author? GetById(int id)
        {
            var user = _authorRepository.GetById(id);
            return user;
        }

        [HttpPost(nameof(Add))]
        public bool Add(Author user)
        {
            _authorRepository.AddAuthor(user);
            return true;
        }

        [HttpGet(nameof(GetGuid))]

        public Guid GetGuid()
        {
            return _authorRepository.GetGuid();
        }

    }
}
