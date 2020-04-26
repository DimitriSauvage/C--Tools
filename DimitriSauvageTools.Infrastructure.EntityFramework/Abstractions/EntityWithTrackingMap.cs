using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DimitriSauvageTools.Domain.Abstractions;

namespace DimitriSauvageTools.Infrastructure.EntityFramework.Abstractions
{
    public abstract class EntityWithTrackingMap<TEntity> : EntityWithIdMap<TEntity, Guid>, IEntityTypeConfiguration<TEntity>
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