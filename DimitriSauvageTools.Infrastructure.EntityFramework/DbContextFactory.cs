using System;
using System.IO;
using System.Linq;
using DimitriSauvageTools.Infrastructure.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DimitriSauvageTools.Infrastructure.Settings;

namespace DimitriSauvageTools.Infrastructure.EntityFramework
{
    public class DbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        #region Private attributes

        private readonly DbContextOptions<TContext> dbContextOptions = null;
        private readonly DbType dbType;

        #endregion

        public TContext Context { get; private set; }

        public DbContextFactory(DbType dbType)
        {
            this.dbType = dbType;
        }

        public DbContextFactory(DbType dbType, DbContextOptionsBuilder<TContext> dbContextOptions)
        {
            this.dbType = dbType;
            this.dbContextOptions = dbContextOptions.Options;
        }

        public TContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.connectionstrings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder =
                new DbContextOptionsBuilder<TContext>(dbContextOptions == null
                    ? new DbContextOptions<TContext>()
                    : dbContextOptions);

            var appSettings = configuration.GetSection("AppSettings").Get<DatabaseSettings>();
            var connectionString = appSettings.ConnectionStrings
                .First(cs => cs.Name == appSettings.UsedConnectionString).ConnectionString;

            switch (dbType)
            {
                case DbType.SqlServer:
                    builder.UseSqlServer(connectionString);
                    break;
                default:
                    break;
            }

            //Création du context
            Context = (TContext) Activator.CreateInstance(typeof(TContext), builder.Options);
            Context.ChangeTracker.QueryTrackingBehavior = appSettings.TrackEntities
                ? QueryTrackingBehavior.TrackAll
                : QueryTrackingBehavior.NoTracking;


            return Context;
        }
    }
}