using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<Author?> AddAuthor(Author autor);
        public Task<Author?> DeleteAutor(int authorId);
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<Author?> GetById(int id);
        public Task<Author> GetAuthorByName(string name);
        public Task<Author> UpdateAuthor(Author autor);
    }
}