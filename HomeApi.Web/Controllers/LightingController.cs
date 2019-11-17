using System;
using System.Threading.Tasks;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting;
using HomeApi.Web.Services.Lighting.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web.Controllers
{
    [Route("/api/lights")]
    public class LightingController : HomeApiController
    {
        private readonly ILightingService lighting;

        public LightingController(ILightingService lightingService, IConfigService config,
            ILogger<LightingController> logger) : base(config, logger)
        {
            lighting = lightingService;
        }

        [HttpGet("list-groups")]
        public async Task<IActionResult> ListGroups()
        {
            try
            {
                var groups = await lighting.GetGroupsAsync();

                return Ok(groups);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet("list-lights")]
        public async Task<IActionResult> ListLights()
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

        [HttpGet("register")]
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

        [HttpPost("set-group-state")]
        public async Task<IActionResult> SetGroupState([FromBody] SetGroupStateRequest request)
        {
            try
            {
                await lighting.SetGroupStateAsync(request);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPost("set-light-state")]
        public async Task<IActionResult> SetLightState([FromBody] SetLightStateRequest request)
        {
            try
            {
                await lighting.SetLightStateAsync(request);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}