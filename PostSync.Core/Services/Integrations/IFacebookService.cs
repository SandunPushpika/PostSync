using PostSync.Core.DTOs.Responses;

namespace PostSync.Core.Services.Integrations;

public interface IFacebookService : IIntegrations
{
    Task<FacebookPageTokens> GetPageTokens(string accessToken);
}