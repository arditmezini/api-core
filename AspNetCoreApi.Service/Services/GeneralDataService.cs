using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class GeneralDataService : IGeneralDataService
    {
        private readonly IUnitOfWork uow;

        public GeneralDataService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Countries>> GetCountries()
        {
            return await uow.GeneralData.GetCountries();
        }
    }
}