namespace PostSync.Core.Models;

public class IntegrationSessionModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime AtExpire { get; set; }
    public DateTime RtExpire { get; set; }
    public Platform Platform { get; set; }
}

public enum Platform
{
    FACEBOOK
}