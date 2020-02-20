namespace HomeApi.Libraries.Models.Requests
{
    public class SetGroupStateRequest : AbstractStateRequest
    {
        public string[] GroupIds { get; set; }
    }
}