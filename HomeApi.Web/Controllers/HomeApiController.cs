using System;
using System.Collections.Generic;
using HomeApi.Libraries.Models.Responses;
using HomeApi.Web.Libraries.ActionFilters;
using HomeApi.Web.Services.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web.Controllers
{
    [LogRouteFilter]
    public abstract class HomeApiController : Controller
    {
        protected IConfigService Config { get; }

        protected ILogger Logger { get; }

        protected HomeApiController(IConfigService config, ILogger logger)
        {
            Config = config;
            Logger = logger;
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
            if (Config.IsDevelopmentMode) LogResponse("Bad Request", error);

            return base.BadRequest(error ?? new {error = "Null object passed to BadRequest :'("});
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

        public JsonResult ErrorResponse(Exception exception)
        {
            var response = StandardResponse(false, exception.Message);

            response.StatusCode = 400;

            return response;
        }

        public JsonResult StandardResponse(string message)
        {
            return StandardResponse(true, null, message);
        }

        public JsonResult StandardResponse(object data = null, string message = null)
        {
            return StandardResponse(true, data, message);
        }

        public JsonResult StandardResponse(bool success, string message)
        {
            return StandardResponse(success, null, message);
        }

        public JsonResult StandardResponse(bool success, object data = null, string message = null)
        {
            return Json(new StandardResponse
            {
                Data = data,
                Message = message,
                Success = success
            });
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