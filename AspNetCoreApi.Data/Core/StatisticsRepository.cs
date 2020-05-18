using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public readonly ApiContext _context;

        public StatisticsRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Statistics> GetStatistics()
        {
            return await _context.Statistics.SingleOrDefaultAsync();
        }
    }
}
