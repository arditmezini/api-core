using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork<ApiContext> uow;

        public BookService(IUnitOfWork<ApiContext> uow)
        {
            this.uow = uow;
        }

        public IEnumerable<BookDto> Get()
        {
            return uow.GetRepository<Book>().Get().Select(x => new BookDto {
                Id = x.Id,
                Title = x.Title,
                //Category = new BookCategoryDto
                //{
                //    Id = x.Category.Id,
                //    Name = x.Category.Name
                //},
                //Publisher = new PublisherDto
                //{
                //    Id = x.Publisher.Id,
                //    Name = x.Publisher.Name
                //}
            }).ToList();
        }
    }
}
