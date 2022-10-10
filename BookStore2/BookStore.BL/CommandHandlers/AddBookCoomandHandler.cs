using BookStore.BL.Interfaces;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.Responses;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    internal class AddBookCoomandHandler : IRequestHandler<AddBookCommand, AddBookResponse>
    {
        private readonly IBookService _bookService;

        public AddBookCoomandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<AddBookResponse> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookService.AddBook(request.addBookRequest);
        }
    }
}
