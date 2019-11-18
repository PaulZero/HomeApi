using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeApi.Dashboard.Requests
{
    public abstract class AbstractRequest<TResponse>
    {
        public const string BaseUrl = "http://pi-server:5000";

        public abstract Task<TResponse> Execute();

        protected async Task<TResponse> PostAsync(string relativeUri, object data = null)
        {
            var request = WebRequest.CreateHttp(CreateUri(relativeUri));

            request.Method = WebRequestMethods.Http.Post;

            if (data != null)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    await streamWriter.WriteAsync(JsonConvert.SerializeObject(data));

                    request.ContentType = "application/json";
                }
            }

            var response = await request.GetResponseAsync() as HttpWebResponse;

            if (response?.GetResponseStream() == null)
            {
                // TODO: Handle this more gracefully.
                throw new Exception("Empty response returned");
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var jsonResponse = await streamReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<TResponse>(jsonResponse);
            }
        }

        private static Uri CreateUri(string relativeUri)
        {
            if (!relativeUri.StartsWith("/"))
            {
                relativeUri = $"/{relativeUri}";
            }

            return new Uri($"{BaseUrl}{relativeUri}");
        }
    }
}
