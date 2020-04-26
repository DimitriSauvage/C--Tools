using Microsoft.EntityFrameworkCore;
using DimitriSauvageTools.Infrastructure.EntityFramework;
using DimitriSauvageTools.Infrastructure.EntityFramework.Abstractions;
using System;

namespace DimitriSauvageTools.UnitTesting
{
    public abstract class BaseTests<TContext> : IDisposable
        where TContext : DbContext
    {
        #region Attributes        
        protected readonly TContext dbContext;
        #endregion

        #region Constructor
        public BaseTests()
        {
            //Création du contexte de base de données

            var dbContextFactory = new SoPerfDbContextFactory<TContext>(DbType.SQL_SERVER);
            this.dbContext = dbContextFactory.CreateDbContext(null);
            
            string localEnvironment = Environment.GetEnvironmentVariable("SOPERF_LOCAL_DEV");

            //Suppression et recréatino de la base de données pour avoir les données des seeds
            //Se fait uniquement en dev donc si on a la variable d'environnement
            if (localEnvironment != null)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }
        }


        #endregion

        #region Dispose
        public void Dispose()
        {
        }
        #endregion
    }
}
