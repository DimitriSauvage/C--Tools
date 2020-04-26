using Microsoft.AspNetCore.Http;
using DimitriSauvageTools.Http.Enums;

namespace DimitriSauvageTools.Http.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string AUTHORIZE = "Authorization";
        private const string BEARER_NAME = "Bearer ";

        /// <summary>
        /// Extrait la valeur du jeton d'authorization depuis l'entête de la requête 
        /// </summary>
        /// <param name="request">Requête reçue</param>
        /// <param name="source">Source ou récupérer le token</param>
        /// <returns></returns>
        public static string GetJWTAuthorizeToken(this HttpRequest request,
            JWTAuthorizeTokenSource source = JWTAuthorizeTokenSource.Header)
        {
            string authorizeToken = null;

            if (source == JWTAuthorizeTokenSource.Header)
            {
                //Récupération depuis le header
                authorizeToken = request.Headers[AUTHORIZE];
                if (!string.IsNullOrEmpty(authorizeToken))
                    authorizeToken = authorizeToken.Remove(0, BEARER_NAME.Length);
            }
            else
            {
                //On récupère le token depuis les cookies
                request.Cookies.TryGetValue(AUTHORIZE, out authorizeToken);
            }

            return authorizeToken;
        }
    }
}