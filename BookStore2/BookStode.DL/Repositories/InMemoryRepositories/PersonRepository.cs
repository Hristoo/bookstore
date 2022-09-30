using BookStode.DL.interfaces;
using BookStore.Models.Models;

namespace BookStode.DL.Repositories.InMemoryRepositories
{
    public class PersonRepository : IPersonRepository
    {
        private static List<Person> _persons = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "Ivan"
            },
            new Person()
            {
                Id = 2,
                Name = "Petko"
            },
            new Person()
            {
                Id = 3,
                Name = "Kerana"
            }
        };

        public Guid Id { get; set; }

        public PersonRepository()
        {
            Id = Guid.NewGuid();
        }
        public IEnumerable<Person> GetAllPersons()
        {
            return _persons;
        }

        public Person? GetById(int id)
        {
            return _persons.FirstOrDefault(x => x.Id == id);
        }

        public Person? AddPerson(Person user)
        {
            try
            {
                _persons.Add(user);

            }
            catch (Exception e)
            {
                return null;
            }
            return user;
        }

        public Person UpdatePerson(Person user)
        {
            var existingUser = _persons.FirstOrDefault(x => x.Id == user.Id);

            if (existingUser == null) return null;

            _persons.Remove(existingUser);

            _persons.Add(user);

            return user;
        }

        public Person? DeletePerson(int userId)
        {
            var person = _persons.FirstOrDefault(x => x.Id == userId);

            _persons.Remove(person);

            return person;
        }

        public Guid GetGuid()
        {
            return Id;
        }
    }
}