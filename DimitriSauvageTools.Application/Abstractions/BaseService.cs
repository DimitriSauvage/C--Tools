using AutoMapper;
using DimitriSauvageTools.Domain.Abstractions;
using DimitriSauvageTools.Helpers;
using DimitriSauvageTools.Infrastructure.Abstraction;

namespace DimitriSauvageTools.Application.Abstractions
{
    public abstract class BaseService<TEntity, TRepository> : BaseService
        where TEntity : class, IEntity
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

    public abstract class BaseService
    {
    }
}