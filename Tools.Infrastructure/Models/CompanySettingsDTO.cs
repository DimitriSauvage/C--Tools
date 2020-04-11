using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Tools.Infrastructure.Models
{
    public class CompanySettingsDTO
    {
        /// <summary>
        /// Affecte ou obtient la raison sociale de la société
        /// </summary>
        public string Name { get; set; }
    }
}
