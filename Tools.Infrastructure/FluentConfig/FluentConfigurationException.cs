using Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.FluentConfig
{
    public class FluentConfigurationException : ToolsException
    {
        public FluentConfigurationException()
        {

        }

        public FluentConfigurationException(string message) : base(message)
        {

        }

        public FluentConfigurationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
