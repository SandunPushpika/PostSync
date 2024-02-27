using System.Text.Json.Serialization;

namespace PostSync.Core.Models;

public class PageSessionModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int UserSession { get; set; }
    public string PageId { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public string AccessToken { get; set; }
    public Platform? Platform { get; set; } = null;
}