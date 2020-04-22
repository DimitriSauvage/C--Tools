using System;
using Tools.Domain.DataAnnotations;

namespace Tools.Domain.Abstractions
{
    /// <summary>
    /// Entité générique qui sera mappée dans un ORM
    /// </summary>
    /// <typeparam name="T">Type de l'identifiant</typeparam>
    public abstract class EntityWithId<T> : Entity, IEntityWithId<T>
    {
        #region Properties

        /// <summary>
        /// Affecte ou obtient l'identifiant de type <typeparamref name="T"/>
        /// </summary>
        [Id]
        public virtual T Id { get; set; }

        /// <summary>
        /// Obtient si l'instance actuelle est transiante
        /// </summary>
        public bool IsTransient
        {
            get
            {
                T defaultValue = default;
                return defaultValue == null ? this.Id == null : defaultValue.Equals(this.Id);
            }
        }

        #endregion

        #region Constructors

        public EntityWithId()
        {
        }

        #endregion
    }

    /// <summary>
    /// Entité de base qui sera mappée dans un ORM.
    /// </summary>
    public abstract class EntityWithId : EntityWithId<Guid>, IEntityWithId
    {
    }
}