namespace WsStorageCore.Utils;

public static class WsDataFormatUtils
{
    #region Public and private fields and properties

    private static readonly char[] DigitsCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private static readonly char[] SpecialCharacters = { ' ', ',', '.', '-', '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=', '"', '№', ';', ':', '?', '/', '|', '\\', '{', '}', '<', '>' };

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get pretty formatted XML or JSON string.
    /// </summary>
    /// <param name="value"></param>
    public static string GetPrettyXmlOrJson(string value)
    {
        if (value.StartsWith("<") && value.EndsWith(">"))
            return GetPrettyXml(value);
        if (value.StartsWith("{") && value.EndsWith("}"))
            return GetPrettyJson(value);
        return value;
    }

    /// <summary>
    /// Get pretty formatted XML string.
    /// </summary>
    /// <param name="xml"></param>
    public static string GetPrettyXml(string xml)
    {
        try
        {
            return string.IsNullOrEmpty(xml) ? string.Empty : XDocument.Parse(xml).ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    /// <summary>
    /// Get pretty formatted JSON string.
    /// </summary>
    /// <param name="json"></param>
    private static string GetPrettyJson(string json) =>
        string.IsNullOrEmpty(json) ? string.Empty : JToken.Parse(json).ToString(Newtonsoft.Json.Formatting.Indented);
    
    public static string XmlReplaceNextLine(string xml) => xml.Replace("|", "\\&");

    public static string XsltTransformation(string xslInput, string? xmlInput)
    {
        if (xmlInput is null || string.IsNullOrEmpty(xmlInput)) return xslInput;

        using StringReader stringReaderXslt = new(xslInput.Trim());
        using StringReader stringReaderXml = new(xmlInput.Trim());
        using XmlReader xmlReaderXslt = XmlReader.Create(stringReaderXslt);
        using XmlReader xmlReaderXml = XmlReader.Create(stringReaderXml);
        XslCompiledTransform xslt = new();
        xslt.Load(xmlReaderXslt);
        using StringWriter stringWriter = new();
        // Use OutputSettings of xsl, so it can be output as HTML.
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings);
        xslt.Transform(xmlReaderXml, xmlWriter);
        string result = stringWriter.ToString();
        return result;
    }

    public static bool IsCyrillic(char value)
    {
        char[] cyrillic = Enumerable
            .Range(UnicodeRanges.Cyrillic.FirstCodePoint, UnicodeRanges.Cyrillic.Length)
            .Select(ch => (char)ch)
            .ToArray();
        return Array.BinarySearch(cyrillic, value) >= 0;
    }

    public static bool IsDigit(char value) => DigitsCharacters.Contains(value);

    public static bool IsSpecial(char value, bool isExcludeTop = true)
    {
        if (isExcludeTop && value == '^')
            return false;
        return SpecialCharacters.Contains(value);
    }

    public static XmlDocument XmlMerge(XmlDocument documentA, XmlDocument documentB)
    {
        if (documentA.DocumentElement is not null && documentB.DocumentElement is not null)
        {
            if (!Equals(documentA.DocumentElement.Name, documentB.DocumentElement.Name))
            {
                XmlNode xmlImport = documentA.ImportNode(documentB.DocumentElement, true);
                documentA.DocumentElement.AppendChild(xmlImport);
            }
            else
            {
                foreach (XmlNode xmlNode in documentB.DocumentElement.ChildNodes)
                {
                    XmlNode xmlImport = documentA.ImportNode(xmlNode, true);
                    documentA.DocumentElement.AppendChild(xmlImport);
                }
            }
        }
        return documentA;
    }

    #endregion
    

    #region Public and private methods - Serialize
    
    public static XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false,
        Indent = true,
        IndentChars = "\t"
    };

    public static string SerializeAsJson<T>(T item) => JsonConvert.SerializeObject(item);
    

