using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DimitriSauvageTools.Mvc.Abstractions
{
    public abstract class BaseViewComponent : ViewComponent
    {
        /// <summary>
        /// Obtient le gestionnaire de ressources <see cref="IStringLocalizer"/>
        /// </summary>
        public IStringLocalizer Localizer { get; private set; }

        #region Constructors
        /// <summary>
        /// Instancie un nouveau <see cref="BaseViewComponent"/>
        /// </summary>
        protected BaseViewComponent(IStringLocalizerFactory stringLocalizerFactory)
        {
            // Localisation des ressources propres au module
            this.Localizer = stringLocalizerFactory.Create($"Views.Shared.Components.{this.GetType().Name.Replace("ViewComponent", string.Empty)}.Default", Assembly.GetCallingAssembly().GetName().Name);
        }
        #endregion
    }
}
