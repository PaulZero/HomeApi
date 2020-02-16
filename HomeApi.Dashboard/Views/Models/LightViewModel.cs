using System;
using System.ComponentModel;
using HomeApi.Dashboard.Requests.Lighting;
using HomeApi.Dashboard.Services.Lighting;
using HomeApi.Libraries.Models.Lighting;
using HomeApi.Libraries.Models.Requests;

namespace HomeApi.Dashboard.Views.Models
{
    public class LightViewModel : Light
    {
        public bool CanUpdateLight
        {
            get => _canUpdateLight;
            set
            {
                if (value == _canUpdateLight) return;
                _canUpdateLight = value;
                NotifyPropertyChanged();
            }
        }

        protected Light Light { get; }

        protected LightingService LightingService { get; }

        private bool _canUpdateLight;

        public LightViewModel(Light light, LightingService lightingService)
        {
            Light = light;
            LightingService = lightingService;

            UpdateFrom(light, true);

            PropertyChanged += OnPropertyChanged;

            CanUpdateLight = true;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!CanUpdateLight) return;

            if (e.PropertyName == nameof(IsOn))
            {
                CanUpdateLight = false;

                UpdateLight();
            }
            else if (e.PropertyName == nameof(Brightness))
            {
                UpdateLight();
            }
        }

        private async void UpdateLight()
        {
            //            CanUpdateLight = false;

            try
            {
                var request = new SetLightState();

                await request.Execute(new SetLightStateRequest
                {
                    PowerState = IsOn,
                    Brightness = Brightness,
                    LightIds = new[] { Id }
                });
            }
            catch
            {

            }
            finally
            {
                CanUpdateLight = true;
            }
        }
    }
}