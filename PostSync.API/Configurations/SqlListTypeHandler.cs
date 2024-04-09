using System.Data;
using System.Text.Json;

namespace PostSync.Configurations;

public class SqlListTypeHandler<T> : Dapper.SqlMapper.TypeHandler<List<T>>
{
    public override void SetValue(IDbDataParameter parameter, List<T>? value)
    {
        parameter.Value = JsonSerializer.Serialize(value);
    }

    public override List<T>? Parse(object value)
    {
        return ((T[])value).ToList();
    }
}