namespace Integration.Core.Interfaces;

public interface IHttpHelper
{
    Task<T> SendPost<T>(string url, object obj = null);
    Task<T> SendGet<T>(string url, object obj = null);
}