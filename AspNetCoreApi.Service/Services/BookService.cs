using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork uow;

        public BookService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await uow.Books.GetAll();
        }
    }
}