using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace PostSync.Core.Helpers;

public class CustomDeserializerOpt : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (name.IsNullOrEmpty())
        {
            return name;
        }

        var newString = string.Empty;
        if (char.IsLower(name[0]))
        {
            newString = name[0].ToString().ToUpper() + name.Substring(1);
            return newString;
        }

        return name;
    }
}