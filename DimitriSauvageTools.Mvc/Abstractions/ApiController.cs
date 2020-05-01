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
        protected TService Service { get; }

        #endregion

        #region Constructors

        protected ApiController(TService service)
        {
            this.Service = service;
        }

        #endregion
    }

    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]", Name = "BaseApiRoute_[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}