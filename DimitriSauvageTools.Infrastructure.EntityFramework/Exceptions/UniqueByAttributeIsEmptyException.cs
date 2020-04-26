using DimitriSauvageTools.Exceptions;

namespace Nestor.DimitriSauvageTools.Infrastructure.EntityFramework.Exceptions
{
    public class UniqueByAttributeIsEmptyException<T> : AppException
    {
        public UniqueByAttributeIsEmptyException() : base(
            $"The entity type {typeof(T).Name} has has a UniqueBy attribute, but its configuration is empty")
        {
        }
    }
}