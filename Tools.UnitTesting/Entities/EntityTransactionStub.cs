using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace Tools.UnitTesting.Entities
{
    /// <summary>
    /// Classe implémentant l'inteface IDbContextTransaction pour les mocks des tests
    /// </summary>
    public class EntityTransactionStub : IDbContextTransaction
    {
        public Guid TransactionId => new Guid();

        public void Commit(){ }
        public void Dispose(){ }
        public void Rollback(){ }
    }
}
