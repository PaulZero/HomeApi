using System;

namespace HomeApi.Web.Services.Lighting.Exceptions
{
    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException(string message = null) : base(message)
        {

        }
    }
}
