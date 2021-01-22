using AspNetCoreApi.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface INewsRepository : IRepositoryBase<News>
    {
        Task<IEnumerable<News>> GetAll();
    }
}