using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Web.Services.Config;
using HomeApi.Web.Services.Lighting.Config;
using HomeApi.Web.Services.Lighting.Exceptions;
using HomeApi.Web.Services.Lighting.Hue.Models;
using HomeApi.Web.Services.Lighting.RequestModels;
using Microsoft.Extensions.Logging;
using Q42.HueApi;
using Q42.HueApi.Models.Bridge;
using Q42.HueApi.Models.Groups;

namespace HomeApi.Web.Services.Lighting.Hue
{
    public class HueLightingService : ILightingService
    {
        private LightingConfig Config => _configService.Config.Lighting;

        private readonly IConfigService _configService;

        private readonly ILogger _logger;

        private LocatedBridge[] _bridges;

        private LocalHueClient _client;

        public HueLightingService(IConfigService configService, ILogger<HueLightingService> logger)
        {
            _configService = configService;
            _logger = logger;
        }

        private async Task<IEnumerable<Group>> FindAllGroupsAsync()
        {
            try
            {
                var client = await GetClientAsync();

                return await client.GetGroupsAsync();
            }
            catch (Exception exception)
            {
                var message = $"Failed to load all groups from Hue Bridge: {exception.Message}";

                _logger.LogError(exception, message);

                throw new Exception(message, exception);
            }
        }

        private async Task<IEnumerable<Light>> FindAllLightsAsync()
        {
            try
            {
                var client = await GetClientAsync();

                return await client.GetLightsAsync();
            }
            catch (Exception exception)
            {
                var message = $"Failed to load all lights from Hue Bridge: {exception.Message}";

                _logger.LogError(exception, message);

                throw new Exception(message, exception);
            }
        }

        private async Task<Group> FindGroupByIdAsync(string id)
        {
            var groups = await FindAllGroupsAsync();
            var selectedGroup = groups.FirstOrDefault(g => g.Id == id);

            if (selectedGroup == null) throw new UnknownGroupException(id);

            return selectedGroup;
        }

        private async Task<LocatedBridge[]> GetBridgesAsync()
        {
            if (_bridges == null)
            {
                var locator = new HttpBridgeLocator();
                var locatedBridges = await locator.LocateBridgesAsync(Config.SearchTimeout);

                _bridges = locatedBridges.ToArray();
            }

            if (!_bridges.Any()) throw new Exception("No Hue bridges were detected on the local network.");

            return _bridges;
        }

        private async Task<LocalHueClient> GetClientAsync()
        {
            if (string.IsNullOrWhiteSpace(Config.HueAppKey)) throw new Exception("Hue has not yet been initialised.");

            if (_client == null)
            {
                var bridges = await GetBridgesAsync();

                _client = new LocalHueClient(bridges.First().IpAddress);

                _client.Initialize(Config.HueAppKey);
            }

            return _client;
        }

        public async Task SetGroupStateAsync(SetGroupStateRequest request)
        {
            var client = await GetClientAsync();
            var groups = await FindAllGroupsAsync();

            var lightIds = groups.Where(g => request.GroupIds.Contains(g.Id))
                .SelectMany(g => g.Lights)
                .Distinct();

            var command = new LightCommand
            {
                On = request.PowerState,
                Brightness = request.Brightness,
                TransitionTime = request.TransitionTime ?? Config.TransitionTime
            };

            await client.SendCommandAsync(command, lightIds);
        }

        public async Task RegisterAsync()
        {
            if (Config.HueAppKey != null) throw new Exception("HueBridge is already registered.");

            try
            {
                var bridges = await GetBridgesAsync();

                var client = new LocalHueClient(bridges.First().IpAddress);

                Config.HueAppKey = await client.RegisterAsync("HomeApi", "HomeApiServer");

                await _configService.SaveAsync();
            }
            catch (LinkButtonNotPressedException)
            {
                throw new RegistrationFailedException(
                    "The Hue bridge button must be long pressed before you attempt registration.");
            }
            catch (Exception exception)
            {
                throw new RegistrationFailedException($"The Hue bridge could not be registered: {exception.Message}");
            }
        }

        public async Task<IEnumerable<LightViewModel>> GetLightsAsync()
        {
            var lights = await FindAllLightsAsync();

            return lights.Select(l => new LightViewModel(l));
        }

        public async Task SetLightStateAsync(SetLightStateRequest request)
        {
            var client = await GetClientAsync();

            var command = new LightCommand
            {
                On = request.PowerState,
                TransitionTime = request.TransitionTime ?? Config.TransitionTime,
                Brightness = request.Brightness ?? 255
            };

            var results = await client.SendCommandAsync(command, request.LightIds);

            var kek = 6;
        }

        public async Task SetTransitionTimeAsync(TimeSpan transition)
        {
            Config.TransitionTime = transition;

            await _configService.SaveAsync();
        }

        public async Task SetSearchTimeoutAsync(TimeSpan timeout)
        {
            Config.SearchTimeout = timeout;

            await _configService.SaveAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupsAsync()
        {
            var groups = await FindAllGroupsAsync();

            return groups.Select(g => new GroupViewModel(g));
        }
    }
}