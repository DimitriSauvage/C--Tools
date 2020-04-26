using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DimitriSauvageTools.Application.Abstractions;
using DimitriSauvageTools.Domain.Abstractions;
using DimitriSauvageTools.Helpers;
using DimitriSauvageTools.Infrastructure.Abstraction;

namespace DimitriSauvageTools.Mvc.Abstractions
{
    public abstract class ApiController<TService> : ApiController
        where TService : BaseService
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

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Area("api")]
    public abstract class ApiController : ControllerBase
    {
    }
}