using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
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

        private readonly DispatcherTimer _refreshTimer;

        public LightingService()
        {
            Lights = new ReadOnlyObservableCollection<LightViewModel>(_internalLights);

            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10)
            };

            _refreshTimer.Tick += RefreshTimer_Tick;
        }

        public async Task InitialiseAsync()
        {
            await RefreshLights();

            _refreshTimer.Start();
        }

        private async void RefreshTimer_Tick(object sender, object e)
        {
            Debug.WriteLine("Refreshing lights...");

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
