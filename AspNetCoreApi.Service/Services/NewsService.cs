using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class NewsService : INewsService
    {
        private IUnitOfWork uow;

        public NewsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await uow.News.GetAll();
        }

        public async Task<bool> Add(News news)
        {
            await uow.News.Add(news);
            return await uow.CompleteAsync();
        }
    }
}