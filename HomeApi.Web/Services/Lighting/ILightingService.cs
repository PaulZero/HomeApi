using HomeApi.Web.Services.Lighting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Web.Services.Lighting
{
    public interface ILightingService
    {
        Task RegisterAsync();

        Task<IEnumerable<Light>> GetLightsAsync();

        Task TurnOnAsync(string id);

        Task TurnOffAsync(string id);
    }
}
