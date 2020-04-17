using Tools.Domain.DataAnnotations;

namespace Tools.Infrastructure.Plugins
{
    public class ViewInfo
    {
        #region Properties
        /// <summary>
        /// Affecte ou obtient le de la page
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Affecte ou obtient une description du contenu de la page
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Affecte ou obtient le nom complet de la vue
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Affecte ou obtient le nom du controller
        /// </summary>
        [UniqueKey]
        public string ControllerName { get; set; }
        /// <summary>
        /// Affecte ou obtient le nom de l'action
        /// </summary>
        [UniqueKey]
        public string ActionName { get; set; }
        /// <summary>
        /// Affecte ou obtient la route pour charger la vue
        /// </summary>
        public string Route { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public ViewInfo()
        {

        }
        #endregion

        #region Methods

        #endregion
        
    }
}
