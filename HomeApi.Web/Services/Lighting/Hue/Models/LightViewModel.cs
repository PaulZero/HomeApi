using Q42.HueApi;
using System;
using System.Text.Json.Serialization;

namespace HomeApi.Web.Services.Lighting.Hue.Models
{
    public class LightViewModel
    {
        [JsonIgnore]
        public TimeSpan MinTransitionTime { get; } = TimeSpan.FromMilliseconds(500);

        public string Id => light.Id;

        public string Name => light.Name;

        public byte Brightness => light.State.Brightness;

        public string ColourMode => light.State.ColorMode;

        public int? ColourTemperature => light.State.ColorTemperature;

        public double[] ColourCoordinates => light.State.ColorCoordinates;

        public bool IsOn => light.State.On;

        public bool? IsReachable => light.State.IsReachable;

        public int? Hue => light.State.Hue;

        public int? Saturation => light.State.Saturation;

        public int? TransitionMilliseconds => light.State.TransitionTime?.Milliseconds;

        private readonly Light light;

        public LightViewModel(Light light)
        {
            this.light = light;
        }
    }
}
