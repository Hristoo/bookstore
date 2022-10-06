using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record DeleteBookComand(int bookId) : IRequest<Book>
    {
    }
}
