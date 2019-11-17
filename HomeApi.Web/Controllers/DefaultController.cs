using HomeApi.Web.Services.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web.Controllers
{
    public class DefaultController : HomeApiController
    {
        public DefaultController(IConfigService config, ILogger<DefaultController> logger) : base(config, logger)
        {
        }

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
