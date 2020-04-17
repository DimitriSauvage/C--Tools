using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Tools.Application.Abstractions;
using Tools.Domain.Abstractions;
using Tools.Helpers;
using Tools.Infrastructure.Abstraction;

namespace Tools.Mvc.Abstractions
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api")]
    public abstract class ApiController<TEntity, TRepository, TService> : BaseController
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
        where TService : BaseService<TEntity, TRepository>
    {
        #region Fields

        /// <summary>
        /// Data manager
        /// </summary>
        protected TService Service { get; } = ServiceCollectionHelper.GetElementFromDependencyInjection<TService>();

        #endregion

        #region Constructors

        #endregion
    }
}