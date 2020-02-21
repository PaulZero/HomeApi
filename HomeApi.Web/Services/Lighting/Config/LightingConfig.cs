using System;

namespace HomeApi.Web.Services.Lighting.Config
{
    public class LightingConfig
    {
        public string HueAppKey { get; set; }

        public TimeSpan TransitionTime { get; set; } = TimeSpan.FromMilliseconds(200);

        public TimeSpan SearchTimeout { get; set; } = TimeSpan.FromSeconds(5);
    }
}
