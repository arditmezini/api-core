using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorDto>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName)
                .PropertyNotEmpty()
                .PropertyLength(3, 50)
                .PropertyAllLetter();

            RuleFor(x => x.LastName)
                .PropertyNotEmpty()
                .PropertyLength(3, 50);

            RuleFor(x => x.AuthorContact)
                .NotNull();
        }
    }
}