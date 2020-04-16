using System;
using System.Collections.Generic;
using System.Text;
using Tools.Domain.Extensions;

namespace Tools.Domain.Abstractions
{
    public interface IEntity
    {
        /// <summary>
        /// Recopie l'ensemble des propriétés simples (membres par valeur).
        /// Ne recopie par les champs qui composent la clé unique ainsi que l'identifiant.
        /// </summary>
        /// <param name="other">objet à recopier</param>
        /// <returns>Copie</returns>
        void CopyFrom(IEntity other);
    }
}