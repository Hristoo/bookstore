using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record GetAuthorByNameCommand(string name) : IRequest<Author>
    {
    }
}
