using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Laba1;

namespace Laba2
{
    public abstract class HttpClientBase
    {
        private readonly string hostUrl;
        private readonly Serializer serializer = new Serializer();
        private readonly HttpClient client = new HttpClient();

        protected HttpClientBase(string hostUrl)
        {
            this.hostUrl = hostUrl;
        }

        protected T MakeGetRequest<T>(string methodName, params (string Key, string Value)[] parameters)
        {
            var urlParameters = GetUrlParameters(parameters);
            var response = client.GetAsync($"{hostUrl}{methodName}?{urlParameters}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            var ans = serializer.DeserializeJson<T>(responseBody);

            return ans;
        }

        protected string MakeGetRequest(string methodName, params (string Key, string Value)[] parameters)
        {
            var urlParameters = GetUrlParameters(parameters);
            var response = client.GetAsync($"{hostUrl}{methodName}?{urlParameters}").Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
        }



        protected void MakePostRequest<TRequest>(string methodName, TRequest requestBody,
            params (string Key, string Value)[] parameters)
        {
            var urlParameters = GetUrlParameters(parameters);
            var requestStringContent = GetRequestStringContent(requestBody);

            var response = client.PostAsync($"{hostUrl}{methodName}?{urlParameters}", requestStringContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Плохой http status code {response.StatusCode}. Сообщение {response.ReasonPhrase}");
            }

            var task = response.Content.ReadAsStringAsync();
            task.Wait();
        }

        private StringContent GetRequestStringContent<T>(T requestObj)
        {
            var requestBody = serializer.SerializeJson(requestObj);
            var stringContent = new StringContent(requestBody, Encoding.Default, "application/json");
            return stringContent;
        }

        private string GetUrlParameters(params (string Key, string Value)[] parameters)
        {
            return string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"));
        }
    }
}