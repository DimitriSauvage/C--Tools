using Tools.Domain.Abstractions;
using Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.Exceptions
{
    public class EntityNotFoundException<TEntity> : ToolsException where TEntity : Entity
    {
        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(long id) : base($"Unable to find an entity of type {nameof(TEntity)} corresponding to the identifier {id}.")
        {

        }
    }
}
