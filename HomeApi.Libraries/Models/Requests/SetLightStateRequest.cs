namespace HomeApi.Libraries.Models.Requests
{
    public class SetLightStateRequest : AbstractStateRequest
    {
        public string[] LightIds { get; set; }
    }
}
