using BookStode.DL.interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService : IPersonRepository
    {
        Person? AddPerson(Person user);

        Person? DeletePerson(int userId);

        IEnumerable<Person> GetAllPersons();

        Person? GetById(int id);

        Person UpdatePerson(Person user);

        Guid GetGuid();
    }
}
