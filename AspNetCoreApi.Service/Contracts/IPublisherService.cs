using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> GetAll();
    }
}
