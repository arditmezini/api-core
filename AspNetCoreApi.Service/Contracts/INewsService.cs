using AspNetCoreApi.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAll();
        Task<bool> Add(News news);
    }
}