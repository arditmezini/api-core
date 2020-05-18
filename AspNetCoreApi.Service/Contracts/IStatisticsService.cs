using AspNetCoreApi.Dal.Entities;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IStatisticsService
    {
        Task<Statistics> GetStatistics();
    }
}