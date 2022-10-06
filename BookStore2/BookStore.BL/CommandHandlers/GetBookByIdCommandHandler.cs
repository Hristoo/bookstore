using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class GetBookByIdCommandHandler : IRequestHandler<GetBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
        {
           return await _bookRepository.GetById(request.bookId);
        }
    }
}
