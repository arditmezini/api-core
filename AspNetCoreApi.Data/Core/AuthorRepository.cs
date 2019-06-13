using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Core
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApiContext _context;

        public AuthorRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.Include(x => x.AuthorContact);
        }
    }
}
