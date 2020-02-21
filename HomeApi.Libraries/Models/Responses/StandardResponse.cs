namespace HomeApi.Libraries.Models.Responses
{
    public class StandardResponse
    {
        public object Data { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }
    }
}