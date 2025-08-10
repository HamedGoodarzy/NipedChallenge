using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebApplication1.Domain
{
    public class RestClient : IRestClient
    {
        private static HttpClient _httpClient;
        public RestClient()
        {
            // Create HttpClient if it doesn't exist
            if (_httpClient == null) _httpClient = new HttpClient();
        }

        private HttpRequestMessage CreateRequest(string url, HttpMethod method, object? data = null)
        {
            if (string.IsNullOrEmpty(Constants.ApiBaseUrl)) throw new Exception("ApiBaseUrl is not set");
            if (string.IsNullOrEmpty(url)) throw new Exception("Url is not set");

            url = Constants.ApiBaseUrl + (Constants.ApiBaseUrl.EndsWith('/') ? "" : "/") + url;
            var request = new HttpRequestMessage(method, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if ((method == HttpMethod.Post | method == HttpMethod.Put) && data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return request;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var request = CreateRequest(url, HttpMethod.Get);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            var errJson = await response.Content.ReadAsStringAsync();
            //TODO Handle error correctly
            throw new Exception(errJson);
        }
        public async Task<T?> PostAsync<T>(string url, object? data)
        {
            var request = CreateRequest(url, HttpMethod.Post, data);
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            var errJson = await response.Content.ReadAsStringAsync();
            //TODO Handle error correclty
            throw new Exception(errJson);
        }
    }
    public interface IRestClient
    {
        public Task<T?> PostAsync<T>(string url, object? data);
        public Task<T?> GetAsync<T>(string url);
    }
}
