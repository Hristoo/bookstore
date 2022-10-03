using System.Net;
using AutoMapper;
using BookStode.DL.interfaces;
using BookStode.DL.Interfaces;
using BookStode.DL.Repositories.InMemoryRepositories;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        public readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;


        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public AddBookResponse AddBook(AddBookRequest bookRequest)
        {
            var bookTitle = _bookRepository.GetByTitle(bookRequest.Title);

            if (bookTitle != null)
            {
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "The book already exist"
                };
            }

            var book = _mapper.Map<Book>(bookRequest);
            var result = _bookRepository.AddBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }

        public AddBookResponse UpdateBook(AddBookRequest bookRequest)
        {
            var bookTitle = _bookRepository.GetByTitle(bookRequest.Title);

            if (bookTitle == null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "The book not exist"
                };

            var book = _mapper.Map<Book>(bookRequest);
            var result = _bookRepository.UpdateBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
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
