namespace PostSync.Configurations;

public class SqlMappers
{
    public static void MapObjects()
    {
        Dapper.SqlMapper.AddTypeHandler(new SqlListTypeHandler<string>());
    }
}