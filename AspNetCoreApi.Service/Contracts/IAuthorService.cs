using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> Get();
    }
}
