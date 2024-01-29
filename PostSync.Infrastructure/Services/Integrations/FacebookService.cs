using Flurl;
using Microsoft.Extensions.Options;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Helpers;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;

namespace PostSync.Infrastructure.Services.Integrations;

public class FacebookService : IFacebookService
{
    private readonly AppConfig _configs;
    private readonly IHttpClientHelper _helper;
    
    public FacebookService(IOptions<AppConfig> options, IHttpClientHelper helper)
    {
        _configs = options.Value;
        _helper = helper;
    }
    
    public async Task<string> GetOAuthUrl()
    {
        var OauthUrl = OAuthUrls.FacebookOAuthUrl
            .AppendQueryParam("client_id", _configs.Integrations.Facebook.ClientId)
            .AppendQueryParam("redirect_uri", _configs.Integrations.Facebook.RedirectUri)
            .AppendQueryParam("state", "sdjf3246#$jji%$#*834j")
            .AppendQueryParam("response_type", "code")
            .AppendQueryParam("scope", _configs.Integrations.Facebook.Scopes);

        return OauthUrl;
    }

    public async Task<IntegrationTokens> GetTokens(string code)
    {
        var tokenUrl = OAuthUrls.FacebookOAuthTokenUrl
            .AppendQueryParam("client_id", _configs.Integrations.Facebook.ClientId)
            .AppendQueryParam("redirect_uri", _configs.Integrations.Facebook.RedirectUri)
            .AppendQueryParam("client_secret", _configs.Integrations.Facebook.ClientSecret)
            .AppendQueryParam("response_type", "token")
            .AppendQueryParam("code", code);

        var res = await _helper.Get<IntegrationTokens>(tokenUrl);
        return res;
    }

    public Task<IntegrationTokens> RefreshTokens<T>(T token)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAccess<T>(T token)
    {
        throw new NotImplementedException();
    }
}