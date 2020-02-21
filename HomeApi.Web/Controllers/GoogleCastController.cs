using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.GoogleCast;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web.Controllers
{
    public class GoogleCastController : HomeApiController
    {
        private GoogleCastService GoogleService { get; }

        public GoogleCastController(IConfigService config, ILogger<GoogleCastController> logger, GoogleCastService googleService) : base(config, logger)
        {
            GoogleService = googleService;
        }

        [Route("/api/cast/list")]
        public async Task<IActionResult> List()
        {
            var chromecasts = await GoogleService.GetReceiverNames();

            return Ok(new
            {
                chromecasts
            });
        }
    }
}
