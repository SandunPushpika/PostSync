namespace PostSync.Core.Domain.Responses;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}