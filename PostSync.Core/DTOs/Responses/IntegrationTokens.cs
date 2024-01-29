using System.Text.Json.Serialization;

namespace PostSync.Core.DTOs.Responses;

public class IntegrationTokens
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonPropertyName("expires_in")]
    public long ATokenExpire { get; set; }
    public long RTokenExpire { get; set; }
}