using System.Net;
using System.Text;
using System.Text.Json;
using PostSync.Core.Domain.Responses;
using HttpResponse = PostSync.Core.Domain.Responses.HttpResponse;

namespace PostSync.API.Middlewears;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate request)
    {
        _next = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            ThrowError(context, e);
        }
    }

    private void ThrowError(HttpContext context, Exception e)
    {
        var response = new HttpResponse()
        {
            Message = e.Message,
            Success = false,
            StatusCode = 500
        };
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.WriteAsJsonAsync(response);
    }
    
}