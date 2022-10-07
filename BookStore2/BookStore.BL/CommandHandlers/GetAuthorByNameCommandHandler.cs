using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class GetAuthorByNameCommandHandler : IRequestHandler<GetAuthorByNameCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByNameCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(GetAuthorByNameCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAuthorByName(request.name);
        }
    }
}
