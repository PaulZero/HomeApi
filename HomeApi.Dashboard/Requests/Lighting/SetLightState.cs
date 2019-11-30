using System;
using System.Threading.Tasks;
using HomeApi.Libraries.Models.Requests;

namespace HomeApi.Dashboard.Requests.Lighting
{
    public class SetLightState : AbstractRequest
    {
        public async Task Execute(SetLightStateRequest request)
        {
            try
            {
                await PostAsync("/api/lights/set-light-state", request);
            }
            catch (Exception exception)
            {
                var kek = 5;
            }
        }
    }
}
