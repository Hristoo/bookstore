using BookStode.DL.interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL
{
    public class PersonService : IPersonService
    {
        public readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person? AddPerson(Person person)
        {
            _personRepository.AddPerson(person);
            return person;
        }

        public Person? DeletePerson(int personId)
        {
            var person = _personRepository.GetById(personId);

            _personRepository.DeletePerson(personId);

            return person;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return _personRepository.GetAllPersons();
        }

        public Person? GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public Person UpdatePerson(Person person)
        {
            return _personRepository.UpdatePerson(person);
        }
    }
}