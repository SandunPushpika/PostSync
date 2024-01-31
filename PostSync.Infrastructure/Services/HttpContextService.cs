using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using PostSync.Core.Services;

namespace PostSync.Infrastructure.Services;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAuthService _authService;

    public HttpContextService(IHttpContextAccessor contextAccessor, IAuthService authService)
    {
        _contextAccessor = contextAccessor;
        _authService = authService;
    }

    public async Task<int?> GetUserId()
    {
        var token = _contextAccessor.HttpContext.Request.Headers.Authorization[0].Replace("Bearer ","");
        var userId = await _authService.GetUserId(token);

        return userId;
    }
    
}