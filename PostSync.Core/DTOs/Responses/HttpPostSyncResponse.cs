namespace PostSync.Core.DTOs.Responses;

public class HttpPostSyncResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }

    public HttpPostSyncResponse(string message, bool success, int statusCode)
    {
        Message = message;
        Success = success;
        StatusCode = statusCode;
    }
    
    public HttpPostSyncResponse(){}
    
}