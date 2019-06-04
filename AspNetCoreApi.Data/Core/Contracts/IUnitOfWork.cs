using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IUnitOfWork<TDbContext> : IDisposable where TDbContext : DbContext
    {
        IRepository<TDbContext, TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<IRepository<TDbContext, TEntity>> GetRepositoryAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = default(IsolationLevel), CancellationToken cancellationToken = default(CancellationToken));
    }
}