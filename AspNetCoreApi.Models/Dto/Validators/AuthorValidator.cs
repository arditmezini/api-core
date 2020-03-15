using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorDto>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName name cannot be blank.")
                .Length(3, 50).WithMessage("FirstName cannot be more than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName name cannot be blank.")
                .Length(3, 50).WithMessage("LastName cannot be more than 50 characters.");

            RuleFor(x => x.AuthorContact)
                .NotNull();
        }
    }
}