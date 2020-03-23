using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _context;

        public UnitOfWork(ApiContext context)
        {
            _context = context;
        }

        #region Repos
        private IAuthorRepository _authors;
        public IAuthorRepository Authors
        {
            get
            {
                if (_authors == null)
                {
                    _authors = new AuthorRepository(_context);
                }
                return _authors;
            }
        }

        private IBookCategoryRepository _bookCategorys;
        public IBookCategoryRepository BookCategorys
        {
            get
            {
                if (_bookCategorys == null)
                {
                    _bookCategorys = new BookCategoryRepository(_context);
                }
                return _bookCategorys;
            }
        }

        private IBookRepository _books;
        public IBookRepository Books
        {
            get
            {
                if (_books == null)
                {
                    _books = new BookRepository(_context);
                }
                return _books;
            }
        }

        private IPublisherRepository _publishers;
        public IPublisherRepository Publishers
        {
            get
            {
                if (_publishers == null)
                {
                    _publishers = new PublisherRepository(_context);
                }
                return _publishers;
            }
        }

        private IGeneralDataRepository _generalData;
        public IGeneralDataRepository GeneralData
        {
            get
            {
                if (_generalData == null)
                {
                    _generalData = new GeneralDataRepository(_context);
                }
                return _generalData;
            }
        }
        #endregion

        #region Methods
        public bool Complete()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = default(IsolationLevel))
        {
            return _context.Database.BeginTransaction(isolationLevel);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = default(IsolationLevel), CancellationToken cancellationToken = default(CancellationToken))
        {
            return isolationLevel != default(IsolationLevel) ? await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken) : await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            _context?.Dispose();

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}