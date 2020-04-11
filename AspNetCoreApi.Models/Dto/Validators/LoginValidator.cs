using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email cannot be blank.")
               .EmailAddress().WithMessage("Email is not in valid format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be blank.")
                .Length(6, 100).WithMessage("Password length should be between 6 and 100 chars");
        }
    }
}