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

        private readonly IMapper _mapper;

        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
                //throw new Exception();

                var auth = _authorRepository.GetAuthorByName(authorRequest.Name);

                if (auth != null)

                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                    };

                var author = _mapper.Map<Author>(authorRequest);
                var result = _authorRepository.AddAuthor(author);

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

        public UpdateAuthorResponse UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            var auth = _authorRepository.GetAuthorByName(authorRequest.Name);

            if (auth == null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author not exist"
                };

            var author = _mapper.Map<Author>(authorRequest);
            var result = _authorRepository.UpdateAuthor(author);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Author = result
            };
        }

        public Author? DeleteAutor(int authorId)
        {
            var author = _authorRepository.GetById(authorId);

            _authorRepository.DeleteAutor(authorId);

            return author;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _authorRepository.GetAllAuthors();
        }

        public Author? GetAuthorByName(string name)
        {
            return _authorRepository.GetAuthorByName(name);
        }

        public Author? GetById(int id)
        {
            return _authorRepository.GetById(id);
        }


    }
}
