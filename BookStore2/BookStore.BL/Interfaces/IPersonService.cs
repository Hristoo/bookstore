using BookStode.DL.interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService : IPersonRepository
    {
        Person? AddPerson(Person person);

        Person? DeletePerson(int personId);

        IEnumerable<Person> GetAllPersons();

        Person? GetById(int id);

        Person UpdatePerson(Person person);

        Guid GetGuid();
    }
}
