using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApiContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books
                .Include(x => x.BookCategory)
                .Include(x => x.Publisher).ToListAsync();
        }
    }
}
