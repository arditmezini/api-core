using System;
using System.Collections.Concurrent;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreApi.Dal.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApi.Dal.Core
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<Type, object> _repositories;

        public UnitOfWork(TContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _repositories = new ConcurrentDictionary<Type, object>();
        }

        public IRepository<TContext, TEntity> GetRepository<TEntity>() where TEntity : class
        {
            var lazyRepository = new Lazy<IRepository<TContext, TEntity>>(() => _serviceProvider.GetRequiredService<IRepository<TContext, TEntity>>());
            lazyRepository = (Lazy<IRepository<TContext, TEntity>>)_repositories.GetOrAdd(typeof(TEntity), lazyRepository);
            return lazyRepository.Value;
        }

        public async Task<IRepository<TContext, TEntity>> GetRepositoryAsync<TEntity>(CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class
        {
            return await Task.Run(() => GetRepository<TEntity>(), cancellationToken);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = default(IsolationLevel), CancellationToken cancellationToken = default(CancellationToken))
        {
            return isolationLevel != default(IsolationLevel) ? await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken) : await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
            _repositories?.Clear();

            GC.SuppressFinalize(this);
        }
    }
}