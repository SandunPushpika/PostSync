using System.Net;
using Microsoft.AspNetCore.Mvc;
using PostSync.Core.DTOs.Responses;

namespace PostSync.Infrastructure.Services;

public class ResponseService
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public ActionResult ReplyResponse<T>(T model, int statusCode = 200)
    {
        switch (StatusCode)
        {
            case HttpStatusCode.Unauthorized:
                return new ObjectResult(new HttpPostSyncResponse()
                    { Message = Message, StatusCode = (int)StatusCode, Success = false })
                    {StatusCode = (int)StatusCode};
            
            case HttpStatusCode.Forbidden:
                return new ObjectResult(new HttpPostSyncResponse()
                        { Message = Message, StatusCode = (int)StatusCode, Success = false })
                    {StatusCode = (int)StatusCode};
            
            case HttpStatusCode.NotFound:
                return new ObjectResult(new HttpPostSyncResponse()
                        { Message = Message, StatusCode = (int)StatusCode, Success = false })
                    {StatusCode = (int)StatusCode};
            
            default:
                return new ObjectResult(model) { StatusCode = statusCode };
            
        }
    }  
    
}