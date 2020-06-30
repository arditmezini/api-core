using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class PublisherValidator : AbstractValidator<PublisherDto>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Name)
                .PropertyNotEmpty()
                .PropertyLength(3, 100);
        }
    }
}