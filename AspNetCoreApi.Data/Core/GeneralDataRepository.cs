using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class GeneralDataRepository : IGeneralDataRepository
    {
        private readonly ApiContext _context;

        public GeneralDataRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Countries>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }
    }
}
