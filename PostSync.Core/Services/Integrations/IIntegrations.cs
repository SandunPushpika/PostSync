using PostSync.Core.DTOs.Responses;

namespace PostSync.Core.Services.Integrations;

public interface IIntegrations
{
    Task<string> GetOAuthUrl();
    Task<IntegrationTokens> GetTokens(string code);
    Task<IntegrationTokens> RefreshTokens<T>(T token);
    Task<bool> RemoveAccess<T>(T token);
}