using BookStore.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Contracts.Services.Data
{
    public interface IStatisticsService
    {
        Task<List<StatisticsResponse>> GetStatistics();
    }
}