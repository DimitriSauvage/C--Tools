using System;

namespace DimitriSauvageTools.Domain.Abstractions
{
    public interface IEntityWithTracking : IEntityWithId
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