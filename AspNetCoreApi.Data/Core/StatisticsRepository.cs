using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<Statistics>> GetStatistics()
        {
            return await _context.Statistics.ToListAsync();
        }
    }
}