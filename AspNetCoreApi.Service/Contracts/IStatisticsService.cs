using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IStatisticsService
    {
        Task<List<Statistics>> GetStatistics();
    }
}