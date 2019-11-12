using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting.Exceptions;
using Q42.HueApi;
using Q42.HueApi.Models.Bridge;
using System;
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
                var bridges = await GetBridgesAsync();

                var client = new LocalHueClient(bridges.First().IpAddress);

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

        private async Task<LocatedBridge[]> GetBridgesAsync()
        {
            if (bridges == null)
            {
                var locator = new HttpBridgeLocator();
                var locatedBridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

                bridges = locatedBridges.ToArray();
            }

            return bridges;
        }
    }
}
