using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DimitriSauvageTools.Infrastructure.EntityFramework.Abstractions
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
        }

        protected AppDbContext() : base()
        {
            //Database.Migrate();
        }


        protected AppDbContext(DbContextOptions options) : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var maps = GetAllMaps();
        }

        /// <summary>
        /// Obtient toutes les classes qui sont déclarées comme étant des maps
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<Type> GetAllMaps()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IEntityTypeConfiguration<>).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
        }
    }
}