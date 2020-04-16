using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tools.Mvc.Abstractions
{
    [Controller]
    [Authorize]
    public abstract class BaseController : Controller
    {
    }
}
