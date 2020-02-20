using System;

namespace HomeApi.Web.Services.Lighting.Exceptions
{
    public class UnknownLightException : Exception
    {
        public UnknownLightException(string id) : base($"The light '{id}' is not recognised.")
        {
        }
    }
}
