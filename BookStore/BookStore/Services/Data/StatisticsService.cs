using BookStore.Constants;
using BookStore.Contracts.Repository;
using BookStore.Contracts.Services.Data;
using BookStore.Models.Response;
using System.Threading.Tasks;

namespace BookStore.Services.Data
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IGenericRepository _genericRepository;

        public StatisticsService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<StatisticsResponse> GetStatistics()
        {
            var response = await _genericRepository.Get<StatisticsResponse>(ApiConstants.StatisticsDashboard);
            return response;
        }
    }
}
