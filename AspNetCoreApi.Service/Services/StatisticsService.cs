using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork uow;

        public StatisticsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<Statistics> GetStatistics()
        {
            return await uow.StatisticsRepository.GetStatistics();
        }
    }
}