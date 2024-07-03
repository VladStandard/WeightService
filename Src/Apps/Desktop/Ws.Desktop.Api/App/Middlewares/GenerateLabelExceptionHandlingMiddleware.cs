using System.Net;
using System.Text.Json;
using Ws.Desktop.Models.Shared;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
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
        catch (LabelGenerateException e)
        {
            logger.LogError(e, "{ECode}", e.Code);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            ServerException problem = new()
            {
                MessageLocalizeKey = EnumHelper.GetEnumDescription(e.Code)
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}