using AspNetCoreApi.Models.Dto;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IPublisherService
    {
        IEnumerable<PublisherDto> GetAll();
    }
}
