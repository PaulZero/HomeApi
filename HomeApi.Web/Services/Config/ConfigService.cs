using System;
using System.IO;
using System.Threading.Tasks;
using HomeApi.Web.Services.Lighting.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeApi.Web.Services.Config
{
    public class ConfigService : IConfigService, IDisposable
    {
        public LightingConfig Lighting { get; set; } = new LightingConfig();

        [JsonIgnore]
        private const string LogFileName = "logs/home-api-config.json";

        [JsonIgnore]
        private ILogger<ConfigService> Logger { get; }

        protected ConfigService(/*IWebHostEnvironment environment, */ILogger<ConfigService> logger)
        {
            Logger = logger;
        }

        public Task SaveAsync()
        {
            return Task.Run(Save);
        }

        public void Save()
        {
            try
            {
                //File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(this));
            }
            catch (Exception exception)
            {
                Logger.LogError($"Failed to save config file: {exception.Message}");
            }
        }
        public static ConfigService Load(/*IWebHostEnvironment environment,*/ ILogger<ConfigService> logger)
        {
            //try
            //{
            //    //var contents = File.ReadAllText(ConfigFilePath);

            //    return JsonConvert.DeserializeObject<ConfigService>(contents);
            //}
            //catch
            //{
                return new ConfigService(logger);
            //}
        }

        public void Dispose()
        {
            Save();
        }
    }
}
