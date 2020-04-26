using DimitriSauvageTools.Exceptions;

namespace Nestor.DimitriSauvageTools.Infrastructure.EntityFramework.Exceptions
{
    public class OutsideTransactionalContext : AppException
    {
        public OutsideTransactionalContext() : base("You must be in transactional context")
        {
        }
    }
}