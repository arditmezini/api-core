using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {
        Task<IEnumerable<Author>> GetAuthors();
    }
}