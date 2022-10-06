using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;
using MediatR;

namespace BookStore.Models.Models.MediatR.Commands
{
    public record AddBookCommand(AddBookRequest addBookRequest) : IRequest<Responses.AddBookResponse> 
    {
    }
}
