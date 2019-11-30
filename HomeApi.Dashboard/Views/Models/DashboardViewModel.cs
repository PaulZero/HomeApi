using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HomeApi.Dashboard.Requests.Lighting;
using HomeApi.Dashboard.Services.Lighting;
using HomeApi.Libraries.Models.Lighting;
using HomeApi.Libraries.Models.Requests;

namespace HomeApi.Dashboard.Views.Models
{
    public class DashboardViewModel : AbstractViewModel
    {
        protected LightingService Lighting { get;  }

        public ReadOnlyObservableCollection<LightViewModel> Lights => Lighting.Lights;

        public Command ToggleLightCommand { get; }

        public DashboardViewModel()
        {
            Lighting = CurrentApp.ServiceProvider.GetService<LightingService>();

            ToggleLightCommand = new Command(ToggleLight);
        }

        private async void ToggleLight(object obj)
        {
            if (!(obj is Light light))
            {
                return;
            }
            
            var request = new SetLightState();

            await request.Execute(new SetLightStateRequest
            {
                LightIds = new[] {light.Id},
                PowerState = !light.IsOn
            });

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            await Lighting.RefreshLights();
        }
    }
}