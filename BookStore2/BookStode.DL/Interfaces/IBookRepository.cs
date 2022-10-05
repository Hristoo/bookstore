using BookStore.Models.Models;

namespace BookStode.DL.Interfaces
{
    public interface IBookRepository
    {
        public Task<Book?> AddBook(Book book);
        public Task<Book?> DeleteBook(int bookId);
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book?> GetById(int id);
        public Task<Book?> GetByTitle(string title);
        public Task<Book> UpdateBook(Book book);
        public Task<Book> GetBookByAuthorId(int authorId);
    }
}
