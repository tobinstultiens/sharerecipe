using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.Common.Infrastructure
{
    public interface IAggregateUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets a bool indicating whether there is an active database transaction.
        /// </summary>
        public bool HasActiveTransaction { get; }

        /// <summary>
        /// Gets the current database transaction.
        /// </summary>
        public IDbContextTransaction CurrentTransaction { get; }

        /// <summary>
        /// Gets the database facade.
        /// </summary>
        public DatabaseFacade Database { get; }

        /// <summary>
        /// Begins the database transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable database transaction.</returns>
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the transaction to the database asynchronously.
        /// </summary>
        /// <param name="transaction">The database transaction.</param>
        /// <returns>Returns an awaitable task.</returns>
        public Task CommitTransactionAsync(IDbContextTransaction transaction);

        /// <summary>
        /// Rolls back the database transaction.
        /// </summary>
        public void RollbackTransaction();
    }
}