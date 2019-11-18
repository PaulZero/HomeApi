using System;
using System.Threading.Tasks;
using HomeApi.Libraries.Models.Lighting;

namespace HomeApi.Dashboard.Requests.Lighting
{
    public class GetLightsRequest : AbstractRequest<Light[]>
    {
        public override async Task<Light[]> Execute()
        {
            try
            {
                return await PostAsync("/api/lights/list-lights");
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
