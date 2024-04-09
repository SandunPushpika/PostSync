using AutoMapper;
using PostSync.Core.DTOs.Requests;
using PostSync.Core.Enums;
using PostSync.Core.Models;
using PostSync.Core.Services;
using PostSync.Infrastructure.Queries;

namespace PostSync.Infrastructure.Services;

public class PostDbService : IPostDbService
{

    private readonly IDbContext _dbContext;
    private readonly IHttpContextService _httpContext;
    private readonly IMapper _mapper;
    
    public PostDbService(IDbContext dbContext, IHttpContextService httpContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
        _mapper = mapper;
    }
    
    public async Task<int> CreatePost(PostCreateRequest postRequest)
    {
        var post = _mapper.Map<PostModel>(postRequest);
        post.UserId = (int) (await _httpContext.GetUserId())!;
        post.Status = PostStatus.IN_QUEUE;
        var res = await _dbContext.ExecuteAsync(PostQueries.INSERT_POST, post);
        return res;
    }

    public async Task<int> UpdatePost(int id, PostCreateRequest postRequest)
    {
        var post = _mapper.Map<PostModel>(postRequest);
        post.Id = id;
        var res = await _dbContext.ExecuteAsync(PostQueries.UPDATE_POST, post);
        return res;
    }

    public async Task DeletePost(int id)
    {
        await _dbContext.ExecuteAsync(PostQueries.DELETE_POST, new {Id = id});
    }

    public async Task<List<PostModel>> GetAllPost()
    {
        var UserId = (int)(await _httpContext.GetUserId())!;
        var res = await _dbContext.GetAllAsync<PostModel>(PostQueries.GET_ALL_POST,new {UserId});

        return res;
    }
}