using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HomeApi.Web.Services.Config
{
    public class ConfigService : IConfigService, IDisposable
    {
        public bool IsDevelopmentMode => Environment.IsDevelopment();

        public Configuration Config { get; private set; }

        private IWebHostEnvironment Environment { get; }

        private ILogger<ConfigService> Logger { get; }

        private IFileInfo LogFile => Environment.ContentRootFileProvider.GetFileInfo("config/home-api-config.json");

        private string ConfigFilePath => LogFile.PhysicalPath;

        public ConfigService(IWebHostEnvironment environment, ILogger<ConfigService> logger)
        {
            Environment = environment;

            Logger = logger;

            RefreshConfig();
        }

        public void RefreshConfig()
        {
            try
            {
                if (LogFile.Exists)
                {
                    using var reader = new StreamReader(LogFile.CreateReadStream());

                    var configFormat = IsDevelopmentMode ? Formatting.Indented : Formatting.None;

                    Config = JsonConvert.DeserializeObject<Configuration>(reader.ReadToEnd(), new JsonSerializerSettings { Formatting = configFormat });

                    return;
                }
            }
            catch
            {
                Logger.LogError("Failed to reload config from file.");
            }

            Config = new Configuration();
        }

        public Task SaveAsync()
        {
            return Task.Run(Save);
        }

        public void Save()
        {
            try
            {
                var directory = Path.GetDirectoryName(LogFile.PhysicalPath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(Config));

                RefreshConfig();
            }
            catch (Exception exception)
            {
                Logger.LogError($"Failed to save config file: {exception.Message}");
            }
        }

        public void Dispose()
        {
            Save();
        }
    }
}
