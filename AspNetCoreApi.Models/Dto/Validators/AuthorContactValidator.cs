using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class AuthorContactValidator : AbstractValidator<AuthorContactDto>
    {
        public AuthorContactValidator()
        {
            RuleFor(x => x.ContactNumber)
                .PropertyNotEmpty()
                .PropertyLength(1, 15);

            RuleFor(x => x.Address)
                .PropertyNotEmpty()
                .PropertyLength(1, 100);
        }
    }
}