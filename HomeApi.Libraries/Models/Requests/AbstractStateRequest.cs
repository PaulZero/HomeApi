using System;

namespace HomeApi.Libraries.Models.Requests
{
    public abstract class AbstractStateRequest
    {
        public byte? Brightness { get; set; }

        public bool PowerState { get; set; }

        public int? TransitionMilliseconds { get; set; }

        public TimeSpan? TransitionTime
        {
            get
            {
                if (TransitionMilliseconds == null) return null;

                return TimeSpan.FromMilliseconds((int)TransitionMilliseconds);
            }
        }
    }
}