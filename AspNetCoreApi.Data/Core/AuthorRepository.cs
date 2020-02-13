using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
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
    }
}
