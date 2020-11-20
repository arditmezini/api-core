using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class NewsValidator : AbstractValidator<NewsDto>
    {
        public NewsValidator()
        {
            RuleFor(x => x.Title)
                .PropertyNotEmpty()
                .PropertyLength(1, 100);

            RuleFor(x => x.Description)
                .PropertyNotEmpty()
                .PropertyLength(1, 500);
        }
    }
}