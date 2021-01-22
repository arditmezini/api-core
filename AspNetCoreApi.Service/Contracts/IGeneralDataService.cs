using AspNetCoreApi.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IGeneralDataService
    {
        Task<IEnumerable<Countries>> GetCountries();
    }
}