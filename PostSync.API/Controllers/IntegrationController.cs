using System.Net;
using Microsoft.AspNetCore.Mvc;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Services.Integrations;
using PostSync.Infrastructure.Services;

namespace PostSync.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class IntegrationController : Controller
{
    private readonly IFacebookService _facebookService;
    private readonly ResponseService _response;

    public IntegrationController(IFacebookService facebookService, ResponseService response)
    {
        _facebookService = facebookService;
        _response = response;
    }

    [HttpGet("url/{platform}")]
    public async Task<IActionResult> GetOAuthUrl(string platform)
    {
        switch (platform)
        {
            case "facebook":case "Facebook":
                var res = await _facebookService.GetOAuthUrl();
                return _response.ReplyResponse(res);
        }

        return _response.ReplyResponse(new HttpPostSyncResponse("Invalid Platform",false,(int)HttpStatusCode.BadRequest));
    }
    
    [HttpGet("authenticate/{platform}")]
    public async Task<IActionResult> AuthorizePlatform([FromRoute]string platform, [FromQuery] string code)
    {
        switch (platform)
        {
            case "facebook":case "Facebook":
                var res = await _facebookService.GetTokens(code);
                return _response.ReplyResponse(res);
        }

        return _response.ReplyResponse(new HttpPostSyncResponse("Invalid Platform",false,(int)HttpStatusCode.BadRequest));
    }
    
}