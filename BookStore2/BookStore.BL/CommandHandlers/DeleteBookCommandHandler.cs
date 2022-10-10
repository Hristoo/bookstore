using BookStode.DL.Interfaces;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using MediatR;

namespace BookStore.BL.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookComand, Book>
    {
        private readonly IBookService _bookService;

        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<Book> Handle(DeleteBookComand request, CancellationToken cancellationToken)
        {
            return await _bookService.DeleteBook(request.bookId);
        }
    }
}
