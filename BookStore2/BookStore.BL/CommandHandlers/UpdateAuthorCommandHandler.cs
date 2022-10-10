using BookStore.Models.Models;
using System.Net;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.Responses;
using MediatR;
using BookStode.DL.Interfaces;
using AutoMapper;

namespace BookStore.BL.CommandHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorResponse>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }


        public async  Task<UpdateAuthorResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var auth = await _authorRepository.GetAuthorByName(request.authorRequest.Name);

            if (auth == null)
                return new UpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Author not exist"
                };

            var author = _mapper.Map<Author>(request.authorRequest);
            var result = await _authorRepository.UpdateAuthor(author);

            return new UpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Author updated",
                Author = result
            };
        }
    }
}
