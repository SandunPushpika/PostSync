using PostSync.Core.Domain.Models;
using PostSync.Core.Domain.Requests;

namespace PostSync.Core.Interfaces;

public interface IUserService
{
    Task<bool> CreateUser(UserCreateRequest createRequest);
    Task<UserModel> GetByUsername(string username);
}