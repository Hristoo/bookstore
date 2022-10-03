using BookStode.DL.interfaces;
using BookStore.Models.Models;

namespace BookStode.DL.Repositories.InMemoryRepositories
{
    public class PersonRepository : IPersonRepository
    {
        public static List<Person> _persons = new List<Person>()
        {
            new Person()
            {
                Id = 1,
                Name = "Ivan",
                Age = 25,
                DateOfBirth = DateTime.Now
,            },
            new Person()
            {
                Id = 2,
                Name = "Petko",
                Age= 29,
                DateOfBirth = DateTime.Now
            },
            new Person()
            {
                Id = 3,
                Name = "Kerana",
                Age = 32,
                DateOfBirth = DateTime.Now
            }
        };

        public IEnumerable<Person> GetAllPersons()
        {
            return _persons;
        }

        public Person? GetById(int id)
        {
            return _persons.FirstOrDefault(x => x.Id == id);
        }

        public Person? AddPerson(Person person)
        {
            try
            {
                _persons.Add(person);

            }
            catch (Exception e)
            {
                return null;
            }
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            var existingUser = _persons.FirstOrDefault(x => x.Id == person.Id);

            if (existingUser == null) return null;

            _persons.Remove(existingUser);

            _persons.Add(person);

            return person;
        }

        public Person? DeletePerson(int personId)
        {
            var person = _persons.FirstOrDefault(x => x.Id == personId);

            _persons.Remove(person);

            return person;
        }

     
    }
}