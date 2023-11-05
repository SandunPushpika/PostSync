using System.Text;
using System.Text.Json;
using Integration.Core.Interfaces;

namespace Integration.Infrastructure.Services;

public class HttpHelper : IHttpHelper
{

    private readonly HttpClient _client;

    public HttpHelper()
    {
        _client = new();
    }

    public async Task<T> SendPost<T>(string url, object obj = null)
    {
        var message = new HttpRequestMessage(HttpMethod.Post, url);
        if (obj != null)
        {
            message.Content = new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }

        var res = await _client.SendAsync(message);
        var content = res.Content.ReadAsStringAsync().Result;

        if (!res.IsSuccessStatusCode) return default;
        
        var resultObject = JsonSerializer.Deserialize<T>(content);

        return resultObject;
    }
    
    public async Task<T> SendGet<T>(string url, object obj = null)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, url);

        var res = await _client.SendAsync(message);
        var content = res.Content.ReadAsStringAsync().Result;

        if (!res.IsSuccessStatusCode) return default;
        
        var resultObject = JsonSerializer.Deserialize<T>(content);

        return resultObject;
    }
    
    
}