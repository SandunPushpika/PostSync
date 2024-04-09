using PostSync.Core.DTOs.Requests;
using PostSync.Core.Models;

namespace PostSync.Core.Services;

public interface IPostDbService
{
    public Task<int> CreatePost(PostCreateRequest post);
    public Task<int> UpdatePost(int id, PostCreateRequest post);
    public Task DeletePost(int id);
    public Task<List<PostModel>> GetAllPost();
}