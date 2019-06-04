using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service
{
    public class AuthorService
    {
        private readonly IUnitOfWork<ApiContext> uow;

        public AuthorService(IUnitOfWork<ApiContext> uow)
        {
            this.uow = uow;
        }

        public IEnumerable<Author> Get()
        {
            return uow.GetRepository<Author>().Get().ToList();
        }
    }
}
