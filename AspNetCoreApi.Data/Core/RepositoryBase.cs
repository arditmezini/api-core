using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public abstract class RepositoryBase<T> where T : class
    {
        public readonly ApiContext _context;
        public readonly DbSet<T> dbSet;

        public RepositoryBase(ApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            dbSet = context.Set<T>();
        }

        public virtual async Task<PagedList<T>> Get(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (include != null)
                    query = include(query);

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);

                if (pageNumber > 0)
                    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                var count = await query.CountAsync();
                var items = await query.ToListAsync();

                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(int id)
        {
            T entity = await GetById(id);

            if (entity != null)
            {
                entity.SoftDelete();
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}