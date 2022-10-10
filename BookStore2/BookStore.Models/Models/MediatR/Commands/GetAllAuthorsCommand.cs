using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record GetAllAuthorsCommand : IRequest<IEnumerable<Author>>
    {
    }
}
