using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IBookService
    {
        AddBookResponse? AddBook(AddBookRequest bookRequest);
        Book? DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        AddBookResponse UpdateBook(AddBookRequest bookRequest);
    }
}
