using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class PublisherValidator : AbstractValidator<PublisherDto>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be blank.")
                .Length(3, 100).WithMessage("Name length should be between 3 and 100 chars");
        }
    }
}
