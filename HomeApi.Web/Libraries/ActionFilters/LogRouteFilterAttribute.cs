using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web.Libraries.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class LogRouteFilterAttribute : ActionFilterAttribute
    {
        private DateTime _startTime;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var logger =
                context.HttpContext.RequestServices.GetService(typeof(ILogger<LogRouteFilterAttribute>)) as ILogger;

            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;
            var duration = DateTime.Now - _startTime;

            logger.LogInformation($"Executed {controllerName}.{actionName} in {duration.TotalSeconds} seconds");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logger =
                context.HttpContext.RequestServices.GetService(typeof(ILogger<LogRouteFilterAttribute>)) as ILogger;

            _startTime = DateTime.Now;

            var controllerName = context.Controller.GetType().Name;
            var actionName = context.ActionDescriptor.DisplayName;

            logger.LogInformation($"Executing {controllerName}.{actionName}");
        }
    }
}