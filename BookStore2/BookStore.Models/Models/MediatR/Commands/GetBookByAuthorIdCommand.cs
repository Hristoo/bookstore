using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record GetBookByAuthorIdCommand(int authorId) : IRequest<Book>
    {
    }
}
