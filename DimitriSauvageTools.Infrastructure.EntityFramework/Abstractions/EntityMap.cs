using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DimitriSauvageTools.Domain.Abstractions;
using DimitriSauvageTools.Domain.Helpers;

namespace DimitriSauvageTools.Infrastructure.EntityFramework.Abstractions
{
    /// <summary>
    /// Map class for an entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        /// <inheritdoc />
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var tableName = typeof(TEntity).Name;
            var schema = typeof(TEntity).Namespace.ExtractSchemaFromDomain();

            builder.ToTable(tableName, schema);
        }
    }
}