using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.Application.Abstractions;
using Tools.Domain.Abstractions;
using Tools.Helpers;
using Tools.Infrastructure.Abstraction;

namespace Tools.Mvc.Abstractions
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
    [Route("api")]
    public abstract class ApiController : ControllerBase
    {
    }
}