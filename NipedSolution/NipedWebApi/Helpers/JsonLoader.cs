using System.Text.Json;

namespace WebApplication1.Helpers
{
    public static class JsonLoader
    {
        public static T LoadJson<T>(string content)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<T>(content, options)?? throw new Exception("json file could not be deserialized");
        }
    }
}
