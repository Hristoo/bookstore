using BookStore.Models.Models.Responses;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record DeleteAuthorCommand(int id) : IRequest<Author>
    {
    }
}
