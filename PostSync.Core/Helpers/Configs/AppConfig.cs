namespace PostSync.Core.Helpers.Configs;

public class AppConfig
{
    public Connections ConnectionStrings { get; set; }
    public JwtDetails Jwt { get; set; }
}

public class Connections
{
    public string Postgres { get; set; }
}

public class JwtDetails
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}