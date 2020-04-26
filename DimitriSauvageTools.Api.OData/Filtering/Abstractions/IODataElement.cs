namespace DimitriSauvageTools.Api.OData.Filtering.Abstractions
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
