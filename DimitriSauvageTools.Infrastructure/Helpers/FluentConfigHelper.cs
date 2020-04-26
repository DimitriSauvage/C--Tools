using System.Collections.Generic;
using System.Linq;
using DimitriSauvageTools.Infrastructure.FluentConfig;

namespace DimitriSauvageTools.Infrastructure.Helpers
{
    public class FluentConfigHelper
    {
        /// <summary>
        /// Obtient la configuration des assemblys à mapper en base de données
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetAssemblies()
        {
            // Récupération de la configuration
            FluentConfigurationSectionHandler appFluentConfigurationSection = FluentConfigurationSectionHandler.GetConfig();

            if (appFluentConfigurationSection.FluentConfigurationDispatchers.Count == 0)
                throw new FluentConfigurationException("Aucun assembli à mapper n'a été configuré dans l'application pour la section 'fluentConfiguration'", null);
            else if (appFluentConfigurationSection.FluentConfigurationDispatchers.Count > 1)
                throw new FluentConfigurationException("Un seul dispatcher d'assembli ne peut-être configuré dans l'application pour la section 'fluentConfiguration'", null);

            return appFluentConfigurationSection.FluentConfigurationDispatchers.First().Assemblies.Select(ass => ass.Assembly);
        }
    }
}
