using System;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting;
using HomeApi.Web.Services.Lighting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Drawing;

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

        [Route("/api/lights/{id}/colour/{hex}")]
        public async Task<IActionResult> ChangeLightColour(string id, string hex)
        {
            try
            {
                Color colour = HexToColor(hex);
                await lighting.ChangeLightColour(id, colour);

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Convert a six-letter hexadecimal string to a Color
        /// </summary>
        /// 
        /// Does not strip any prefixes
        ///
        /// <param name="hex">The hex string</param>
        /// <returns>The Color object represented by the string</returns>
        private static Color HexToColor(string hex)
        {
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);

            return Color.FromArgb(r, g, b);
        }
    }
}
