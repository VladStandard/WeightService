using System.Text.Json;

namespace Ws.DeviceControl.Api.App.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger): IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApiExceptionServer e)
        {
            logger.LogWarning(e, "{EMessage}\n{EInternal}",
                e.ErrorDisplayMessage, e.ErrorInternalMessage);

            context.Response.StatusCode = (int)e.StatusCode;

            ApiExceptionClient problem = new()
            {
                LocalizeMessage = e.ErrorDisplayMessage,
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}