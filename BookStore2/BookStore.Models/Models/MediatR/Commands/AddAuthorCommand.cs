using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record AddAuthorCommand(AddAuthorRequest addAuthorRequest) : IRequest<AddAuthorResponse>
    {
    }
}
