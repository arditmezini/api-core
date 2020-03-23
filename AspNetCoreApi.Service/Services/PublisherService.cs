using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork uow;

        public PublisherService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await uow.Publishers.GetAll();
        }
    }
}