using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PostSync.Infrastructure.Services;

public class HttpClientHelper
{
    private readonly HttpClient _client;

    public HttpClientHelper()
    {
        _client = new();
    }

    public async Task<T?> Post<T>(string url, object content)
    {
        var serializedContent = JsonSerializer.Serialize(content);
        var JsonContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
        
        var res = await _client.PostAsync(url, JsonContent);
        var httpResponse = res.Content.ReadAsStringAsync().Result;

        var responseDTO = JsonSerializer.Deserialize<T>(httpResponse);

        return responseDTO;
    }
    
    public async Task<T?> Post<T>(string url, object content, string tokenHeader, string token)
    {
        var serializedContent = JsonSerializer.Serialize(content);
        var JsonContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
        
        using (var request = new HttpRequestMessage(HttpMethod.Post, url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(tokenHeader, token);
            request.Content = JsonContent;
            
            var res = await _client.SendAsync(request);
            
            if (res.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception("Access forbidden!");
            
            if (res.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Unauthorized access!");

            if (res.StatusCode == HttpStatusCode.MethodNotAllowed)
                throw new Exception("Incompatible action!");

            var httpResponse = res.Content.ReadAsStringAsync().Result;
            
            if (res.StatusCode == HttpStatusCode.Created || res.StatusCode == HttpStatusCode.OK ||
                res.StatusCode == HttpStatusCode.Accepted)
            {
                var responseDTO = JsonSerializer.Deserialize<T>(httpResponse);

                return responseDTO;
            }

            return default;
        }
    }
    
    public async Task<T?> Put<T>(string url, Stream content, string contentType,string tokenHeader, string token)
    {
        using (var request = new HttpRequestMessage(HttpMethod.Put, url))
        {
            using var requestContent = new StreamContent(content);
            requestContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            
            request.Headers.Authorization = new AuthenticationHeaderValue(tokenHeader, token);
            request.Content = requestContent;
            
            var res = await _client.SendAsync(request);
            
            if (res.StatusCode == HttpStatusCode.BadRequest)
                throw new Exception("Bad Request!");
            
            if (res.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception("Access forbidden!");
            
            if (res.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Unauthorized access!");

            if (res.StatusCode == HttpStatusCode.MethodNotAllowed)
                throw new Exception("Incompatible action!");

            var httpResponse = res.Content.ReadAsStringAsync().Result;
            
            if (res.StatusCode == HttpStatusCode.Created || res.StatusCode == HttpStatusCode.OK ||
                res.StatusCode == HttpStatusCode.Accepted)
            {
                var responseDTO = JsonSerializer.Deserialize<T>(httpResponse);

                return responseDTO;
            }

            return default;
        }
    }

    public async Task<T> PostAsFormData<T>(string url, List<KeyValuePair<string,string>> content)
    {
        var dataContent = new FormUrlEncodedContent(content);
        var response = await _client.PostAsync(url, dataContent);

        var resultString = response.Content.ReadAsStringAsync().Result;
        var responseDTO = JsonSerializer.Deserialize<T>(resultString);

        return responseDTO;
    }

    public async Task<T> Get<T>(string url)
    {
        var res = await _client.GetAsync(url);
        var httpResponse = res.Content.ReadAsStringAsync().Result;

        return JsonSerializer.Deserialize<T>(httpResponse);
    }
    
    public async Task<T> Get<T>(string url, string tokenHeader, string token)
    {
        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(tokenHeader, token);
            
            var response = await _client.SendAsync(request);
            
            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception("Access forbidden!");
            
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Unauthorized access!");
            
            if (response.StatusCode == HttpStatusCode.MethodNotAllowed)
                throw new Exception("Incompatible action!");
            
            var httpResponse = response.Content.ReadAsStringAsync().Result;
            
            return JsonSerializer.Deserialize<T>(httpResponse);
        }
    }

    public async Task<HttpStatusCode> PostWithBearerToken(string url, string tokenHeader, string token)
    {
        using (var request = new HttpRequestMessage(HttpMethod.Post, url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(tokenHeader, token);
            var response = await _client.SendAsync(request);
            return response.StatusCode;
        }
    }

    public async Task<HttpStatusCode> Delete(string url, string tokenHeader, string token)
    {
        using (var req = new HttpRequestMessage(HttpMethod.Delete, url))
        {
            req.Headers.Authorization = new AuthenticationHeaderValue(tokenHeader, token);
            var res = await _client.SendAsync(req);

            return res.StatusCode;
        }
    }
}