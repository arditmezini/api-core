using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class BookCategoryRepository : RepositoryBase<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(ApiContext context)
                : base(context)
        { }

        public async Task<IEnumerable<BookCategory>> GetAll()
        {
            return await _context.BookCategories.Include(x => x.Books).ToListAsync();
        }
    }
}