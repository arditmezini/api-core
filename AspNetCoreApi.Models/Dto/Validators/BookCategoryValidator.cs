using FluentValidation;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class BookCategoryValidator : AbstractValidator<BookCategoryDto>
    {
        public BookCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Book category name cannot be blank.")
                .Length(3, 50).WithMessage("Book category name length should be between 3 and 50 chars");
        }
    }
}
