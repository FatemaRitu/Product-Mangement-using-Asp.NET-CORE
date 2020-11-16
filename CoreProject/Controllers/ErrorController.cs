using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested is not found.";
                    _logger.LogWarning($"404 error occured: Path= "
                        + $"{statusCodeResult.OriginalPath} and QueryString= "
                        + $"{statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The Path= {exceptionHandlerFeature.Path}"
                + $"Threw an exception {exceptionHandlerFeature.Error}");
            ViewBag.ExceptionPath = exceptionHandlerFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerFeature.Error.StackTrace;
            return View("Error");
        }
    }
}
