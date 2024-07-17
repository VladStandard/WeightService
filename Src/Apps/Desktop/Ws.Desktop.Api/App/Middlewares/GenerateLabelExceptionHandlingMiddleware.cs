using System.Text.Json;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Utils;

namespace Ws.Desktop.Api.App.Middlewares;

public class GenerateLabelExceptionHandlingMiddleware(
    ILogger<GenerateLabelExceptionHandlingMiddleware> logger
    ): IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApiExceptionServer e)
        {
            string exceptionStr = EnumHelper.GetEnumDescription(e.ExceptionType);

            logger.LogWarning(e, "{ECode} {EMessage}\n{EInternal}",
                exceptionStr, e.ErrorDisplayMessage, e.ErrorInternalMessage);

            context.Response.StatusCode = (int)e.StatusCode;

            ApiExceptionClient problem = new()
            {
                ErrorLocalizeKey = exceptionStr,
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}