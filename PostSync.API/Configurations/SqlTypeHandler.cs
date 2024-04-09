using System.Data;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Unicode;

namespace PostSync.Configurations;

public class SqlTypeHandler<T> : Dapper.SqlMapper.TypeHandler<T>
{
    public override void SetValue(IDbDataParameter parameter, T? value)
    {
        var json = JsonSerializer.Serialize(value);
        parameter.Value = json;
    }

    public override T? Parse(object value)
    {  
        var obj = JsonSerializer.Deserialize<T>(value.ToString());
        return obj;
    }
}