using BookStore.Models.Response;
using System.Threading.Tasks;

namespace BookStore.Contracts.Services.Data
{
    public interface IStatisticsService
    {
        Task<StatisticsResponse> GetStatistics();
    }
}