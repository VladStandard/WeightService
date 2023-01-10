// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DataCore.Sql.Models;

[Serializable]
public class SerializeDeprecatedModel<T> where T : new()
{
    #region Public and private methods

    public XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false, // не подавлять xml заголовок
        Encoding = Encoding.UTF8,   // кодировка
                                    // Какого то кипариса! эта настройка не работает
                                    // и UTF16 записывается в шапку XML
                                    // типа Visual Studio работает только с UTF16
        Indent = true,              // добавлять отступы
        IndentChars = "\t"          // сиволы отступа
    };

    public string SerializeAsJson() => JsonConvert.SerializeObject(this);

    public string SerializeAsXmlWithEmptyNamespaces()
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        XmlSerializerNamespaces emptyNamespaces = new();
        emptyNamespaces.Add(string.Empty, string.Empty);
        using StringWriter stringWriter = new();
        using (XmlWriter xw = XmlWriter.Create(stringWriter, GetXmlWriterSettings()))
        {
            xmlSerializer.Serialize(xw, this, emptyNamespaces);
        }
        return stringWriter.ToString();
    }

    public string SerializeAsXml()
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        using StringWriter stringWriter = new();
        xmlSerializer.Serialize(stringWriter, this);
        return stringWriter.ToString();
    }

    public static T DeserializeFromXml(string xml)
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
    }

    public static T DeserializeFromXmlVersion2(string xml)
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        //T result = new();
        T result;
        using (TextReader reader = new StringReader(xml))
        {
            result = (T)xmlSerializer.Deserialize(reader);
        }
        return result;
    }

    public string SerializeAsHtml() => @$"
<html>
<body>
    {this}
</body>
</html>
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public string SerializeAsText() => ToString();

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

    private static string GetContentType(string formatString) => GetContentType(DataUtils.GetFormatType(formatString));

    private static ContentResult ContentResultCore(FormatType formatType, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = GetContentType(formatType),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString()
    };

    private static ContentResult ContentResultCore(string formatString, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = GetContentType(formatString),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString()
    };

    public static ContentResult GetContentResult(string formatString, object content, HttpStatusCode statusCode) => 
        ContentResultCore(formatString, content, statusCode);

    public static ContentResult GetContentResult(FormatType formatType, object content, HttpStatusCode statusCode) => 
        ContentResultCore(formatType, content, statusCode);

    public ContentResult GetContentResult(FormatType formatType, HttpStatusCode statusCode) => formatType switch
    {
        FormatType.Text => GetContentResult(formatType, SerializeAsText(), statusCode),
        FormatType.JavaScript => GetContentResult(formatType, SerializeAsText(), statusCode),
        FormatType.Json => GetContentResult(formatType, DataFormatUtils.GetPrettyXmlOrJson(SerializeAsJson()), statusCode),
        FormatType.Html => GetContentResult(formatType, SerializeAsHtml(), statusCode),
        FormatType.Xml => GetContentResult(formatType, SerializeAsXml(), statusCode),
        _ => throw GetArgumentException(nameof(formatType)),
    };

    public ContentResult GetContentResult(string formatString, HttpStatusCode statusCode) =>
        GetContentResult(DataUtils.GetFormatType(formatString), statusCode);

    public string GetContent(FormatType formatType) => formatType switch
    {
        FormatType.Text => SerializeAsText(),
        FormatType.JavaScript => SerializeAsText(),
        FormatType.Json => DataFormatUtils.GetPrettyXmlOrJson(SerializeAsJson()),
        FormatType.Html => SerializeAsHtml(),
        FormatType.Xml => DataFormatUtils.GetPrettyXml(SerializeAsXml()),
        _ => throw GetArgumentException(nameof(formatType)),
    };

    public string GetContent(string formatString) => 
        GetContent(DataUtils.GetFormatType(formatString));

    #endregion
}
