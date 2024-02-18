using PostSync.Core.Models;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;
using PostSync.Infrastructure.Queries;

namespace PostSync.Infrastructure.Services.Integrations;

public class PageSessionService : IPageSessionService
{
    private readonly IDbContext _dbContext;

    public PageSessionService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> AddPageSession(PageSessionModel pgModel)
    {
        var res = await _dbContext.ExecuteAsync(PageSessionQueries.INSERT_SESSION, pgModel);

        return res != 0;
    }

    public async Task<bool> UpdatePageSession(int id,  string accessToken)
    {
        var pageModel = new PageSessionModel()
        {
            Id = id,
            AccessToken = accessToken
        };

        var res = await _dbContext.ExecuteAsync(PageSessionQueries.UPDATE_SESSION, pageModel);
        return res != 0;
    }

    public async Task<bool> DeletePageSession(int id)
    {
        var res = await _dbContext.ExecuteAsync(PageSessionQueries.DELETE_SESSION, new { id });
        return res != 0;
    }

    public async Task<List<PageSessionModel>> GetPageSessionModels(int userId)
    {
        var res = await _dbContext.GetAllAsync<PageSessionModel>(PageSessionQueries.GET_SESSION, new { userId });
        return res;
    }

    public async Task<PageSessionModel> GetPageSessionByPageId(int userId, string pageId)
    {
        var res = await _dbContext.GetAsync<PageSessionModel>(PageSessionQueries.GET_SESSION_BY_PAGE, new {userId, pageId });
        return res;
    }
    

}