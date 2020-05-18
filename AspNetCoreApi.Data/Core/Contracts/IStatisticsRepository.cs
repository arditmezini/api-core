using AspNetCoreApi.Dal.Entities;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IStatisticsRepository
    {
        Task<Statistics> GetStatistics();
    }
}