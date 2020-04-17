using AutoMapper;
using Tools.Domain.Abstractions;
using Tools.Helpers;
using Tools.Infrastructure.Abstraction;

namespace Tools.Application.Abstractions
{
    public abstract class BaseService<TEntity, TRepository> where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        #region Fields

        /// <summary>
        /// Data manager
        /// </summary>
        protected TRepository Repository { get; } =
            ServiceCollectionHelper.GetElementFromDependencyInjection<TRepository>();

        /// <summary>
        /// Mapper
        /// </summary>
        protected IMapper Mapper { get; } = ServiceCollectionHelper.GetElementFromDependencyInjection<IMapper>();

        #endregion
    }
}