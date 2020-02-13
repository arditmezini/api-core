using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork uow;

        public AuthorService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Author>> Get()
        {
            return await uow.Authors.GetAuthors();
        }
    }
}