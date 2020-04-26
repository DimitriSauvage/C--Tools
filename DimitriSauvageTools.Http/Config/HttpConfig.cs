namespace DimitriSauvageTools.Http.Config
{
    public class HttpConfig
    {
        #region Properties
        /// <summary>
        /// Affecte ou obtient le nombre de tentative avant de tomber en erreur
        /// </summary>
        public int HttpClientRetryCount { get; set; } = 6;
        /// <summary>
        /// Affecte ou obtient le nombre d'exceptions à catcher avant de tomber en erreur
        /// </summary>
        public int HttpClientExceptionsAllowedBeforeBreaking { get; set; } = 5;
        /// <summary>
        /// Affecte ou obtient un booléen qui indique si on utilise un client HTTP "élastique"
        /// </summary>
        public bool UseResilientHttp { get; set; } = true;
        #endregion
    }
}
