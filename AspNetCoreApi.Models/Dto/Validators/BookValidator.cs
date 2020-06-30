using FluentValidation;
using System;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .PropertyNotEmpty()
                .PropertyLength(1, 100);

            RuleFor(x => x.Isbn)
                .PropertyNotEmpty()
                .PropertyLength(10, 13);

            RuleFor(x => x.PublishedYear)
                .PropertyNotEmpty()
                .PropertyInclusiveBetween(0, DateTime.Now.Year);
        }
    }
}