using FluentValidation;
using System;

namespace AspNetCoreApi.Models.Dto.Validators
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be blank.")
                .Length(1, 100).WithMessage("Title length should be between 1 and 100 chars");

            RuleFor(x => x.Isbn)
                .NotEmpty().WithMessage("Isbn cannot be blank.")
                .Length(10, 13).WithMessage("Isbn length should be between 10 and 13 chars");

            RuleFor(x => x.PublishedYear)
                .NotEmpty().WithMessage("PublishedYear cannot be blank.")
                .InclusiveBetween(0, DateTime.Now.Year).WithMessage($"PublishedYear should be between 0 and {DateTime.Now.Year}");
        }
    }
}