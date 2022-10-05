using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public Task<AddAuthorResponse?> AddAuthor(AddAuthorRequest autor);

        public Task<Author?> DeleteAutor(int authorId);

        public Task<IEnumerable<Author>> GetAllAuthors();

        public Task<Author?> GetById(int id);

        public Task<UpdateAuthorResponse> UpdateAuthor(UpdateAuthorRequest updateAuthorRequest);

        public Task<Author> GetAuthorByName(string Name);
     }
}
