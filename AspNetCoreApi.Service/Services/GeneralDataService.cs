using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
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

        public IEnumerable<Countries> GetCountries()
        {
            return uow.GeneralData.GetCountries().ToList();
        }
    }
}