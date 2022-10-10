using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    internal class GetAuthorByIdCommandHandler : IRequestHandler<GetAuthorByIdCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetById(request.id);
        }
    }
}
