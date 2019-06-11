using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service.Services
{
    public class GeneralDataService : IGeneralDataService
    {
        private readonly IUnitOfWork uow;

        public GeneralDataService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<CountriesDto> GetCountries()
        {
            return uow.GeneralData.GetCountries().Select(x => new CountriesDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
