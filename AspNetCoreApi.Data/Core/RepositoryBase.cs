using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public abstract class RepositoryBase<T> where T : class
    {
        public readonly ApiContext _context;
        public readonly DbSet<T> dbSet;

        public RepositoryBase(ApiContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
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