namespace PostSync.Core.Models;

public class PageSessionModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int UserSession { get; set; }
    public string PageId { get; set; }
    public string Name { get; set; }
    public string AccessToken { get; set; }
}