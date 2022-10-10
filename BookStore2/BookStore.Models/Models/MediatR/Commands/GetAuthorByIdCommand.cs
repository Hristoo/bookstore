using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record GetAuthorByIdCommand(int id) : IRequest<Author>
    {
    }
}
