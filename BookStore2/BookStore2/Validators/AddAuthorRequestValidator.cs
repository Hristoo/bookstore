using BookStore.Models.Models.Requests;
using FluentValidation;

namespace BookStore2.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0)
                .WithMessage("The age must be greater '0'")
                .LessThanOrEqualTo(120)
                .WithMessage("The age must be smaller '120'");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            When(x => !string.IsNullOrEmpty(x.NickName), () =>
            {
                RuleFor(x => x.NickName).MinimumLength(2).MaximumLength(50);
            });
            RuleFor(x => x.DateOfBirth)
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.MaxValue);
        }
    }
}
