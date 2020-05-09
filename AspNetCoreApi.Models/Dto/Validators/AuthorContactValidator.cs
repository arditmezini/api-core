using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class AuthorContactValidator : AbstractValidator<AuthorContactDto>
    {
        public AuthorContactValidator()
        {
            RuleFor(x => x.ContactNumber)
                .NotEmpty().WithMessage("Contact Number cannot be blank.")
                .Length(1, 15).WithMessage("Contact Number length should be between 1 and 15 chars");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be blank.")
                .Length(1, 100).WithMessage("Address length should be between 1 and 100 chars");
        }
    }
}