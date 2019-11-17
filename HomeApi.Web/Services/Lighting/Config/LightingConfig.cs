using System;

namespace HomeApi.Web.Services.Lighting.Config
{
    public class LightingConfig
    {
        public string HueAppKey { get; set; }

        public TimeSpan FadeTime { get; set; } = TimeSpan.FromMilliseconds(200);
    }
}
