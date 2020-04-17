using Tools.Domain.Abstractions;
using Tools.Exceptions;

namespace Tools.Infrastructure.Exceptions
{
    public class EntityNotFoundException<TEntity> : AppException where TEntity : IEntity
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(long id) : base(
            $"Unable to find an entity of type {nameof(TEntity)} corresponding to the identifier {id}.")
        {
        }
    }
}