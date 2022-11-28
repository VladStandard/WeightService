// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;
using DataCore.Enums;

namespace DataCore.Sql.Models;

[Serializable]
public class SerializeBase : ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Contrsuctor.
    /// </summary>
    public SerializeBase()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SerializeBase(SerializationInfo info, StreamingContext context)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //
    }

    public virtual XmlReaderSettings GetXmlReaderSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        //OmitXmlDeclaration = false, // не подавлять xml заголовок
        //Encoding = Encoding.UTF32,   // кодировка // настройка не работает и UTF16 записывается в шапку XML, типа Visual Studio работает только с UTF16
        //Encoding = Encoding.Unicode,
        //Indent = true,              // добавлять отступы
        //IndentChars = "\t"          // сиволы отступа
    };

    public virtual XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false, // не подавлять xml заголовок
        //Encoding = Encoding.UTF32,   // кодировка // настройка не работает и UTF16 записывается в шапку XML, типа Visual Studio работает только с UTF16
        Encoding = Encoding.Unicode,
        Indent = true,              // добавлять отступы
        IndentChars = "\t"          // сиволы отступа
    };

    public virtual string SerializeAsJson() => JsonConvert.SerializeObject(this);

    public virtual string SerializeAsXmlString<T>(bool isAddEmptyNamespace) where T : new()
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        // The T object must have properties with { get; set; }.
        using StringWriter stringWriter = new();
        switch (isAddEmptyNamespace)
        {
            case true:
                {
                    XmlSerializerNamespaces emptyNamespaces = new();
                    emptyNamespaces.Add(string.Empty, string.Empty);
                    using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, GetXmlWriterSettings());
                    xmlSerializer.Serialize(xmlWriter, this, emptyNamespaces);
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    break;
                }
            default:
                xmlSerializer.Serialize(stringWriter, this);
                break;
        }
        return stringWriter.ToString();
    }

    public virtual XmlDocument SerializeAsXmlDocument<T>(bool isAddEmptyNamespace) where T : new()
    {
        XmlDocument xmlDocument = new();
	    string xmlString = SerializeAsXmlString<T>(isAddEmptyNamespace);
        byte[] bytes = Encoding.Unicode.GetBytes(xmlString);
	    using MemoryStream memoryStream = new(bytes);
		memoryStream.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);
        xmlDocument.Load(memoryStream);
	    return xmlDocument;
    }

    public virtual string SerializeByMemoryStream<T>() where T : new()
    {
        MemoryStream memoryStream = new();
        IFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(memoryStream, this);
        //string result = memoryStream.ToString();
        string result;
        using StreamReader streamReader = new(memoryStream);
        memoryStream.Position = 0;
        result = streamReader.ReadToEnd();
        memoryStream.Close();
        return result;
    }

    public virtual T DeserializeFromMemoryStream<T>(MemoryStream memoryStream) where T : new()
    {
        IFormatter formatter = new BinaryFormatter();
        memoryStream.Seek(0, SeekOrigin.Begin);
        return (T)formatter.Deserialize(memoryStream);
    }

    public virtual T DeserializeFromXml<T>(string xml) where T : new()
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        //return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
        return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.Unicode.GetBytes(xml)));
    }

    public virtual T DeserializeFromXmlVersion2<T>(string xml) where T : new()
    {
        // Don't use it.
        // XmlSerializer xmlSerializer = new(typeof(T));
        // Use it.
        XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
        //T result = new();
        using TextReader reader = new StringReader(xml);
        return (T)xmlSerializer.Deserialize(reader);
    }

    public virtual string SerializeAsHtml() => @$"
<html>
<body>
    {this}
</body>
</html>
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

    public virtual string SerializeAsText() => ToString();

    public virtual ContentResult GetContentResultCore(FormatType formatType, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = DataUtils.GetContentType(formatType),
        StatusCode = (int)statusCode,
        Content = content as string ?? content.ToString()
    };

    public virtual ContentResult GetContentResultCore(string formatString, object content, HttpStatusCode statusCode) => 
        GetContentResultCore(DataUtils.GetFormatType(formatString), content, statusCode);

    public virtual ContentResult GetContentResult(FormatType formatType, object content, HttpStatusCode statusCode) => 
        GetContentResultCore(formatType, content, statusCode);

    public virtual ContentResult GetContentResult(string formatString, object content, HttpStatusCode statusCode) => 
        GetContentResultCore(formatString, content, statusCode);

    public virtual ContentResult GetContentResult<T>(FormatType formatType, HttpStatusCode statusCode) where T : new() => formatType switch
    {
        FormatType.Text => GetContentResult(formatType, SerializeAsText(), statusCode),
        FormatType.JavaScript => GetContentResult(formatType, SerializeAsText(), statusCode),
        FormatType.Json => GetContentResult(formatType, SerializeAsJson(), statusCode),
        FormatType.Html => GetContentResult(formatType, SerializeAsHtml(), statusCode),
        FormatType.Xml => GetContentResult(formatType, SerializeAsXmlString<T>(false), statusCode),
        _ => throw DataUtils.GetArgumentException(nameof(formatType)),
    };

    public virtual ContentResult GetContentResult<T>(string formatString, HttpStatusCode statusCode) where T : new() => 
        GetContentResult<T>(GetFormatType(formatString), statusCode);

    public virtual FormatType GetFormatType(string formatType) => formatType.ToUpper() switch
    {
        "TEXT" => FormatType.Text,
        "JAVASCRIPT" => FormatType.JavaScript,
        "JSON" => FormatType.Json,
        "HTML" => FormatType.Html,
        "XML" or "" => FormatType.Xml,
        _ => throw DataUtils.GetArgumentException(nameof(formatType)),
    };

    public virtual T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : new()
    {
        Type type = typeof(T);
        T result = (T)Activator.CreateInstance(type);
        foreach (KeyValuePair<string, object> item in dict)
        {
            type.GetProperty(item.Key)?.SetValue(result, item.Value, null);
        }
        return result;
    }

    public virtual IDictionary<string, object> ObjectToDictionary<T>(T item) where T : new()
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

    public virtual XDocument GetBtXmlNamedSubString<T>(T item, XName name, object value) where T : new()
    {
        IDictionary<string, object> dict = ObjectToDictionary(item);
        XDocument result = new(
            new XElement("XMLScript", new XAttribute("Version", "2.0"),
                new XElement("Command",
                    new XElement("Print",
                        new XElement("Format", new XAttribute(name, value)),
                        dict.Select(x => new XElement("NameSubString",
                            new XAttribute("Key", x.Key),
                            new XElement("Value", x.Value)))
                    ))));
        return result;
    }

    #endregion

    public virtual string GetContent<T>(FormatType formatType) where T : new()
    {
        return formatType switch
        {
            FormatType.Text => SerializeAsText(),
            FormatType.JavaScript => XmlUtils.GetPrettyXmlOrJson(SerializeAsJson()),
            FormatType.Json => XmlUtils.GetPrettyXmlOrJson(SerializeAsJson()),
            FormatType.Html => SerializeAsHtml(),
            FormatType.Xml => XmlUtils.GetPrettyXml(SerializeAsXmlString<T>(true)),
            _ => throw DataUtils.GetArgumentException(nameof(formatType)),
        };
    }
}
