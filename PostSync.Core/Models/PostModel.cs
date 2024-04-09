using PostSync.Core.Enums;

namespace PostSync.Core.Models;

public class PostModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Time { get; set; }
    public string TimeZone { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; }
    public int IntegrationId { get; set; }
    public int UserId { get; set; }
    public PostStatus Status { get; set; }
}