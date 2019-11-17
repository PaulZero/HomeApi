using System;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting;
using HomeApi.Web.Services.Lighting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web.Controllers
{
    public class LightingController : HomeApiController
    {
        private readonly ILightingService lighting;

        public LightingController(ILightingService lightingService, IConfigService config, ILogger<LightingController> logger) : base(config, logger)
        {
            lighting = lightingService;
        }

        [Route("/api/lights/register")]
        public async Task<IActionResult> Register()
        {
            try
            {
                await lighting.RegisterAsync();

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [Route("/api/lights/list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var lights = await lighting.GetLightsAsync();

                return Ok(lights);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [Route("/api/lights/{id}/on")]
        public async Task<IActionResult> TurnLightOn(string id)
        {
            try
            {
                await lighting.TurnOnAsync(id);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [Route("/api/lights/{id}/off")]
        public async Task<IActionResult> TurnLightOff(string id)
        {
            try
            {
                await lighting.TurnOffAsync(id);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
