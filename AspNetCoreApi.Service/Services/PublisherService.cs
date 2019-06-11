using System.Collections.Generic;
using System.Linq;
using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Dto;
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

        public IEnumerable<PublisherDto> GetAll()
        {
            return uow.Publishers.GetAll().Select(x => new PublisherDto
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.Country
            }).ToList();
        }
    }
}
