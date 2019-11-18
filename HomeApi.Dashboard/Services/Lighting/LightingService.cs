using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HomeApi.Dashboard.Libraries.DI;
using HomeApi.Dashboard.Requests.Lighting;
using HomeApi.Libraries.Models.Lighting;

namespace HomeApi.Dashboard.Services.Lighting
{
    public class LightingService : IService
    {
        public ReadOnlyObservableCollection<Light> Lights { get; }

        private readonly ObservableCollection<Light> _internalLights = new ObservableCollection<Light>();

        public LightingService()
        {
            Lights = new ReadOnlyObservableCollection<Light>(_internalLights);
        }

        public async Task InitialiseAsync()
        {
            await RefreshLights();
        }

        public async Task RefreshLights()
        {
            var request = new GetLightsRequest();
            var newLights = await request.Execute();

            _internalLights.Clear();

            foreach (var light in newLights)
            {
                _internalLights.Add(light);
            }
        }
    }
}
