using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;
using PostSync.Infrastructure.Services;

namespace PostSync.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class IntegrationController : Controller
{
    private readonly IFbOauthService _fbOauthService;
    private readonly ResponseService _response;
    private readonly IHttpContextService _context;
    private readonly IIntegrationService _integrationService;

    public IntegrationController(IFbOauthService fbOauthService, ResponseService response,
        IHttpContextService context, IIntegrationService integrationService)
    {
        _fbOauthService = fbOauthService;
        _response = response;
        _context = context;
        _integrationService = integrationService;
    }

    [HttpGet("url/{platform}")]
    public async Task<IActionResult> GetOAuthUrl(string platform)
    {
        switch (platform)
        {
            case "facebook":case "Facebook":
                var res = await _fbOauthService.GetOAuthUrl();
                return _response.ReplyResponse(res);
            case "all":case "All":case "ALL":
                var fburl = await _fbOauthService.GetOAuthUrl();
                return _response.ReplyResponse(new { facebook = fburl, instagram = "", linkedin = "" });
        }

        return _response.ReplyResponse(new HttpPostSyncResponse("Invalid Platform",false,(int)HttpStatusCode.BadRequest));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetIntegrations()
    {
        var userId = (int)await _context.GetUserId();
        var res = await _integrationService.GetConnectedPages(userId);
        return _response.ReplyResponse(res);
    }
    
    [HttpGet("authenticate/{platform}")]
    public async Task<IActionResult> AuthorizePlatform([FromRoute]string platform, [FromQuery] string code)
    {
        switch (platform)
        {
            case "facebook":case "Facebook":
                var res = await _fbOauthService.GetTokens(code);
                return _response.ReplyResponse(res);
        }

        return _response.ReplyResponse(new HttpPostSyncResponse("Invalid Platform",false,(int)HttpStatusCode.BadRequest));
    }
    
}