using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Exceptions
{
    [Serializable]
    public class ToolsException : ApplicationException
    {

        public ToolsException()
        {

        }

        public ToolsException(string message) : base(message)
        {

        }

        public ToolsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
