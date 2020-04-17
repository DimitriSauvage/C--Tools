using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tools.Mvc.Abstractions
{
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [Route("api")]
    public abstract class ApiController : BaseController
    {
    }
}