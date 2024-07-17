using System.Text;
using System.Xml.Serialization;
using Ws.PalychExchangeApi.Dto;

namespace Ws.PalychExchangeApi.Middlewares;

public class LoggingMiddleware(
    RequestDelegate next,
    ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        Stream originalBodyStream = context.Response.Body;

        using MemoryStream responseBody = new();

        context.Response.Body = responseBody;

        await next(context);

        responseBody.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(responseBody).ReadToEndAsync();

        if (context.Response is { StatusCode: 200, ContentType: "application/xml; charset=utf-8" })
        {
            if (TryDeserializeResponseDto(responseText, out var responseDto))
                if (responseDto.Errors.Count > 0)
                {
                    StringBuilder errors = new();

                    foreach (ResponseError error in responseDto.Errors)
                        errors.AppendLine($"{error.Uid} : {error.Message}\n");
                    logger.LogWarning("Endpoint: {RequestPath} \n{Errors}",  context.Request.Path, errors.ToString());
                }
        }
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
        context.Response.Body = originalBodyStream;
    }

    private static bool TryDeserializeResponseDto(string responseText, out ResponseDto responseDto)
    {
        try
        {
            using var reader = new StringReader(responseText);

            responseDto = (ResponseDto) new XmlSerializer(typeof(ResponseDto)).Deserialize(reader)!;
            return true;
        }
        catch (Exception)
        {
            responseDto = new();
            return false;
        }
    }
}