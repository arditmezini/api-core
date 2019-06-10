using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service
{
    public class GeneralDataService : IGeneralDataService
    {
        private readonly IUnitOfWork<ApiContext> uow;

        public GeneralDataService(IUnitOfWork<ApiContext> uow)
        {
            this.uow = uow;
        }

        public IEnumerable<CountriesDto> GetCountries()
        {
            return uow.GetRepository<Countries>().Get().Select(x => new CountriesDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
