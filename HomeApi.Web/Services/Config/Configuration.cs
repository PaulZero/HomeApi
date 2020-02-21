using HomeApi.Web.Services.Lighting.Config;

namespace HomeApi.Web.Services.Config
{
    public class Configuration
    {
        public LightingConfig Lighting { get; set; } = new LightingConfig();
    }
}
