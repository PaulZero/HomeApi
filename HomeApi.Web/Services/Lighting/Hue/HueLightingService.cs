using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting.Exceptions;
using HomeApi.Web.Services.Lighting.Models;
using Q42.HueApi;
using Q42.HueApi.Models.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Web.Services.Lighting.Hue
{
    public class HueLightingService : ILightingService
    {
        private readonly IConfigService config;

        private LocatedBridge[] bridges;

        public HueLightingService(IConfigService configService)
        {
            config = configService;
        }

        public async Task RegisterAsync()
        {
            if (config.Lighting.HueAppKey != null)
            {
                return;
            }

            try
            {
                var client = await GetClientAsync();

                config.Lighting.HueAppKey = await client.RegisterAsync("HomeApi", "HomeApiServer");

                await config.SaveAsync();                
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

        private async Task<LocalHueClient> GetClientAsync()
        {
            var bridges = await GetBridgesAsync();

            return new LocalHueClient(bridges.First().IpAddress);
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

        public async Task<IEnumerable<Models.Light>> GetLightsAsync()
        {
            var client = await GetClientAsync();

            var hueLights = await client.GetLightsAsync();

            return hueLights.Select(l => new Models.Light(l.Id, l.State.On));
        }

        public Task TurnOnAsync(Models.Light light)
        {
            throw new NotImplementedException();
        }

        public Task TurnOffAsync(Models.Light light)
        {
            throw new NotImplementedException();
        }
    }
}
