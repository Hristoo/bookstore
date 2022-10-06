using System.Net;
using AutoMapper;
using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        public readonly IAuthorRepository _authorRepository; 
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, ILogger<AuthorService> logger, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
            _bookRepository = bookRepository;
        }

        public async Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
                var auth =  await _authorRepository.GetAuthorByName(authorRequest.Name);

                if (auth != null)

                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                    };

                var author = _mapper.Map<Author>(authorRequest);
                var result = await _authorRepository.AddAuthor(author);

                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }

        public async Task<UpdateAuthorResponse> UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            var auth = await _authorRepository.GetAuthorByName(authorRequest.Name);

            if (auth == null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author not exist"
                };

            var author = _mapper.Map<Author>(authorRequest);
            var result = await _authorRepository.UpdateAuthor(author);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Author updated",
                Author = result
            };
        }

        public async Task<Author?> DeleteAutor(int authorId)
        {
            var author = await _authorRepository.GetById(authorId);
            var isAuthorHaveBook = await _bookRepository.GetBookByAuthorId(authorId);

            if (isAuthorHaveBook != null || author == null)
            {
                var e = new Exception("The author can't be deleted");

                return new Author();
            }
            await _authorRepository.DeleteAutor(authorId);

            return author;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _authorRepository.GetAllAuthors();
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _authorRepository.GetAuthorByName(name);
        }

        public async Task<Author?> GetById(int id)
        {
            return await _authorRepository.GetById(id);
        }


    }
}
