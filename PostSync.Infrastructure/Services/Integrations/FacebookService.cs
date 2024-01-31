using System.Net;
using AutoMapper;
using Flurl;
using Microsoft.Extensions.Options;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Helpers;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Models;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;

namespace PostSync.Infrastructure.Services.Integrations;

public class FacebookService : IFacebookService
{
    private readonly AppConfig _configs;
    private readonly IHttpClientHelper _helper;
    private readonly IHttpContextService _context;
    private readonly ResponseService _responseService;
    private readonly IMapper _mapper;
    private readonly IIntegrationService _integrationService;
    
    public FacebookService(IOptions<AppConfig> options, 
        IHttpClientHelper helper, IHttpContextService context, ResponseService responseService, IMapper mapper, IIntegrationService integrationService)
    {
        _configs = options.Value;
        _helper = helper;
        _context = context;
        _responseService = responseService;
        _mapper = mapper;
        _integrationService = integrationService;
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
        if (res?.AccessToken == null)
            return null;

        var sessionModel = _mapper.Map<IntegrationSessionModel>(res);
        sessionModel.UserId = (int) await _context.GetUserId();

        var email = await GetEmail(res.AccessToken);
        sessionModel.Email = email;

        var prevSession = await _integrationService.GetIntegration(sessionModel.UserId, Platform.FACEBOOK, email);

        if (prevSession != null)
        {
            var updated = await _integrationService.UpdateIntegrationSession(sessionModel, prevSession.Id);
            if (!updated)
            {
                _responseService.StatusCode = HttpStatusCode.BadRequest;
                _responseService.Message = "Error adding integration!";
            }
            return res;
        }
        
        var result = await _integrationService.AddIntegrationSession(sessionModel);
        if (!result)
        {
            _responseService.StatusCode = HttpStatusCode.BadRequest;
            _responseService.Message = "Error adding integration!";
        }
        
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

    private async Task<string> GetEmail(string accessToken)
    {
        var url = OAuthUrls.FacebookGraph + "me?fields=email&access_token=" + accessToken;
        var res = await _helper.Get<Dictionary<string,object>>(url);
        var email = res["email"].ToString();

        return email;
    }
}