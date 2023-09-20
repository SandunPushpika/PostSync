using PostSync.Core.Domain.Requests;
using PostSync.Core.Domain.Responses;

namespace PostSync.Core.Services;

public interface IAuthService
{
    Task<TokenResponse> Login(UserLoginRequest request);
    Task<TokenResponse> RefreshToken(string token);
}