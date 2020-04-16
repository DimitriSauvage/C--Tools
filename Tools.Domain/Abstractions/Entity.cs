using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tools.Domain.DataAnnotations;
using Tools.Domain.Extensions;
using Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tools.Domain.Helpers;

namespace Tools.Domain.Abstractions
{
    public abstract class Entity : IEntity, IEquatable<IEntity>, IComparable<IEntity>
    {
        #region Attributes
        private List<INotification> _domainEvents = null;
        #endregion

        #region Properties
        /// <summary>
        /// Affecte ou obtient un dictionnaire contenant les propriétés qui ont changées
        /// Key = nom de la propriété
        /// Value = Tuple[AncienneValeur, NouvelleValeur]
        /// </summary>
        protected virtual Dictionary<string, Tuple<dynamic, dynamic>> ValuesChanged { get; set; } = new Dictionary<string, Tuple<dynamic, dynamic>>();
        /// <summary>
        /// Obtient la liste des évènements déclenchés sur cette entité
        /// </summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
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
            this.ValidateObject();
        }

        /// <summary>
        /// Méthode de validation de l'objet
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> TryValidateObject()
        {
            return this.TryValidateObject();
        }

        /// <summary>
        /// Ajoute l'évènement passé en paramètre à la collection des évènements
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Supprime l'évènement passé en paramètre de la collection des évènements
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        /// <summary>
        /// Réinitialise la collection d'évènements
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Sert de fonction de hachage pour l'objet en cours.
        /// </summary>
        /// <returns>Code de hachage pour l'objet en cours.</returns>
        public override int GetHashCode()
        {
            int? hashCode = HashCodeHelper.GetHashCode(this);
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
    }


}
