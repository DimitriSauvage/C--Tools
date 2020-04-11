using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tools.Infrastructure.EntityFramework.Helpers;
using Tools.Infrastructure.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.EntityFramework.Abstractions
{
    public class SoPerfDbContextWithIdentity<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>, ISoPerfDbContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        static SoPerfDbContextWithIdentity()
        {

        }

        protected SoPerfDbContextWithIdentity() : base()
        {
            //Database.Migrate();
        }

        protected SoPerfDbContextWithIdentity(DbContextOptions options) : base(options)
        {
            //Database.Migrate();
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
