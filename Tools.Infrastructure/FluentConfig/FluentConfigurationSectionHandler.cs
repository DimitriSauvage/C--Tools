
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Infrastructure.FluentConfig
{
    public class FluentConfigurationSectionHandler : ConfigurationSection
    {
        /// <summary>
        /// Retourne la configuration base de données par base de données
        /// </summary>
        [ConfigurationProperty("fluentConfigurationDatabaseDispatchers")]
        [ConfigurationCollection(typeof(FluentConfigurationDatabaseDispatcherElement), AddItemName = "fluentConfigurationDatabaseDispatcher")]
        public FluentConfigurationDatabaseDispatcherCollection FluentConfigurationDispatchers
        {
            get { return this["fluentConfigurationDatabaseDispatchers"] as FluentConfigurationDatabaseDispatcherCollection; }
        }

        /// <summary>
        /// Retourne la configuration pour nHibernate
        /// </summary>
        /// <returns></returns>
        public static FluentConfigurationSectionHandler GetConfig()
        {
            return ConfigurationManager.GetSection("fluentConfiguration") as FluentConfigurationSectionHandler;
        }
    }
}
