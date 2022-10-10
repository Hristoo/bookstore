using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    internal class GetAllAuthorsCommandHandler : IRequestHandler<GetAllAuthorsCommand, IEnumerable<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
        {
            return await _authorRepository.GetAllAuthors();
        }
    }
}
