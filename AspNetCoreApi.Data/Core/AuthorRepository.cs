﻿using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(ApiContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await dbSet.Include(x => x.AuthorContact).ToListAsync();
        }
    }
}
