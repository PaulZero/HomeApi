using System.Threading.Tasks;
using HomeApi.Web.Services.Lighting;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Web.Controllers
{
    public class LightingController : ControllerBase
    {
        private readonly ILightingService lighting;

        public LightingController(ILightingService lightingService)
        {
            lighting = lightingService;
        }

        [Route("/api/lighting/register")]
        public async Task<IActionResult> Register()
        {
            await lighting.RegisterAsync();

            return Ok();
        }
    }
}