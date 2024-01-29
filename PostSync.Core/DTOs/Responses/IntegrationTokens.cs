namespace PostSync.Core.DTOs.Responses;

public class IntegrationTokens
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ATokenExpire { get; set; }
    public DateTime RTokenExpire { get; set; }
}