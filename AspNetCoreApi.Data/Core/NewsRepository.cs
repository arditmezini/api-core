using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AspNetCoreApi.Models.Entity;

namespace AspNetCoreApi.Dal.Core
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(ApiContext context)
            : base(context)
        { }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.OrderByDescending(x => x.DateCreated).ToListAsync();
        }
    }
}