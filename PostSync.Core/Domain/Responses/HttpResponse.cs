namespace PostSync.Core.Domain.Responses;

public class HttpResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }

    public HttpResponse(string message, bool success, int statusCode)
    {
        Message = message;
        Success = success;
        StatusCode = statusCode;
    }
    
    public HttpResponse(){}
    
}