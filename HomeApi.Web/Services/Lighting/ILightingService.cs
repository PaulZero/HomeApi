using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApi.Web.Services.Lighting.Hue.Models;
using HomeApi.Web.Services.Lighting.RequestModels;

namespace HomeApi.Web.Services.Lighting
{
    public interface ILightingService
    {
        Task<IEnumerable<GroupViewModel>> GetGroupsAsync();

        Task<IEnumerable<LightViewModel>> GetLightsAsync();

        Task RegisterAsync();

        Task SetGroupStateAsync(SetGroupStateRequest request);

        Task SetLightStateAsync(SetLightStateRequest request);

        Task SetSearchTimeoutAsync(TimeSpan timeout);

        Task SetTransitionTimeAsync(TimeSpan transition);
    }
}