using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Web.Controllers
{
    public class DefaultController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/api")]
        public IActionResult Api()
        {
            return Ok("Welcome to HomeApi");
        }
    }
}
