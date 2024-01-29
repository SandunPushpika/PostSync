using PostSync.Core.DTOs.Requests;
using PostSync.Core.Models;

namespace PostSync.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUser(UserCreateRequest createRequest);
    Task<UserModel> GetByUsername(string username);
}