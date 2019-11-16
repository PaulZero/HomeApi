using System.Threading.Tasks;
using HomeApi.Web.Services.Lighting;
using HomeApi.Web.Services.Lighting.Exceptions;
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
            try
            {
                await lighting.RegisterAsync();

                return Ok(new
                {
                    Success = true
                });
            } catch (RegistrationFailedException exception)
            {
                return BadRequest(new
                {
                    Success = false,
                    Details = exception.Message
                });
            }
        }

        public async Task<IActionResult> TurnLightOn()
        {

        }
    }
}