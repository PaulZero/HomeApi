using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Libraries.Models.Lighting;
using Newtonsoft.Json;

namespace HomeApi.Dashboard.Requests.Lighting
{
    public class GetLights : AbstractRequest
    {
        public async Task<Light[]> Execute()
        {
            try
            {
                var standardResponse = await GetAsync("/api/lights/list-lights");

                if (standardResponse.Success)
                {
                    var json = standardResponse.Data.ToString();

                    var lights = JsonConvert.DeserializeObject<Light[]>(standardResponse.Data.ToString())
                        .OrderBy(l => l.Name).ToArray();

                    foreach (var light in lights)
                    {
                        Debug.WriteLine($"Fetched light: {light.Name} | Brightness: {light.Brightness} ({light.BrightnessPercentage}%");
                    }

                    return lights;
                }

                return new Light[0];
            }
            catch (Exception exception)
            {
                return new Light[0];
            }
        }
    }
}