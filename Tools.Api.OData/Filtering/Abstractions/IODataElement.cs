using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Api.OData.Filtering.Abstractions
{
    public interface IODataElement
    {
        /// <summary>
        /// Récupère la representation du paramètre pour une URL OData
        /// </summary>
        /// <returns></returns>
        string GetUrlRepresentation();
    }
}
