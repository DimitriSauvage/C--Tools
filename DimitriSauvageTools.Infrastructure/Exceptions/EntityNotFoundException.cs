using System;
using DimitriSauvageTools.Domain.Abstractions;
using DimitriSauvageTools.Exceptions;

namespace DimitriSauvageTools.Infrastructure.Exceptions
{
    public class EntityNotFoundException<TEntity> : AppException where TEntity : IEntity
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(Guid id) : base(
            $"Unable to find an entity of type {nameof(TEntity)} corresponding to the identifier {id}.")
        {
        }
    }
}