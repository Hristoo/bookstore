using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public AddAuthorResponse? AddAuthor(AddAuthorRequest autor);

        public Author? DeleteAutor(int authorId);

        public IEnumerable<Author> GetAllAuthors();

        public Author? GetById(int id);

        public UpdateAuthorResponse UpdateAuthor(UpdateAuthorRequest updateAuthorRequest);

        public Author GetAuthorByName(string Name);


    }
}
