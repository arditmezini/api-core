using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Core
{
    public class GeneralDataRepository : IGeneralDataRepository
    {
        private readonly ApiContext _context;

        public GeneralDataRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Countries> GetCountries()
        {
            return _context.Countries;
        }
    }
}
