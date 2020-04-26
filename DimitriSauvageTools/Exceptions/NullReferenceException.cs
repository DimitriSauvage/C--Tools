using System;

namespace DimitriSauvageTools.Exceptions
{
    public class NullReferenceException<T> : NullReferenceException where T : class
    {
        public NullReferenceException() : base($"The object of type {typeof(T).FullName} is null")
        {

        }

        public NullReferenceException(string message) : base(message)
        {

        }
    }
}
