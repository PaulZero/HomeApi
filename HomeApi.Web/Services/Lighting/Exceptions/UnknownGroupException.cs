using System;

namespace HomeApi.Web.Services.Lighting.Exceptions
{
    public class UnknownGroupException : Exception
    {
        public UnknownGroupException(string id) : base($"The group '{id}' is not recognised.")
        {
        }
    }
}
