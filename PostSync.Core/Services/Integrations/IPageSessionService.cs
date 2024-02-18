using PostSync.Core.Models;

namespace PostSync.Core.Services.Integrations;

public interface IPageSessionService
{
    Task<bool> AddPageSession(PageSessionModel pgModel);
    Task<bool> UpdatePageSession(int id, string accessToken);
    Task<bool> DeletePageSession(int id);
    Task<List<PageSessionModel>> GetPageSessionModels(int userId);
    Task<PageSessionModel> GetPageSessionByPageId(int userId, string pageId);
}