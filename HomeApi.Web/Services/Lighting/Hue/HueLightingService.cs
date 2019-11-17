using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting.Config;
using HomeApi.Web.Services.Lighting.Exceptions;
using HomeApi.Web.Services.Lighting.Models;
using Q42.HueApi;
using Q42.HueApi.Models;
using Q42.HueApi.Models.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HueLight = Q42.HueApi.Light;

namespace HomeApi.Web.Services.Lighting.Hue
{
    public class HueLightingService : ILightingService
    {
        private IConfigService ConfigService { get; }

        private LightingConfig Config => ConfigService.Config.Lighting;

        private LocatedBridge[] bridges;

        private LocalHueClient client;

        public HueLightingService(IConfigService configService)
        {
            ConfigService = configService;
        }

        public async Task RegisterAsync()
        {            
            if (Config.HueAppKey != null)
            {
                throw new Exception("HueBridge is already registered.");
            }

            try
            {
                var bridges = await GetBridgesAsync();

                var client = new LocalHueClient(bridges.First().IpAddress);

                Config.HueAppKey = await client.RegisterAsync("HomeApi", "HomeApiServer");

                await ConfigService.SaveAsync();                
            }
            catch (LinkButtonNotPressedException)
            {
                throw new RegistrationFailedException("The Hue bridge button must be long pressed before you attempt registration.");
            }
            catch (Exception exception)
            {
                throw new RegistrationFailedException($"The Hue bridge could not be registered: {exception.Message}");
            }
        }

        public async Task<IEnumerable<Models.Light>> GetLightsAsync()
        {
            var client = await GetClientAsync();

            var hueLights = await client.GetLightsAsync();

            return hueLights.Select(l => new Models.Light(l.Id, l.Name));
        }

        public async Task TurnOnAsync(string id)
        {
            var client = await GetClientAsync();

            await client.SendCommandAsync(new LightCommand() { On = true, Brightness = 255, TransitionTime = Config.FadeTime }, new[] { id });
        }

        public async Task TurnOffAsync(string id)
        {
            var client = await GetClientAsync();

            await client.SendCommandAsync(new LightCommand() { On = false, TransitionTime = Config.FadeTime }, new[] { id });
        }

        private async Task<LocalHueClient> GetClientAsync()
        {
            if (string.IsNullOrWhiteSpace(Config.HueAppKey))
            {
                throw new Exception("Hue has not yet been initialised.");
            }
            
            if (client == null)
            {
                var bridges = await GetBridgesAsync();

                client = new LocalHueClient(bridges.First().IpAddress);

                client.Initialize(Config.HueAppKey);
            }

            return client;
        }

        private async Task<LocatedBridge[]> GetBridgesAsync()
        {
            if (bridges == null)
            {
                var locator = new HttpBridgeLocator();
                var locatedBridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

                bridges = locatedBridges.ToArray();
            }

            if (!bridges.Any())
            {
                throw new Exception("No Hue bridges were detected on the local network.");
            }

            return bridges;
        }

        private async Task<HueLight> GetLightById(string id)
        {
            var client = await GetClientAsync();

            var hueLights = await client.GetLightsAsync();

            return hueLights.FirstOrDefault(l => l.Id == id);
        }
    }
}
