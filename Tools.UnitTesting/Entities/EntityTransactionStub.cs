using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tools.UnitTesting.Entities
{
    /// <summary>
    /// Classe implémentant l'inteface IDbContextTransaction pour les mocks des tests
    /// </summary>
    public class EntityTransactionStub : IDbContextTransaction
    {
        /// <inheritdoc />²
        public async Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken())
        {
        }

        public Guid TransactionId => new Guid();

        public void Commit(){ }
        public void Dispose(){ }
        public void Rollback(){ }

        /// <inheritdoc />
        public async Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
        }

        #region Implementation of IAsyncDisposable

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
        }

        #endregion
    }
}
