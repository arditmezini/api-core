using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IGeneralDataService
    {
        IEnumerable<Countries> GetCountries();
    }
}