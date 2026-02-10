using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyRent.Models;
using System.Diagnostics;

namespace MyRent.Controllers
{
    public class ErrorController : Controller
    {


        private readonly ILogger<ObjectController> _logger;

        public ErrorController(ILogger<ObjectController> logger) {
            _logger = logger; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            string errorMessage = "Server error occured";

            if (exceptionHandlerPathFeature?.Error != null)
            {
                var exception = exceptionHandlerPathFeature.Error;

                _logger.LogError("Exception occured. Message: {Message}", exception.Message);

                errorMessage = exception.Message;
            }

            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ExceptionMessage = errorMessage 
            });
        }
    }
}
