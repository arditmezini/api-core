using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorDto>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName cannot be blank.")
                .Length(3, 50).WithMessage("FirstName length should be between 3 and 50 chars");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName cannot be blank.")
                .Length(3, 50).WithMessage("LastName length should be between 3 and 50 chars");

            RuleFor(x => x.AuthorContact)
                .NotNull();
        }
    }
}