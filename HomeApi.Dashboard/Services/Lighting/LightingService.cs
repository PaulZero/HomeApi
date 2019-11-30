using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Dashboard.Libraries.DI;
using HomeApi.Dashboard.Requests.Lighting;
using HomeApi.Dashboard.Views.Models;
using HomeApi.Libraries.Models.Lighting;

namespace HomeApi.Dashboard.Services.Lighting
{
    public class LightingService : IService
    {
        public ReadOnlyObservableCollection<LightViewModel> Lights { get; }

        private readonly ObservableCollection<LightViewModel> _internalLights = new ObservableCollection<LightViewModel>();

        public LightingService()
        {
            Lights = new ReadOnlyObservableCollection<LightViewModel>(_internalLights);
        }

        public async Task InitialiseAsync()
        {
            await RefreshLights();
        }

        public async Task RefreshLights()
        {
            var request = new GetLights();
            var response = await request.Execute();

            var newLights = response.Select(l => new LightViewModel(l, this)).ToArray();

            var lightsToRemove = _internalLights.Where(light => newLights.All(newLight => newLight.Id != light.Id)).ToArray();

            foreach (var light in lightsToRemove)
            {
                _internalLights.Remove(light);
            }

            var lightsToAdd = newLights.Where(newLight => _internalLights.All(light => light.Id != newLight.Id)).ToArray();
            var lightsToUpdate = _internalLights.Where(light => newLights.Any(newLight => newLight.Id == light.Id)).ToArray();

            foreach (var light in lightsToAdd)
            {
                _internalLights.Add(light);
            }

            foreach (var light in lightsToUpdate)
            {
                light.UpdateFrom(newLights.First(l => l.Id == light.Id));
            }
        }
    }
}
