using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
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

        public async Task<List<Statistics>> GetStatistics()
        {
            return await uow.Statistics.GetStatistics();
        }
    }
}