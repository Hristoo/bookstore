using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record GetAllBooksCommand :IRequest<IEnumerable<Book>>
    {
    }
}
