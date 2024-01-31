namespace PostSync.Core.Services;

public interface IHttpContextService
{
    Task<int?> GetUserId();
}