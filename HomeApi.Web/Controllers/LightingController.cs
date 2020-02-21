using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Libraries.Models.Requests;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting;
using HomeApi.Web.Services.Lighting.Hue.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
                var groups = await lighting.GetGroupsAsync() ?? new GroupViewModel[0];

                return StandardResponse(true, groups);
            }
            catch (Exception exception)
            {
                return ErrorResponse(exception);
            }
        }

        [HttpGet("connection-status")]
        public async Task<IActionResult> ConnectionStatus()
        {
            try
            {
                var isConnected = await lighting.GetConnectionStatusAsync();

                return StandardResponse(true, new { isConnected });
            }
            catch (Exception exception)
            {
                return ErrorResponse(exception);
            }
        }

        [HttpGet("list-lights")]
        public async Task<IActionResult> ListLights()
        {
            try
            {
                var lights = await lighting.GetLightsAsync() ?? new LightViewModel[0];

                return StandardResponse(lights);
            }
            catch (Exception exception)
            {
                return ErrorResponse(exception);
            }
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            try
            {
                await lighting.RegisterAsync();

                return StandardResponse("Hue has been successfully registered.");
            }
            catch (Exception exception)
            {
                return ErrorResponse(exception);
            }
        }

        [HttpPost("set-group-state")]
        public async Task<IActionResult> SetGroupState([FromBody] SetGroupStateRequest request)
        {
            try
            {
                await lighting.SetGroupStateAsync(request);

                return StandardResponse();
            }
            catch (Exception exception)
            {
                return ErrorResponse(exception);
            }
        }

        [HttpOptions("set-light-state")]
        public IActionResult SetLightStateOptions()
        {
            return Ok();
        }

        [HttpPost("set-light-state")]
        public async Task<IActionResult> SetLightState([FromBody] SetLightStateRequest request)
        {
            try
            {
                await lighting.SetLightStateAsync(request);

                return StandardResponse();
            }
            catch (Exception exception)
            {
                return ErrorResponse(exception);
            }
        }
    }
}
