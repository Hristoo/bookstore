using System.Net;
using AutoMapper;
using BookStode.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Models.MediatR.Commands;
using BookStore.Models.Models.Requests;
using BookStore.Models.Models.Responses;
using MediatR;


namespace BookStore.BL.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, AddBookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<AddBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookTitle = await _bookRepository.GetByTitle(request.bookRequest.Title);

            if (bookTitle == null)
                return new AddBookResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "The book not exist"
                };

            var book = _mapper.Map<Book>(request.bookRequest);
            var result = await _bookRepository.UpdateBook(book);

            return new AddBookResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
                Book = result
            };
        }


    }
}
