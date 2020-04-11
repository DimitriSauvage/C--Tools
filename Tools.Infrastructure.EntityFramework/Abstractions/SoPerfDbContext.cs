using Microsoft.EntityFrameworkCore;
using Tools.Infrastructure.EntityFramework.Helpers;
using Tools.Infrastructure.EntityFramework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tools.Infrastructure.EntityFramework.Abstractions
{
    public class SoPerfDbContext<TContext> : SoPerfDbContext, ISoPerfDbContext
        where TContext : DbContext
    {
        //static SoPerfDbContext() : base()
        //{

        //}

        protected SoPerfDbContext() : base()
        {
            //Database.Migrate();
        }

        protected SoPerfDbContext(DbContextOptions<TContext> options) : base(options)
        {
            //Database.Migrate();
        }

    }

    public class SoPerfDbContext : DbContext, ISoPerfDbContext
    {
        //static SoPerfDbContext()
        //{

        //}

        public SoPerfDbContext() : base()
        {
            //Database.Migrate();
        }

        public SoPerfDbContext(DbContextOptions options) : base(options)
        {
           // Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var maps = ClassMapHelper.GetAllMaps(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var map in maps)
            {
                dynamic mapInstance = Activator.CreateInstance(map);
                modelBuilder.ApplyConfiguration(mapInstance);
            }

        }
    }
}
