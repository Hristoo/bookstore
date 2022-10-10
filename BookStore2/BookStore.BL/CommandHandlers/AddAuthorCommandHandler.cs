using System.Net;
using AutoMapper;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookStore.BL.CommandHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            
                var auth = await _authorRepository.GetAuthorByName(request.addAuthorRequest.Name);

                if (auth != null)

                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                    };

                var author = _mapper.Map<Author>(request.addAuthorRequest);
                var result = await _authorRepository.AddAuthor(author);

                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result
                };
            
           
        }
    }
}
