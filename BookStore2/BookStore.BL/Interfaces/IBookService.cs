using BookStode.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IBookService : IBookRepository
    {
        Book? AddBook(Book book);
        Book? DeleteBook(int bookId);
        IEnumerable<Book> GetAllBooks();
        Book? GetById(int id);
        Book UpdateBook(Book book);
    }
}
