using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tools.Domain.Abstractions;
using Tools.Domain.Helpers;

namespace Tools.Infrastructure.EntityFramework.Abstractions
{
    /// <summary>
    /// Map class for an entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class EntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        /// <inheritdoc />
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var tableName = typeof(TEntity).Name;
            var schema = typeof(TEntity).Namespace.ExtractSchemaFromDomain();

            builder.ToTable(tableName, schema);
        }
    }

    /// <summary>
    /// Map class for a composite id entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class EntityWithCompositeIdMap<TEntity> : EntityMap<TEntity>, IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntityWithCompositeId
    {
        /// <inheritdoc />
        public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
        }
    }

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

    public class EntityWithTrackingMap<TEntity> : EntityWithIdMap<TEntity, long>, IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntityWithTracking
    {
        /// <summary>
        /// Génère la configuration type pour l'entité <typeparamref name="TEntity"/>
        /// - La table générée sera du nom du type de l'entité
        /// - Le champ Id sera clé primaire
        /// - Les champs CreatedAd, CreatedBy, UpdatedAd, UpdatedBy sont mappés.
        /// </summary>
        /// <param name="builder"></param>
        public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            
            builder.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("getutcdate()");
            builder.Property(c => c.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(c => c.UpdatedAt);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
        }
    }
}