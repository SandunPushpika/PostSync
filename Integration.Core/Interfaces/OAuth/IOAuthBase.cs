namespace Integration.Core.Interfaces.OAuth;

public interface IOAuthBase
{
    string GetOAuthUrl();
    Task<T> GetTokens<T>();
    Task<T> RefreshTokens<T>();
    Task<T> RevokeToken<T>();
}