using Microsoft.AspNetCore.Mvc;

namespace ZooProjectApi.Middleware;
public class ExceptionMiddleware
{
    private readonly RequestDelegate Next;

    public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "NE NAIDENO"),
            ArgumentException => (StatusCodes.Status400BadRequest, "NE PRAVILNYE DANNYE VVEL EBLAN"),
            _ => (StatusCodes.Status500InternalServerError, "XZ CHTO ETO ZA ERROR")
        };

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message,
            Instance = context.Request.Path
        });

    }


}
