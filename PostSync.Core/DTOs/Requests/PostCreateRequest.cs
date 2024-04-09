namespace PostSync.Core.DTOs.Requests;

public class PostCreateRequest
{
    public string Title { get; set; }
    public DateTime Time { get; set; }
    public string TimeZone { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public int IntegrationId { get; set; }
}