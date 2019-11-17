namespace HomeApi.Web.Services.Lighting.RequestModels
{
    public class SetGroupStateRequest : AbstractStateRequest
    {
        public string[] GroupIds { get; set; }
    }
}