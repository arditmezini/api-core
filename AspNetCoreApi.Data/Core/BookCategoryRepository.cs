using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly ApiContext _context;

        public BookCategoryRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookCategory>> GetAll()
        {
            return await _context.BookCategories.Include(x => x.Books).ToListAsync();
        }

        public async Task<BookCategory> GetById(int id)
        {
            return await _context.BookCategories.FindAsync(id);
        }

        public async Task Add(BookCategory bookCategory)
        {
            await _context.BookCategories.AddAsync(bookCategory);
        }

        public void Update(BookCategory bookCategory)
        {
            _context.Entry(bookCategory).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            BookCategory entity = await GetById(id);

            if (entity != null)
            {
                entity.SoftDelete();
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
