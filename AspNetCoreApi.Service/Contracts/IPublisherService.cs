using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IPublisherService
    {
        IEnumerable<Publisher> GetAll();
    }
}
