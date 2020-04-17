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
    public class AppIdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>, ISoPerfDbContext
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
        where TKey : IEquatable<TKey>
    {
        static AppIdentityDbContext()
        {

        }

        protected AppIdentityDbContext() : base()
        {
        }

        protected AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapHelper.ApplyMapsConfiguration(AppDomain.CurrentDomain.GetAssemblies(), modelBuilder);

        }
    }
}
