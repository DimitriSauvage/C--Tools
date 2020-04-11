using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Mvc.Abstractions
{
    public abstract class SoPerfController : Controller
    {
        #region Methods
        /// <summary>
        /// Redirige la requête vers l'url passée en paramètre si celle-ci est locale
        /// </summary>
        /// <param name="returnUrl">Url de redirection</param>
        /// <returns></returns>
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}
