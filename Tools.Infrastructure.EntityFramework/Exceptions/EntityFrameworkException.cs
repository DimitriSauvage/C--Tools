using Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.EntityFramework.Exceptions
{
    public class EntityFrameworkException : ToolsException
    {
        #region Properties
        /// <summary>
        /// Le paramètre de l'appel de méthode qui a déclenché l'excetion
        /// </summary>
        public object Parameter { get; set; }
        #endregion

        public EntityFrameworkException() : base() { }
        public EntityFrameworkException(string message) : base(message) { }
        public EntityFrameworkException(string message, Exception e) : base(message, e) { }
        public EntityFrameworkException(Exception e) : base(e.Message, e) { }
        public EntityFrameworkException(Exception e, object parameter) : base(e.Message, e) { Parameter = parameter; }
    }
}
