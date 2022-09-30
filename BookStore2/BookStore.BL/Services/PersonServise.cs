using BookStode.DL.interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL
{
    public class PersonServise : IPersonService
    {
        public readonly IPersonRepository _personRepository;

        public PersonServise(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person? AddPerson(Person user)
        {
            _personRepository.AddPerson(user);
            return user;
        }

        public Person? DeletePerson(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAllPersons()
        {
           return _personRepository.GetAllPersons();
        }

        public IEnumerable<Person> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Person? GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Guid GetGuid()
        {
            throw new NotImplementedException();
        }
        public Person UpdatePerson(Person user)
        {
            throw new NotImplementedException();
        }

        public Person UpdateUser(Person user)
        {
            throw new NotImplementedException();
        }
    }
}