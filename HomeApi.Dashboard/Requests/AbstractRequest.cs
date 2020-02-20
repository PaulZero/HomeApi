using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HomeApi.Libraries.Models.Responses;
using Newtonsoft.Json;

namespace HomeApi.Dashboard.Requests
{
    public abstract class AbstractRequest
    {
        public const string BaseUrl = "http://192.168.1.108";

        protected async Task<StandardResponse> GetAsync(string relativeUri)
        {
            var request = WebRequest.CreateHttp(CreateUri(relativeUri));

            request.Method = WebRequestMethods.Http.Get;

            var response = await request.GetResponseAsync() as HttpWebResponse;

            if (response?.GetResponseStream() == null)
            {
                // TODO: Handle this more gracefully.
                throw new Exception("Empty response returned");
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var jsonResponse = await streamReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<StandardResponse>(jsonResponse);
            }
        }

        protected async Task<StandardResponse> PostAsync(string relativeUri, object data = null)
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

                return JsonConvert.DeserializeObject<StandardResponse>(jsonResponse);
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
