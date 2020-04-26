using System;

namespace DimitriSauvageTools.Domain.Abstractions
{
    public interface IEntityWithId<TId> : IEntity
    {
        TId Id { get; set; }
    }

    public interface IEntityWithId : IEntityWithId<Guid>
    {
    }
}