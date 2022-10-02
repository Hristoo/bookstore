using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        public readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public Book? AddBook(Book book)
        {
            _bookRepository.AddBook(book);
            return book;
        }

        public Book? DeleteBook(int bookId)
        {
            var book = _bookRepository.GetById(bookId);

            _bookRepository.DeleteBook(bookId);

            return book;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book? GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public Book UpdateBook(Book book)
        {
            _bookRepository.UpdateBook(book);
            return book;
        }
    }
}
