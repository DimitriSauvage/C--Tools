using Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Http.Exceptions
{
    public class HttpResponseException : ToolsException
    {
        #region Constructors
        public HttpResponseException(string uri) : base($"Une erreur est survenue durant l'appel de la ressource {uri}")
        {
        }

        public HttpResponseException(string uri, string message) : base($"Une erreur est survenue durant l'appel de la ressource {uri} : {message}")
        {

        }
        #endregion
    }
}