    public static string SerializeAsXmlString<T>(ISerializable item, bool isAddEmptyNamespace, bool isUtf16)
    {
        try
        {
            XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
            using StringWriter stringWriter = isUtf16 ? new StringWriter() : new WsSqlStringWriterUtf8Model();
            switch (isAddEmptyNamespace)
            {
                case true:
                {
                    XmlSerializerNamespaces emptyNamespaces = new();
                    emptyNamespaces.Add(string.Empty, string.Empty);
                    using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, GetXmlWriterSettings());
                    xmlSerializer.Serialize(xmlWriter, item, emptyNamespaces);
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    break;
                }
                default:
                    xmlSerializer.Serialize(stringWriter, item);
                    break;
            }
            return stringWriter.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static XmlDocument SerializeAsXmlDocument<T>(ISerializable item, bool isAddEmptyNamespace, bool isUtf16)
    {
        XmlDocument xmlDocument = new();
        string xmlString = SerializeAsXmlString<T>(item, isAddEmptyNamespace, isUtf16);
        byte[] bytes = isUtf16 ? Encoding.Unicode.GetBytes(xmlString) : Encoding.UTF8.GetBytes(xmlString);
        using MemoryStream memoryStream = new(bytes);
        memoryStream.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);
        xmlDocument.Load(memoryStream);
        return xmlDocument;
    }

    public static T DeserializeFromXml<T>(string xml) =>
        DeserializeFromXml<T>(xml, Encoding.Unicode);

    public static T DeserializeFromXml<T>(string xml, Encoding encoding)
    {
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        return (T)xmlSerializer.Deserialize(new MemoryStream(encoding.GetBytes(xml)));
    }

    #endregion

    #region Public and private methods - Content

    public static string SerializeAsText(ISerializable item) => item.ToString() ?? string.Empty;

    private static ContentResult GetContentResultCore(WsEnumFormatType formatType, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = WsDataUtils.GetContentType(formatType),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString(),
    };

    private static ContentResult GetContentResult(WsEnumFormatType formatType, object content, HttpStatusCode statusCode) =>
        GetContentResultCore(formatType, content, statusCode);

    public static ContentResult GetContentResult<T>(ISerializable item, string format, HttpStatusCode statusCode) =>
        GetFormatType(format) switch
        {
            WsEnumFormatType.Text => GetContentResult(WsEnumFormatType.Text, SerializeAsText(item), statusCode),
            WsEnumFormatType.Json => GetContentResult(WsEnumFormatType.Json, SerializeAsJson(item), statusCode),
            WsEnumFormatType.Xml or WsEnumFormatType.XmlUtf8 => GetContentResult(WsEnumFormatType.XmlUtf8, SerializeAsXmlString<T>(item, true, false), statusCode),
            WsEnumFormatType.XmlUtf16 => GetContentResult(WsEnumFormatType.XmlUtf16, SerializeAsXmlString<T>(item, true, true), statusCode),
            _ => throw WsDataUtils.GetArgumentException(nameof(format))
        };

    public static WsEnumFormatType GetFormatType(string format) => format.ToUpper() switch
    {
        "TEXT" => WsEnumFormatType.Text,
        "JSON" => WsEnumFormatType.Json,
        "XML" or "" or "XMLUTF8" => WsEnumFormatType.Xml,
        "XMLUTF16" => WsEnumFormatType.XmlUtf16,
        _ => throw WsDataUtils.GetArgumentException(nameof(format))
    };
    
    public static string GetContent<T>(ISerializable item, WsEnumFormatType formatType, bool isAddEmptyNamespace) => formatType switch
    {
        WsEnumFormatType.Text => SerializeAsText(item),
        WsEnumFormatType.Json => GetPrettyXmlOrJson(SerializeAsJson(item)),
        WsEnumFormatType.Xml or WsEnumFormatType.XmlUtf8 => GetPrettyXml(SerializeAsXmlString<T>(item, isAddEmptyNamespace, false)),
        WsEnumFormatType.XmlUtf16 => GetPrettyXml(SerializeAsXmlString<T>(item, isAddEmptyNamespace, true)),
        _ => throw WsDataUtils.GetArgumentException(nameof(formatType))
    };
    
    #endregion
}
