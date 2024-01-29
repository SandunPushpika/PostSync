using PostSync.Core.DTOs.Requests;
using PostSync.Core.DTOs.Responses;

namespace PostSync.Core.Services;

public interface IAuthService
{
    Task<TokenResponse> Login(UserLoginRequest request);
    Task<TokenResponse> RefreshToken(string token);
}