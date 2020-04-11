using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Http.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string AUTHORIZE = "Authorization";

        /// <summary>
        /// Extrait la valeur du jeton d'authorization depuis l'entête de la requête 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetJWTAuthorizeToken(this HttpRequest request)
        {
            string authorizeToken = request.Headers[AUTHORIZE];
            if (!string.IsNullOrEmpty(authorizeToken))
                return authorizeToken.Remove(0, "Bearer ".Length);

            return null;
        }
    }
}
