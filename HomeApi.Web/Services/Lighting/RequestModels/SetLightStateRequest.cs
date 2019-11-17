using System;
using System.ComponentModel.DataAnnotations;

namespace HomeApi.Web.Services.Lighting.RequestModels
{
    public class SetLightStateRequest : AbstractStateRequest
    {
        

        public string[] LightIds { get; set; }
    }
}
