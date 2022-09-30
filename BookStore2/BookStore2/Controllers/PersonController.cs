using Microsoft.AspNetCore.Mvc;
using BookStode.DL.Repositories.InMemoryRepositories;
using BookStode.DL.interfaces;
using BookStore.Models.Models;
using BookStore.BL;
using BookStore.BL.Interfaces;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PersonController : ControllerBase
    {
        private readonly IPersonService  _personServise;
        private readonly ILogger<PersonServise> _logger;

        public PersonController(ILogger<PersonServise> logger, IPersonService personServise)
        {
            _logger = logger;
            _personServise = personServise;

        }


        [HttpGet(nameof(Get))]

        public IEnumerable<Person> Get()
        {   
            return _personServise.GetAllPersons();
        }

        [HttpGet(nameof(GetById))]
        public Person? GetById(int id)
        {
            var user = _personServise.GetById(id);
            return user;
        }


        [HttpPost(nameof(Add))]
        public bool Add(Person user)
        {
            _personServise.AddPerson(user);
            return true;
        }

        [HttpGet(nameof(GetGuid))]

        public Guid GetGuid()
        {
            return _personServise.GetGuid();
        }

    }
}
