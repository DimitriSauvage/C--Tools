namespace Tools.Infrastructure.Models
{
    public class SetUpDTO
    {
        #region Enums
        public enum ServerModeEnum { Cloud = 0, Server = 1}
        #endregion

        #region Properties
        /// <summary>
        /// Affecte ou obtient le mode de fonctionnement
        /// </summary>
        public ServerModeEnum ServerMode { get; set; } 
        /// <summary>
        /// Affecte ou obtient les options de la base de données
        /// </summary>
        public DatabaseSettingsDTO DatabaseSettings { get; set; } = new DatabaseSettingsDTO();
        /// <summary>
        /// Affecte ou obtient les options d'initialisation de la société
        /// </summary>
        public CompanySettingsDTO CompanySettings { get; set; } = new CompanySettingsDTO();
        /// <summary>
        /// Affecte ou obtient les options d'initialisation du compte admin
        /// </summary>
        public SuperAdminSettingsDTO SuperAdminSettings { get; set; } = new SuperAdminSettingsDTO();
        #endregion
    }
}
