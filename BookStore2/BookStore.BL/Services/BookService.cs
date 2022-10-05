using System.Net;
using AutoMapper;
using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        public readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;


        public BookService(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        public async Task<AddBookResponse> AddBook(AddBookRequest bookRequest)
        {
            var bookTitle = await _bookRepository.GetByTitle(bookRequest.Title);
            var author = await _authorRepository.GetById(bookRequest.AuthorId);

            if (author == null)
            {
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "The author don't exist"
                };
            }

            if (bookTitle != null)
            {
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "The book already exist"
                };
            }


            var book = _mapper.Map<Book>(bookRequest);
            var result = await _bookRepository.AddBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }

        public async Task<AddBookResponse> UpdateBook(AddBookRequest bookRequest)
        {
            var bookTitle = await _bookRepository.GetByTitle(bookRequest.Title);

            if (bookTitle == null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "The book not exist"
                };

            var book = _mapper.Map<Book>(bookRequest);
            var result = await _bookRepository.UpdateBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }


        public async Task<Book?> DeleteBook(int bookId)
        {
            var book = await _bookRepository.GetById(bookId);

            _bookRepository.DeleteBook(bookId);

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<Book> GetBookByAuthorId(int id)
        {
            var book = await _bookRepository.GetBookByAuthorId(id);
            return book;
        }

    }
}
