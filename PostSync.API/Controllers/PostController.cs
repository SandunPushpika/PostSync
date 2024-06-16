using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostSync.Core.DTOs.Requests;
using PostSync.Core.DTOs.Responses;
using PostSync.Core.Services;
using PostSync.Infrastructure.Services;

namespace PostSync.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PostController : Controller
{
    private readonly IPostDbService _postService;
    private readonly ResponseService _responseService;
    
    public PostController(IPostDbService postService, ResponseService responseService)
    {
        _postService = postService;
        _responseService = responseService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateRequest postCreateRequest)
    {
        var result = await _postService.CreatePost(postCreateRequest);

        return _responseService.ReplyResponse(new HttpPostSyncResponse()
        {
            Message = result != 0 ? "Success" : "Error occured",
            Success = result != 0,
            StatusCode = result != 0 ? 204 : 400
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPost()
    {
        var result = await _postService.GetAllPost();

        return _responseService.ReplyResponse(result);
    }
}