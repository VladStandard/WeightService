// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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

    public static XmlDocument XmlCompatibleReplace(XmlDocument xmlDocument)
    {
        XmlDocument result = new();
        result.LoadXml(XmlCompatibleReplace(xmlDocument.OuterXml));
        return result;
    }

    private static string XmlCompatibleReplace(string xml)
    {
        if (string.IsNullOrEmpty(xml)) return xml;

        //xml = xml.Replace(nameof(TableDirectModels.HostDirect), "HostEntity");
        xml = xml.Replace(nameof(WsStorageCore.TableDirectModels.NomenclatureDirect), "NomenclatureEntity");
        //xml = xml.Replace(nameof(TableDirectModels.PrinterDirect), "ZebraPrinterEntity");
        xml = xml.Replace(nameof(WsStorageCore.TableDirectModels.ProductSeriesDirect), "ProductSeriesEntity");
        xml = xml.Replace(nameof(WsStorageCore.TableDirectModels.SsccDirect), "SsccEntity");

        return xml;
    }

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

    /// <summary>
    /// Replace area-resource.
    /// </summary>
    /// <param name="area"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string PrintCmdReplaceArea(WsSqlProductionFacilityModel? area, string value)
    {
        string result = value;
        if (string.IsNullOrEmpty(result))
            return result;
        if (area is not null)
        {
            string areaName = area.Name;
            if (string.IsNullOrEmpty(areaName))
                return result;
            string resourceHex = ZplUtils.ConvertStringToHex(areaName);
            result = result.Replace($"[AREA]", resourceHex);
        }
        return result;
    }

    public static string XmlMerge(string xmlParent, string xmlChild)
    {
        string result = string.Empty;
        bool isSkipHeader = false;
        foreach (string lineParent in xmlParent.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
        {
            if (isSkipHeader && !string.IsNullOrEmpty(xmlChild))
            {
                foreach (string lineChild in xmlChild.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (!lineChild.StartsWith("<?xml "))
                        result += "\t" + lineChild + Environment.NewLine;
                }
                xmlChild = string.Empty;
            }
            if (lineParent.StartsWith("<") && !lineParent.StartsWith("<?xml "))
                isSkipHeader = true;
            result += lineParent + Environment.NewLine;
        }
        return result;
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

    #region Public and private methods - Properties

    public static object? GetPropertyDefaultValue<T>(T item, string name)
    {
        AttributeCollection? attributes = TypeDescriptor.GetProperties(item)[name]?.Attributes;
        Attribute? attribute = attributes?[typeof(DefaultValueAttribute)];
        if (attribute is DefaultValueAttribute defaultValueAttribute)
            return defaultValueAttribute.Value;
        return null;
    }

    public static string GetPropertyDefaultValueAsString<T>(T item, string name) =>
        GetPropertyDefaultValue(item, name)?.ToString() ?? string.Empty;

    public static int GetPropertyDefaultValueAsInt<T>(T item, string name) =>
        GetPropertyDefaultValue(item, name) is int value ? value : default;

    public static bool GetPropertyDefaultValueAsBool<T>(T item, string name) => GetPropertyDefaultValue(item, name) is bool value ? value : default;

    public static IEnumerable<string> GetPropertiesNames<T>(T item) =>
        (from PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item) select propertyDescriptor.Name).ToList();

    #endregion

    #region Public and private methods - Serialize

    public static XmlReaderSettings GetXmlReaderSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document
    };

    public static XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false,
        Indent = true,
        IndentChars = "\t"
    };

    public static string SerializeAsJson<T>(T item) => JsonConvert.SerializeObject(item);

    public static string SerializeByMemoryStream<T>(T item)
    {
        MemoryStream memoryStream = new();
        IFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(memoryStream, item);
        string result;
        using StreamReader streamReader = new(memoryStream);
        memoryStream.Position = 0;
        result = streamReader.ReadToEnd();
        memoryStream.Close();
        return result;
    }

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

    //public static string SerializeAsXmlString(string item, bool isAddEmptyNamespace, bool isUtf16)
    //{
    //    XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(string) })[0];

    //    using StringWriter stringWriter = isUtf16 ? new StringWriter() : new WsSqlStringWriterUtf8Model();

    //    switch (isAddEmptyNamespace)
    //    {
    //        case true:
    //        {
    //            XmlSerializerNamespaces emptyNamespaces = new();
    //            emptyNamespaces.Add(string.Empty, string.Empty);
    //            using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, GetXmlWriterSettings());
    //            xmlSerializer.Serialize(xmlWriter, item, emptyNamespaces);
    //            xmlWriter.Flush();
    //            xmlWriter.Close();
    //            break;
    //        }
    //        default:
    //            xmlSerializer.Serialize(stringWriter, item);
    //            break;
    //    }
    //    return stringWriter.ToString();
    //}

    private static string SerializeAsHtml<T>(T item) => WsSqlQueries.TrimQuery(@$"
<html>
<body>
    {item}
</body>
</html>
        ");

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

    public static T DeserializeFromXmlVersion2<T>(string xml)
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        using TextReader reader = new StringReader(xml);
        return (T)xmlSerializer.Deserialize(reader);
    }

    public static T DeserializeFromMemoryStream<T>(MemoryStream memoryStream) where T : new()
    {
        memoryStream.Position = 0;
        IFormatter formatter = new BinaryFormatter();
        memoryStream.Seek(0, SeekOrigin.Begin);
        object? obj = formatter.Deserialize(memoryStream);
        return obj is null ? new() : (T)obj;
    }

    #endregion

    #region Public and private methods - Content

    public static string SerializeAsText<T>(ISerializable item) => item?.ToString() ?? string.Empty;

    public static string SerializeAsText(string item) => item?.ToString() ?? string.Empty;

    private static ContentResult GetContentResultCore(FormatType formatType, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = DataUtils.GetContentType(formatType),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString(),
    };

    private static ContentResult GetContentResultCore(string formatString, object content, HttpStatusCode statusCode) =>
        GetContentResultCore(DataUtils.GetFormatType(formatString), content, statusCode);

    private static ContentResult GetContentResult(FormatType formatType, object content, HttpStatusCode statusCode) =>
        GetContentResultCore(formatType, content, statusCode);

    public static ContentResult GetContentResult<T>(ISerializable item, string format, HttpStatusCode statusCode) =>
        GetFormatType(format) switch
        {
            FormatType.Text => GetContentResult(FormatType.Text, SerializeAsText<T>(item), statusCode),
            FormatType.JavaScript => GetContentResult(FormatType.JavaScript, SerializeAsText<T>(item), statusCode),
            FormatType.Json => GetContentResult(FormatType.Json, SerializeAsJson(item), statusCode),
            FormatType.Html => GetContentResult(FormatType.Html, SerializeAsHtml(item), statusCode),
            FormatType.Xml or FormatType.XmlUtf8 => GetContentResult(FormatType.XmlUtf8, SerializeAsXmlString<T>(item, true, false), statusCode),
            FormatType.XmlUtf16 => GetContentResult(FormatType.XmlUtf16, SerializeAsXmlString<T>(item, true, true), statusCode),
            _ => throw DataUtils.GetArgumentException(nameof(format))
        };

    public static FormatType GetFormatType(string format) => format.ToUpper() switch
    {
        "TEXT" => FormatType.Text,
        "JAVASCRIPT" => FormatType.JavaScript,
        "JSON" => FormatType.Json,
        "HTML" => FormatType.Html,
        "XML" or "" or "XMLUTF8" => FormatType.Xml,
        "XMLUTF16" => FormatType.XmlUtf16,
        _ => throw DataUtils.GetArgumentException(nameof(format))
    };

    public static IDictionary<string, object> ObjectToDictionary<T>(T item)
    {
        IDictionary<string, object> result = new Dictionary<string, object>();
        if (item is null)
            return result;
        object[] indexer = Array.Empty<object>();
        foreach (PropertyInfo info in item.GetType().GetProperties())
        {
            object value = info.GetValue(item, indexer);
            result.Add(info.Name, value);
        }
        return result;
    }

    public static string GetContent<T>(ISerializable item, FormatType formatType, bool isAddEmptyNamespace) => formatType switch
    {
        FormatType.Text => SerializeAsText<T>(item),
        FormatType.JavaScript => GetPrettyXmlOrJson(SerializeAsJson(item)),
        FormatType.Json => GetPrettyXmlOrJson(SerializeAsJson(item)),
        FormatType.Html => SerializeAsHtml(item),
        FormatType.Xml or FormatType.XmlUtf8 => GetPrettyXml(SerializeAsXmlString<T>(item, isAddEmptyNamespace, false)),
        FormatType.XmlUtf16 => GetPrettyXml(SerializeAsXmlString<T>(item, isAddEmptyNamespace, true)),
        _ => throw DataUtils.GetArgumentException(nameof(formatType))
    };

    #endregion
}
