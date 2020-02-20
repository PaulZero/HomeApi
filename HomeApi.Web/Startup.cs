using HomeApi.Web.Libraries.Middleware;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.GoogleCast;
using HomeApi.Web.Services.Lighting;
using HomeApi.Web.Services.Lighting.Hue;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeApi.Web
{
    public class Startup
    {
        private readonly string contentRoot;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            contentRoot = environment.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddLogging(logStuff =>
            {
                logStuff.AddConsole();

                logStuff.SetMinimumLevel(LogLevel.Debug);
            });

            services.AddSingleton<IConfigService>(s => new ConfigService(s.GetRequiredService<IWebHostEnvironment>(), s.GetRequiredService<ILogger<ConfigService>>()));

            services.AddSingleton<ILightingService>(s => new HueLightingService(s.GetService<IConfigService>(), s.GetService<ILogger<HueLightingService>>()));
            services.AddSingleton<GoogleCastService>(s => new GoogleCastService());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AnnihilateCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
