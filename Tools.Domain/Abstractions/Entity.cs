using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using MediatR;
using Tools.Domain.Extensions;
using Tools.Domain.Helpers;
using Tools.Helpers;

namespace Tools.Domain.Abstractions
{
    public abstract class Entity : IEntity, IEquatable<IEntity>, IComparable<IEntity>
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// recopie l'ensemble des propriétés simples (membres par valeur).
        /// ne recopie par les champs qui composent la clé unique ainsi que l'identifiant.
        /// </summary>
        /// <param name="other">objet à recopier</param>
        /// <returns>copie</returns>
        public virtual void CopyFrom(IEntity other)
        {
            this.ShallowCopy(other);
        }

        /// <summary>
        /// Méthode de validation de l'objet
        /// Déclenche une exception avec les erreurs recontrées
        /// </summary>
        public virtual void ValidateObject()
        {
            Validator.ValidateObject(this, this.GetValidationContext());
        }

        /// <summary>
        /// Méthode de validation de l'objet
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> TryValidateObject()
        {
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(this, this.GetValidationContext(), result, true);
            return result;
        }

        /// <summary>
        /// Sert de fonction de hachage pour l'objet en cours.
        /// </summary>
        /// <returns>Code de hachage pour l'objet en cours.</returns>
        public override int GetHashCode()
        {
            var hashCode = HashCodeHelper.GetHashCode(this);
            return hashCode.HasValue ? hashCode.Value : base.GetHashCode();
        }

        /// <summary>
        /// Détermine si l'objet spécifié est identique à l'objet actuel.
        /// </summary>
        /// <param name="obj">Objet à comparer avec l'objet actif. </param>
        /// <returns>true si l'objet spécifié est égal à l'objet actif ; sinon, false.</returns>
        public override bool Equals(object obj)
        {
            return this.GetHashCode().Equals(obj.GetHashCode());
        }

        /// <summary>
        /// Compare l'objet en cours à un autre objet du même type.
        /// </summary>
        /// <param name="other">Objet à comparer avec cet objet.</param>
        /// <returns>Valeur qui indique l'ordre relatif des objets comparés.</returns>
        public virtual int CompareTo(IEntity other)
        {
            return this.GetHashCode().CompareTo(other.GetHashCode());
        }

        /// <summary>
        /// Indique si l'objet actuel est égal à un autre objet du même type.
        /// </summary>
        /// <param name="other">Objet à comparer avec cet objet.</param>
        /// <returns>true si l'objet en cours est égal au paramètre other ; sinon, false.</returns>
        public virtual bool Equals(IEntity other)
        {
            return this.Equals(other as object);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the validation context
        /// </summary>
        /// <returns>Validation context</returns>
        private ValidationContext GetValidationContext()
        {
            return new ValidationContext(this, serviceProvider: null, items: null);
        }

        #endregion
    }
}