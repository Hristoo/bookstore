using BookStore.Models.Models.Requests;
using FluentValidation;

namespace BookStore2.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Title)
                .MinimumLength(2);
        }
    }
}
