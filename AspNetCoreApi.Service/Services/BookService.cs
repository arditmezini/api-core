using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
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

        public IEnumerable<Book> GetAll()
        {
            return uow.Books.GetAll().ToList();
        }
    }
}