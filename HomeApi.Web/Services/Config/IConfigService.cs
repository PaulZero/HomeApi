﻿using HomeApi.Web.Services.Lighting.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeApi.Web.Services.Config
{
    public interface IConfigService
    {
        Configuration Config { get; }

        bool IsDevelopmentMode { get; }

        void Save();

        Task SaveAsync();
    }
}
