using System;

namespace Tools.Domain.Abstractions
{
    public interface IEntityWithTracking : IEntityWithId<long>
    {
        /// <summary>
        /// Entity creation date
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Entity creator
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// Entity last updating date
        /// </summary>
        DateTimeOffset UpdatedAt { get; set; }
        /// <summary>
        /// Last update author
        /// </summary>
        string UpdatedBy { get; set; }
    }
}