using System.Net;

namespace PostSync.Core.Services;

public interface IHttpClientHelper
{
    Task<T?> Post<T>(string url, object content);
    Task<T?> Post<T>(string url, object content, string tokenHeader, string token);
    Task<T?> Put<T>(string url, Stream content, string contentType, string tokenHeader, string token);
    Task<T> PostAsFormData<T>(string url, List<KeyValuePair<string, string>> content);
    Task<T> Get<T>(string url);
    Task<T> Get<T>(string url, string tokenHeader, string token);
    Task<HttpStatusCode> PostWithBearerToken(string url, string tokenHeader, string token);
    Task<HttpStatusCode> Delete(string url, string tokenHeader, string token);
}