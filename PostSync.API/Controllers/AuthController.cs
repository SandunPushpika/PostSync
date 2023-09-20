using System.Net;
using Microsoft.AspNetCore.Mvc;
using PostSync.Core.Domain.Requests;
using PostSync.Core.Interfaces;
using PostSync.Core.Services;
using PostSync.Infrastructure.Services;
using HttpResponse = PostSync.Core.Domain.Responses.HttpResponse;

namespace PostSync.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly ResponseService _responseService;
    private readonly IAuthService _authService;
    
    public AuthController(IUserService userService, ResponseService responseService, IAuthService authService)
    {
        _userService = userService;
        _responseService = responseService;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var res = await _authService.Login(request);

        return _responseService.ReplyResponse(res);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody]UserCreateRequest createRequest)
    {
        var res = await _userService.CreateUser(createRequest);

        return _responseService.ReplyResponse(new HttpResponse("User created!",true,(int)HttpStatusCode.Created));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokens(string token)
    {
        var res = await _authService.RefreshToken(token);

        return _responseService.ReplyResponse(res);
    }
}