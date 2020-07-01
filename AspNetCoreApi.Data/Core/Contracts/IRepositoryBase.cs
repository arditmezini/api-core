using AspNetCoreApi.Models.Common.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<PagedList<T>> Get(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageNumber = 0, int pageSize = 0);
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}