using Tools.Domain.Abstractions;
using Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.Exceptions
{
    public class EntityAlreadyExistsException<TEntity> : ToolsException where TEntity : IEntity
    {
        public EntityAlreadyExistsException() : base($"Cannot create a duplicate {(typeof(TEntity)).Name.ToLower()}.")
        {

        }

        public EntityAlreadyExistsException(IEntityWithId entity) : base($"Cannot create a duplicate entity of type {nameof(TEntity)}, the existing Id was {entity.Id}")
        {

        }
    }
}
