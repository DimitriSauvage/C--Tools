using System;

namespace DimitriSauvageTools.Domain.Abstractions
{
    /// <summary>
    /// Type d'entité avec des données concernant le suivi 
    /// </summary>
    public abstract class EntityWithTracking : EntityWithId, IEntityWithTracking
    {
        #region Properties

        /// <inheritdoc />
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc />
        public string CreatedBy { get; set; }

        /// <inheritdoc />
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc />
        public string UpdatedBy { get; set; }

        #endregion

        #region Constructors

        public EntityWithTracking() : base()
        {
            this.CreatedAt = DateTime.Now;
        }

        #endregion
    }
}