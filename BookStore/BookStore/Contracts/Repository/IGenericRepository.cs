using System.Threading.Tasks;

namespace BookStore.Contracts.Repository
{
    public interface IGenericRepository
    {
        Task<TOut> Post<TOut, TIn>(string url, TIn content);
    }
}