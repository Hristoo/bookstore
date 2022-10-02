using BookStore.Models.Models;

namespace BookStode.DL.interfaces
{
    public interface IPersonRepository
    {
        Person? AddPerson(Person person);

        Person? DeletePerson(int personId);

        IEnumerable<Person> GetAllPersons();

        Person? GetById(int id);

        Person UpdatePerson(Person person);

        Guid GetGuid();
    }
}