using BookStore.Models.Models;

namespace BookStode.DL.interfaces
{
    public interface IPersonRepository
    {
        Person? AddPerson(Person user);

        Person? DeletePerson(int userId);

        IEnumerable<Person> GetAllPersons();

        Person? GetById(int id);

        Person UpdatePerson(Person user);

        Guid GetGuid();
    }
}