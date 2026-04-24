using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public sealed class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new BadRequestObjectResult(new { error = "System error occurred." });
        context.ExceptionHandled = true;
    }
}