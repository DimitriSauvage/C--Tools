using Tools.Domain.Abstractions;
using Tools.Exceptions;

namespace Tools.Infrastructure.Exceptions
{
    public class EntityAlreadyExistsException<TEntity> : AppException where TEntity : IEntity
    {
        public EntityAlreadyExistsException() : base($"Cannot create a duplicate {(typeof(TEntity)).Name.ToLower()}.")
        {

        }

        public EntityAlreadyExistsException(IEntityWithId entity) : base($"Cannot create a duplicate entity of type {nameof(TEntity)}, the existing Id was {entity.Id}")
        {

        }
    }
}
