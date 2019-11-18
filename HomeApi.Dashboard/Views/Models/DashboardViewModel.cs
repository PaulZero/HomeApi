using System.Collections.ObjectModel;
using System.Windows.Input;
using HomeApi.Dashboard.Services.Lighting;
using HomeApi.Libraries.Models.Lighting;

namespace HomeApi.Dashboard.Views.Models
{
    public class DashboardViewModel : AbstractViewModel
    {
        public ReadOnlyObservableCollection<Light> Lights { get; }

        public DashboardViewModel()
        {
            Lights = CurrentApp.ServiceProvider.GetService<LightingService>().Lights;
        }
    }
}