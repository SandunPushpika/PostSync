using Integration.Core.Interfaces.OAuth;
using Flurl;
using Microsoft.Extensions.Options;
using PostSync.Core.Helpers.Configs;

namespace Integration.Infrastructure.Services.OAuth;

public class FacebookOAuth : IFacebookOAuth
{
    private readonly IntegrationAppConfig _config;
    private string _oAuthUrl = "https://www.facebook.com/v18.0/dialog/oauth";

    public FacebookOAuth(IOptions<IntegrationAppConfig> options)
    {
        _config = options.Value;
    }
    
    
    public string GetOAuthUrl()
    {
        var url = _oAuthUrl.SetQueryParam("client_id", _config.Facebook.ClientId)
            .SetQueryParam("redirect_uri",_config.Facebook.RedirectUrl)
            .SetQueryParam("state", "123njnsdf72342nc7")
            .SetQueryParam("scope",_config.Facebook.Permissions);

        return url;
    }

    public Task<T> GetTokens<T>()
    {
        throw new NotImplementedException();
    }

    public Task<T> RefreshTokens<T>()
    {
        throw new NotImplementedException();
    }

    public Task<T> RevokeToken<T>()
    {
        throw new NotImplementedException();
    }
}