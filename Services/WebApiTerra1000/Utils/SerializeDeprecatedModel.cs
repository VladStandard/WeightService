namespace WebApiTerra1000.Utils;

[Serializable]
public class SerializeDeprecatedModel<T> where T : new()
{
    #region Public and private methods

    public static ArgumentException GetArgumentException(string argument) => new($"Argument {argument} must be setup!");

    private static string GetContentType(WsEnumFormatType formatType) => formatType switch
    {
        WsEnumFormatType.Text => "application/text",
        WsEnumFormatType.JavaScript => "application/js",
        WsEnumFormatType.Json => "application/json",
        WsEnumFormatType.Html => "application/html",
        WsEnumFormatType.Xml => "application/xml",
        _ => throw GetArgumentException(nameof(formatType)),
    };

    private static string GetContentType(string format) => GetContentType(WsDataUtils.GetFormatType(format));

    private static ContentResult ContentResultCore(string format, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = GetContentType(format),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString()
    };

    public static ContentResult GetContentResult(string format, object content, HttpStatusCode statusCode) =>
        ContentResultCore(format, content, statusCode);

    #endregion
}