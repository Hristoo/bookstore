using BookStode.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStode.DL.Repositories.InMemoryRepositories
{
    public class BookRepository
    {

        private static List<Book> _books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Title = "Pod igoto",
                AuthorId = 1,
            },
            new Book()
            {
                Id=2,
                Title  = "Geracite",
                AuthorId = 2,
            }
        };

        public Book? AddBook(Book book)
        {
            _books.Add(book);
            return book;
        }

        public Book? DeleteBook(int bookId)
        {
            var book = _books.FirstOrDefault(x => x.Id == bookId);

            _books.Remove(book);

            return book;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(x =>x.Id == id);
        }

        public Book? GetByTitle(string title)
        {
            return _books.FirstOrDefault(x => x.Title == title);
        }

        public Book UpdateBook(Book book)
        {
            _books.Add(book);
            return book;
        }
    }
}
