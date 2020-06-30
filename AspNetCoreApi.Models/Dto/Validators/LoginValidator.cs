using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
               .PropertyNotEmpty()
               .PropertyEmailAddress();

            RuleFor(x => x.Password)
                .PropertyNotEmpty()
                .PropertyLength(6, 100);
        }
    }
}