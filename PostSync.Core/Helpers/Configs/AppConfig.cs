namespace PostSync.Core.Helpers.Configs;

public class AppConfig
{
    public Connections ConnectionStrings { get; set; }
}

public class Connections
{
    public string Postgres { get; set; }
}