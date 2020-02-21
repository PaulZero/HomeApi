using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HomeApi.Web.Libraries.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DisableCors
    {
        private readonly RequestDelegate _next;

        public DisableCors(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method != HttpMethods.Options)
                return _next(httpContext);

            httpContext.Response.StatusCode = 200;

            return Task.CompletedTask;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DisableCorsExtensions
    {
        public static IApplicationBuilder AnnihilateCors(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DisableCors>();
        }
    }
}
