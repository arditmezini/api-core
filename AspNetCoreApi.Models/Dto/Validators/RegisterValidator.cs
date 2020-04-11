using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role cannot be blank.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName cannot be blank.")
                .Length(3, 50).WithMessage("FirstName length should be between 3 and 50 chars");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName cannot be blank.")
                .Length(3, 50).WithMessage("LastName length should be between 3 and 50 chars");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be blank.")
                .EmailAddress().WithMessage("Email is not in valid format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be blank.")
                .Length(6, 100).WithMessage("Password length should be between 6 and 100 chars");
        }
    }
}
