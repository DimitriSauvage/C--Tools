using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DimitriSauvageTools.Mvc.Abstractions
{
    [Controller]
    [Authorize]
    public abstract class BaseController : Controller
    {
    }
}
