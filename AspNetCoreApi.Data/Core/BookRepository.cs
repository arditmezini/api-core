using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Core
{
    public class BookRepository : IBookRepository
    {
        private readonly ApiContext _context;

        public BookRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(x => x.BookCategory)
                .Include(x => x.Publisher);
        }
    }
}
