using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Core
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly ApiContext _context;

        public BookCategoryRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<BookCategory> GetAll()
        {
            return _context.BookCategories;
        }

        public BookCategory GetById(int id)
        {
            return _context.BookCategories.Find(id);
        }

        public void Add(BookCategory bookCategory)
        {
            _context.BookCategories.Add(bookCategory);
        }

        public void Update(BookCategory bookCategory)
        {
            _context.Entry(bookCategory).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            BookCategory entity = GetById(id);

            if (entity != null)
            {
                entity.SoftDelete();
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
