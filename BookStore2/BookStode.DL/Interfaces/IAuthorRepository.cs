using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IAuthorRepository
    {
        Author? AddAuthor(Author autor);
        Author? DeleteAutor(int authorId);
        IEnumerable<Author> GetAllAuthors();
        Author? GetById(int id);
        Author GetAuthorByName(Author autor);
        Author? GetAuthorByName(string name);

        Author UpdateAuthor(Author autor);

    }
}