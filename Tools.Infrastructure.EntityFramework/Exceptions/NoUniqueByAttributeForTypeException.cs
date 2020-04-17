using Tools.Exceptions;

namespace Tools.Infrastructure.EntityFramework.Exceptions
{
    public class NoUniqueByAttributeForTypeException<T> : AppException
    {
        public NoUniqueByAttributeForTypeException() : base(
            $"The entity type {typeof(T).Name} has no UniqueBy attribute, so Repository.GetByUniqueKey({typeof(T).Name}) function was not available.")
        {
        }
    }
}