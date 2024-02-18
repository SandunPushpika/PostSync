using PostSync.Core.Models;

namespace PostSync.Core.Services.Integrations;

public interface IIntegrationService
{
   Task<int> AddIntegrationSession(IntegrationSessionModel model);
   Task<bool> UpdateIntegrationSession(IntegrationSessionModel model, int id);
   Task<bool> DeleteIntegrationSession(int id);
   Task<List<IntegrationSessionModel>> GetIntegrations(int userId);
   Task<IntegrationSessionModel> GetIntegrationById(int userId, int id);
   Task<IntegrationSessionModel> GetIntegration(int userId, Platform platform,string email);
}