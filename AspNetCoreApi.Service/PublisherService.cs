using System.Collections.Generic;
using System.Linq;
using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;

namespace AspNetCoreApi.Service
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork<ApiContext> uow;

        public PublisherService(IUnitOfWork<ApiContext> uow)
        {
            this.uow = uow;
        }

        public IEnumerable<PublisherDto> Get()
        {
            return uow.GetRepository<Publisher>().Get().Select(x => new PublisherDto
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.Country
            }).ToList();
        }
    }
}
