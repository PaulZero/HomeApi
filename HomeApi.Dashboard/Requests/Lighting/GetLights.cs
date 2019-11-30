using System;
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
                    var lights = JsonConvert.DeserializeObject<Light[]>(standardResponse.Data.ToString());

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