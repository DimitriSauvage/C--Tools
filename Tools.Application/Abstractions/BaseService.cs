using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tools.Domain.Abstractions;
using Tools.Infrastructure.Abstraction;

namespace Tools.Application.Abstractions
{
    public abstract class BaseService<TEntity, TRepository> where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        /// <summary>
        /// Data manager
        /// </summary>
        protected TRepository Repository { get; }

        /// <summary>
        /// Mapper
        /// </summary>
        protected IMapper Mapper { get; }

        #region Constructors

        protected BaseService()
        {
            //Get the service collection
            IServiceCollection serviceCollection = new ServiceCollection();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            //Get the values
            this.Repository = serviceProvider.GetService<TRepository>();
            this.Mapper = serviceProvider.GetService<IMapper>();
        }

        #endregion
    }
}