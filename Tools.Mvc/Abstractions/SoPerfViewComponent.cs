using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tools.Mvc.Abstractions
{
    public abstract class SoPerfViewComponent : ViewComponent
    {
        /// <summary>
        /// Obtient le gestionnaire de ressources <see cref="IStringLocalizer"/>
        /// </summary>
        public IStringLocalizer Localizer { get; private set; }

        #region Constructors
        /// <summary>
        /// Instancie un nouveau <see cref="SoPerfViewComponent"/>
        /// </summary>
        protected SoPerfViewComponent(IStringLocalizerFactory stringLocalizerFactory)
        {
            // Localisation des ressources propres au module
            this.Localizer = stringLocalizerFactory.Create($"Views.Shared.Components.{this.GetType().Name.Replace("ViewComponent", string.Empty)}.Default", Assembly.GetCallingAssembly().GetName().Name);
        }
        #endregion
    }
}
