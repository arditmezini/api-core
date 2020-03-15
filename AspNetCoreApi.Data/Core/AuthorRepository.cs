using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApiContext _context;

        public AuthorRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _context.Authors.Include(x => x.AuthorContact).ToListAsync();
        }

        public async Task<Author> GetById(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task Add(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Author entity = await GetById(id);

            if (entity != null)
            {
                entity.SoftDelete();
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
