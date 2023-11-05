namespace PostSync.Core.Helpers.Configs;

public class IntegrationAppConfig
{
    public Connections ConnectionStrings { get; set; }
    public JwtDetails Jwt { get; set; }
    public RabbitMqDetails RabbitMq { get; set; }
    public OAuthConfig Facebook { get; set; }
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

public class RabbitMqDetails
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}