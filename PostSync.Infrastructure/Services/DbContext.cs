using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using PostSync.Core.Helpers.Configs;
using PostSync.Core.Services;

namespace PostSync.Infrastructure.Services;

public class DbContext : IDbContext
{
    private readonly AppConfig _config;
    private readonly IDbConnection _connection;
    
    public DbContext(IOptions<AppConfig> options)
    {
        _config = options.Value;
        _connection = new NpgsqlConnection(_config.ConnectionStrings.Postgres);
    }

    public async Task<List<T>> GetAllAsync<T>(string query, object data)
    {
        var result = await _connection.QueryAsync<T>(query, data);

        return result.ToList();
    }

    public async Task<T> GetAsync<T>(string query, object data)
    {
        var result = _connection.QueryAsync<T>(query, data).Result.FirstOrDefault();

        return result;
    }

    public async Task<int> ExecuteQueryAsync(string query, object data)
    {
        var result = _connection.QueryAsync<int>(query, data).Result.FirstOrDefault();

        return result;
    }

    public async Task<int> ExecuteAsync(string query, object data)
    {
        var result = await _connection.ExecuteAsync(query, data);

        return result;
    }
    
}