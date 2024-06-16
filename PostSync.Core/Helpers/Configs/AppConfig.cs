namespace PostSync.Core.Helpers.Configs;

public class AppConfig
{
    public Connections ConnectionStrings { get; set; }
    public JwtDetails Jwt { get; set; }
    public Integrations Integrations { get; set; }
}

public class Connections
{
    public string Postgres { get; set; }
    public string Hangfire { get; set; }
}

public class JwtDetails
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}

public class Integrations
{
    public IntegrationInfo Facebook { get; set; }
}

public class IntegrationInfo
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scopes { get; set; }
    public string RedirectUri { get; set; }
}