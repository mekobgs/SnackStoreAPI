using Microsoft.AspNetCore.Mvc;
using SnackStore.Web.Helpers;


namespace SnackStore.Web.Controllers
{
    
        public class BaseController : Controller
        {
            protected new IActionResult Ok()
            {
                return base.Ok(Envelope.Ok());
            }

            protected IActionResult Ok<T>(T result)
            {
                return base.Ok(Envelope.Ok(result));
            }

            protected IActionResult Error(string errorMessage)
            {
                return BadRequest(Envelope.Error(errorMessage));
            }

            protected IActionResult NotFound(string errorMessage)
            {
                return NotFound(Envelope.Error(errorMessage));
            }
        }
    
}