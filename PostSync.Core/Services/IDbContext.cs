namespace PostSync.Core.Services;

public interface IDbContext
{
    Task<List<T>> GetAllAsync<T>(string query, object data);
    Task<T> GetAsync<T>(string query, object data);
    Task<int> ExecuteQueryAsync(string query, object data);
    Task<int> ExecuteAsync(string query, object data);
}