using Tools.Exceptions;

namespace Nestor.Tools.Infrastructure.EntityFramework.Exceptions
{
    public class UniqueByAttributeIsEmptyException<T> : AppException
    {
        public UniqueByAttributeIsEmptyException() : base(
            $"The entity type {typeof(T).Name} has has a UniqueBy attribute, but its configuration is empty")
        {
        }
    }
}