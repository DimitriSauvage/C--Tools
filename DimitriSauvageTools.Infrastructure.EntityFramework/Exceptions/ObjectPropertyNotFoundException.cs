using System;
using DimitriSauvageTools.Exceptions;

namespace DimitriSauvageTools.Infrastructure.EntityFramework.Exceptions
{
    public class ObjectPropertyNotFoundException : AppException
    {
        public ObjectPropertyNotFoundException(string name, Type type) : base(
            $"The property {name} does not exist on the object {type.Name}")
        {
        }
    }
}