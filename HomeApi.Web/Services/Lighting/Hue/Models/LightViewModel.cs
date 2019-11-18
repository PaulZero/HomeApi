using HueLight = Q42.HueApi.Light;
using BaseLight = HomeApi.Libraries.Models.Lighting.Light;

namespace HomeApi.Web.Services.Lighting.Hue.Models
{
    public class LightViewModel : BaseLight
    {
        public LightViewModel(HueLight light)
        {
            Id = light.Id;
            Name = light.Name;
            Brightness = light.State.Brightness;
            ColourMode = light.State.ColorMode;
            ColourTemperature = light.State.ColorTemperature;
            ColourCoordinates = light.State.ColorCoordinates;
            IsOn = light.State.On;
            IsReachable = light.State.IsReachable;
            Hue = light.State.Hue;
            Saturation = light.State.Saturation;
            TransitionMilliseconds = light.State.TransitionTime?.Milliseconds;
        }
    }
}
