using Tools.Exceptions;

namespace Tools.Infrastructure.Exceptions
{
    public class UnexpectedTransiantObjectException<TEntity> : AppException
    {
        public UnexpectedTransiantObjectException() : base($"L'objet de type {typeof(TEntity).Name} est transiant et son id vaut 0. Celui-ci doit disposer d'un identifiant positif pour pouvoir être utilisé.")
        {

        }
    }
}
