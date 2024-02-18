using System.Text.Json.Serialization;

namespace PostSync.Core.DTOs.Responses;

public class FacebookPageTokens
{
    [JsonPropertyName("data")]
    public List<FbPageToken> Data { get; set; }
    
    [JsonPropertyName("paging")]
    public FbPaging Paging { get; set; }   
}

public class FbPageToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public class FbPaging
{
    public FbCursor Cursors { get; set; }
}

public class FbCursor
{
    public string Before { get; set; }
    
    public string After { get; set; }
}