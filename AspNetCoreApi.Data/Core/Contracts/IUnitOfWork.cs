using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IBookCategoryRepository BookCategorys { get; }
        IBookRepository Books { get; }
        IPublisherRepository Publishers { get; }
        IGeneralDataRepository GeneralData { get; }
        IStatisticsRepository StatisticsRepository { get; }

        bool Complete();
        Task<bool> CompleteAsync();

        #region Transaction

        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = default(IsolationLevel));
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = default(IsolationLevel), CancellationToken cancellationToken = default(CancellationToken));
        void Commit();
        void Rollback();

        #endregion
    }
}