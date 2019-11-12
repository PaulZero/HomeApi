using HomeApi.Web.Services.Lighting.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Web.Services.Config
{
    public interface IConfigService
    {
        LightingConfig Lighting { get; }

        void Save();

        Task SaveAsync();
    }
}
