using PostSync.Core.DTOs.Responses;

namespace PostSync.Core.Services.Integrations;

public interface IFbOauthService : IIntegrations
{
    Task<FacebookPageTokens> GetPageTokens(string accessToken);
}