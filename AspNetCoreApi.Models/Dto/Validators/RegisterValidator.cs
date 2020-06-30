using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Role)
                .PropertyNotEmpty();

            RuleFor(x => x.FirstName)
                .PropertyNotEmpty()
                .PropertyLength(3, 50);

            RuleFor(x => x.LastName)
                .PropertyNotEmpty()
                .PropertyLength(3, 50);

            RuleFor(x => x.Email)
                .PropertyNotEmpty()
                .PropertyEmailAddress();

            RuleFor(x => x.Password)
                .PropertyNotEmpty()
                .PropertyLength(6, 100);
        }
    }
}