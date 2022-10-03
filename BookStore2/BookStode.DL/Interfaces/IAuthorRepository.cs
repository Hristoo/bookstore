using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IAuthorRepository
    {
        Guid Id { get; set; }

        Author? AddAuthor(Author autor);
        Author? DeleteAutor(int authorId);
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Person UpdateAuthor(Author autor);
    }
}