using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        public Task<Models.Models.Responses.AddBookResponse?> AddBook(AddBookRequest bookRequest);
        public Task<Book?> DeleteBook(int bookId);
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book?> GetById(int id);
        public Task<Models.Models.Responses.AddBookResponse> UpdateBook(AddBookRequest bookRequest);
    }
}
