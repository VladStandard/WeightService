// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace WebApiTerra1000.Utils;

[Serializable]
public class SerializeDeprecatedModel<T> where T : new()
{
    #region Public and private methods

    public static ArgumentException GetArgumentException(string argument) => new($"Argument {argument} must be setup!");

    private static string GetContentType(FormatType formatType) => formatType switch
    {
        FormatType.Text => "application/text",
        FormatType.JavaScript => "application/js",
        FormatType.Json => "application/json",
        FormatType.Html => "application/html",
        FormatType.Xml => "application/xml",
        _ => throw GetArgumentException(nameof(formatType)),
    };

    private static string GetContentType(string format) => GetContentType(DataUtils.GetFormatType(format));

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