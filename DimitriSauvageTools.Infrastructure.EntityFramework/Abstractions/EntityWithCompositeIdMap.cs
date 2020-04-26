using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DimitriSauvageTools.Domain.Abstractions;

namespace DimitriSauvageTools.Infrastructure.EntityFramework.Abstractions
{
    /// <summary>
    /// Map class for a composite id entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityWithCompositeIdMap<TEntity> : EntityMap<TEntity>, IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntityWithCompositeId
    {
        /// <inheritdoc />
        public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
        }
    }
}