using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{

    public class GetBookByAuthorIdComandHandler : IRequestHandler<GetBookByAuthorIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByAuthorIdComandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByAuthorIdCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookByAuthorId(request.authorId);
            return book;
        }
    }
}
