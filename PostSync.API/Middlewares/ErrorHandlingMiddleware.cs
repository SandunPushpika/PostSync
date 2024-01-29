using PostSync.Core.DTOs.Responses;

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
        var response = new HttpPostSyncResponse()
        {
            Message = e.Message,
            Success = false,
            StatusCode = 500
        };
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.WriteAsJsonAsync(response);
    }
    
}