using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using HomeApi.Dashboard.Services.Lighting;

namespace HomeApi.Dashboard.Views.Models
{
    internal class LightingPageViewModel : AbstractViewModel
    {
        public ReadOnlyObservableCollection<LightViewModel> Lights => _lightingService.Lights;

        private readonly LightingService _lightingService;

        public LightingPageViewModel()
        {
            var services = (Application.Current as App)?.ServiceProvider ??
                        throw new Exception("Service provider not available");

            _lightingService = services.GetService<LightingService>();
        }
    }
}
