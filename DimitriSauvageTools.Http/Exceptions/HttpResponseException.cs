using DimitriSauvageTools.Exceptions;

namespace DimitriSauvageTools.Http.Exceptions
{
    public class HttpResponseException : AppException
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
