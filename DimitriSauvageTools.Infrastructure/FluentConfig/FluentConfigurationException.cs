using System;
using DimitriSauvageTools.Exceptions;

namespace DimitriSauvageTools.Infrastructure.FluentConfig
{
    public class FluentConfigurationException : AppException
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
