using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore2.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController
    {

        private readonly IBookService _bookServise;
        private readonly ILogger<BookService> _logger;

        public BookController(ILogger<BookService> logger, IBookService bookServise)
        {
            _logger = logger;
            _bookServise = bookServise;

        }


        [HttpGet(nameof(Get))]

        public IEnumerable<Book> Get()
        {
            return _bookServise.GetAllBooks();
        }

        [HttpGet(nameof(GetById))]
        public Book? GetById(int id)
        {
            var book = _bookServise.GetById(id);

            return book;
        }


        [HttpPost(nameof(Add))]
        public bool Add(Book book)
        {
            _bookServise.AddBook(book);
            return true;
        }

        [HttpPost(nameof(DeleteBook))]
        public Book DeleteBook(int bookId)
        {
            var book = _bookServise.GetById(bookId);

            _bookServise.DeleteBook(bookId);

            return book;
        }

    }
}
