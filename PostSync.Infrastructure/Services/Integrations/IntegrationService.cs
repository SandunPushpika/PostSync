using PostSync.Core.Models;
using PostSync.Core.Services;
using PostSync.Core.Services.Integrations;
using PostSync.Infrastructure.Queries;

namespace PostSync.Infrastructure.Services.Integrations;

public class IntegrationService(IDbContext dbContext, IPageSessionService pageSessionService) : IIntegrationService
{

    public async Task<int> AddIntegrationSession(IntegrationSessionModel model)
    {
        var res = await dbContext.ExecuteAsync(IntegrationQueries.INSERT_SESSION, model);

        return res;
    }

    public async Task<bool> UpdateIntegrationSession(IntegrationSessionModel model, int id)
    {
        model.Id = id;
        var res = await dbContext.ExecuteAsync(IntegrationQueries.UPDATE_INTEGRATION, model);

        return res != 0;
    }

    public Task<bool> DeleteIntegrationSession(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IntegrationSessionModel>> GetIntegrations(int userId)
    {
        var res = await dbContext.GetAllAsync<IntegrationSessionModel>(IntegrationQueries.GET_SESSION, new { userId });
        return res;
    }

    public Task<IntegrationSessionModel> GetIntegrationById(int userId, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IntegrationSessionModel> GetIntegration(int userId, Platform platform, string email)
    {
        var res = await dbContext.GetAsync<IntegrationSessionModel>(IntegrationQueries.GET_SESSION_BY_EMAIL, new
        {
            Email = email,
            UserId = userId,
            Platform = platform
        });
        return res;
    }

    public async Task<List<PageSessionModel>> GetConnectedPages(int userId)
    {
        var res = await pageSessionService.GetPageSessionModels(userId);
        return res;
    }
}