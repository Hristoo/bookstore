using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        Person? AddPerson(Person person);

        Person? DeletePerson(int personId);

        IEnumerable<Person> GetAllPersons();

        Person? GetById(int id);

        Person UpdatePerson(Person person);

    }
}
