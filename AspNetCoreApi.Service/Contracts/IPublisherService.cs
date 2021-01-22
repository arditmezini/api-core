using AspNetCoreApi.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> GetAll();
        Task<Publisher> GetById(int id);
        Task<bool> Add(Publisher publisher);
        Task<bool> Update(int id, Publisher publisher);
        Task<bool> Delete(int id);
    }
}