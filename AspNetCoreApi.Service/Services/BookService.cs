using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork uow;

        public BookService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<BookDto> Get()
        {
            return uow.Books.GetAll().Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Category = new BookCategoryDto
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                Publisher = new PublisherDto
                {
                    Id = x.Publisher.Id,
                    Name = x.Publisher.Name
                }
            }).ToList();
        }
    }
}
