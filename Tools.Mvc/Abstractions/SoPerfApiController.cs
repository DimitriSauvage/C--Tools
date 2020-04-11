using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Http.Extensions;

namespace Tools.Mvc.Abstractions
{
    public abstract class SoPerfApiController : Controller
    {
        public SoPerfApiController()
        {
            
        }

        /// <summary>
        /// Créé un <see cref="ObjectResult"/> avec un code erreur 500
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ObjectResult InternalServerError(object content)
        {
            return StatusCode((int)StatusCodes.Status500InternalServerError, content);
        }
    }
}
