// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataCore.Sql.Models;

[Serializable]
public class SerializeDeprecatedModel<T> where T : new()
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

    #endregion

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

    private static string GetContentType(FormatTypeEnum format) => format switch
    {
        FormatTypeEnum.Xml => "application/xml",
        FormatTypeEnum.Json => "application/json",
        FormatTypeEnum.Html => "application/html",
        FormatTypeEnum.Text => "application/text",
        FormatTypeEnum.Raw => "application/text",
        _ => throw GetArgumentException(nameof(format)),
    };

    private static ContentResult GetResultInside(FormatTypeEnum format, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = GetContentType(format),
        StatusCode = (int)statusCode,
        Content = content is string ? content as string : content.ToString()
    };

    public static ContentResult GetResult(FormatTypeEnum format, object content, HttpStatusCode statusCode) => 
        GetResultInside(format, content, statusCode);

    public ContentResult GetResult(FormatTypeEnum format, HttpStatusCode statusCode)
    {
        return format switch
        {
            FormatTypeEnum.Json => GetResult(format, XmlUtils.GetPrettyXmlOrJson(SerializeAsJson()), statusCode),
            FormatTypeEnum.Xml => GetResult(format, SerializeAsXml(), statusCode),
            FormatTypeEnum.Html => GetResult(format, SerializeAsHtml(), statusCode),
            FormatTypeEnum.Text or FormatTypeEnum.Raw => GetResult(format, SerializeAsText(), statusCode),
            _ => throw GetArgumentException(nameof(format)),
        };
    }

    public string GetContent(FormatTypeEnum format)
    {
        return format switch
        {
            FormatTypeEnum.Json => XmlUtils.GetPrettyXmlOrJson(SerializeAsJson()),
            FormatTypeEnum.Xml => XmlUtils.GetPrettyXml(SerializeAsXml()),
            FormatTypeEnum.Html => SerializeAsHtml(),
            FormatTypeEnum.Text or FormatTypeEnum.Raw => SerializeAsText(),
            _ => throw GetArgumentException(nameof(format)),
        };
    }

    #endregion
}
