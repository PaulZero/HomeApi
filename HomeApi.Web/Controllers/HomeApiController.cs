using HomeApi.Web.Services.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace HomeApi.Web.Controllers
{
    public abstract class HomeApiController : Controller
    {
        protected IConfigService Config { get; }

        protected ILogger Logger { get; }

        public HomeApiController(IConfigService config, ILogger logger)
        {
            Config = config;
            Logger = logger;            
        }

        public override OkResult Ok()
        {
            if (Config.IsDevelopmentMode)
            {
                LogResponse("OK");

                return base.Ok();
            }

            return base.Ok();
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            if (Config.IsDevelopmentMode)
            {
                LogResponse("OK");

                return base.Ok(value);
            }

            return base.Ok(null);
        }

        public override BadRequestResult BadRequest()
        {
            if (Config.IsDevelopmentMode)
            {
                LogResponse("Bad Request");

                return base.BadRequest();
            }

            return base.BadRequest();
        }

        public BadRequestObjectResult BadRequest(Exception exception)
        {
            return BadRequest($"{exception.GetType().Name} - {exception.Message}");
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object error)
        {
            if (Config.IsDevelopmentMode)
            {
                LogResponse("Bad Request", error);

                return base.BadRequest(error);
            }

            return base.BadRequest(new { });
        }

        private void LogResponse(string status, object value = null)
        {
            var action = RouteData.Values.GetValueOrDefault("action", "UnknownAction");
            var controller = RouteData.Values.GetValueOrDefault("controller", "UnknownController");

            if (value == null)
            {
                Logger.LogDebug($"{controller}::{action}() - {status}");

                return;
            }

            Logger.LogDebug($"{controller}::{action}() - {status} - {value}");
        }
    }
}
