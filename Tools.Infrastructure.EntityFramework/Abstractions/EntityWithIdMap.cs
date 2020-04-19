using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Domain.Abstractions;

namespace Tools.Infrastructure.EntityFramework.Abstractions
{
    /// <summary>
    /// Map class for an entity with an id
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class EntityWithIdMap<TEntity> : EntityWithIdMap<TEntity, long>
        where TEntity : class, IEntityWithId
    {
    }

    /// <summary>
    /// Map class for an entity with an id
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TId">Identifier type</typeparam>
    public class EntityWithIdMap<TEntity, TId> : EntityMap<TEntity>, IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntityWithId<TId> where TId : IEquatable<TId>
    {
        /// <inheritdoc/>
        public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}