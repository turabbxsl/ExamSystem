using System.Net;
using System.Text.Json;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = ex switch
        {
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Authorization failed. Access is denied."),
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),

            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred on the server.")
        };

        context.Response.StatusCode = (int)statusCode;

        var result = Result<object>.Failure(message, (int)statusCode);

        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        var response = new
        {
            result.IsSuccess,
            result.Error,
            result.StatusCode,
            Detail = isDevelopment ? ex.Message : null,
            StackTrace = isDevelopment ? ex.StackTrace : null
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}