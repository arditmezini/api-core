using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class BookCategoryValidator : AbstractValidator<BookCategoryDto>
    {
        public BookCategoryValidator()
        {
            RuleFor(x => x.Name)
                .PropertyNotEmpty()
                .PropertyLength(3, 50);
        }
    }
}