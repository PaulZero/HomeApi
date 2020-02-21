using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using HomeApi.Dashboard.Requests.Lighting;
using HomeApi.Dashboard.Services.Lighting;
using HomeApi.Dashboard.Services.UserInteraction;
using HomeApi.Libraries.Models.Lighting;
using HomeApi.Libraries.Models.Requests;

namespace HomeApi.Dashboard.Views.Models
{
    public class DashboardViewModel : AbstractViewModel
    {
        protected LightingService Lighting { get; }

        protected IdleService IdleService { get; }

        public ReadOnlyObservableCollection<LightViewModel> Lights => Lighting.Lights;

        public Command ToggleLightCommand { get; }

        public Visibility InterfaceVisibility
        {
            get => _interfaceVisibility;
            set
            {
                if (_interfaceVisibility == value) return;

                _interfaceVisibility = value;

                NotifyPropertyChanged();
            }
        }

        private Visibility _interfaceVisibility;

        public DashboardViewModel()
        {
            Lighting = CurrentApp.ServiceProvider.GetService<LightingService>();
            IdleService = CurrentApp.ServiceProvider.GetService<IdleService>();

            IdleService.IdleChanged += IdleService_IdleChanged;

            ToggleLightCommand = new Command(ToggleLight);
        }

        private void IdleService_IdleChanged(object sender, EventArgs e)
        {
            InterfaceVisibility = IdleService.IsIdle ? Visibility.Collapsed : Visibility.Visible;
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
                LightIds = new[] { light.Id },
                PowerState = !light.IsOn
            });

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            await Lighting.RefreshLights();
        }
    }
}
