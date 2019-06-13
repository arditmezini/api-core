using System.Collections.Generic;
using System.Linq;
using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;

namespace AspNetCoreApi.Service.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork uow;

        public PublisherService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return uow.Publishers.GetAll().ToList();
        }
    }
}