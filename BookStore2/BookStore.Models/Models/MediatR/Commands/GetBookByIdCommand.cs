using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record GetBookByIdCommand(int bookId) : IRequest<Book>
    {
    }
}
