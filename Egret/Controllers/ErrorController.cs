using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Egret.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            ViewBag.StatusCode = statusCode;

            switch (statusCode)
            {
                case StatusCodes.Status404NotFound:
                    ViewBag.ErrorMessage = "Sorry, the page you requested could not be found.";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                case StatusCodes.Status500InternalServerError:
                    ViewBag.ErrorMessage = "Sorry, something went wrong on the server.";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
                default:
                    ViewBag.ErrorMessage = "Sorry, something went wrong.";
                    ViewBag.RouteOfException = statusCodeData.OriginalPath;
                    break;
            }

            return View();
        }
    }
}