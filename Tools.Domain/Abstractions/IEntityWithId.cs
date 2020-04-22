using System;

namespace Tools.Domain.Abstractions
{
    public interface IEntityWithId<TId> : IEntity
    {
        TId Id { get; set; }
    }

    public interface IEntityWithId : IEntityWithId<Guid>
    {
    }
}