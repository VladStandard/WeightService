// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Models;

[Serializable]
public class SerializeModel : ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

    /// <summary>
    /// Contrsuctor.
    /// </summary>
    public SerializeModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SerializeModel(SerializationInfo info, StreamingContext context)
    {
        //SqlConnect = (SqlConnectFactory)info.GetValue(nameof(SqlConnect), typeof(SqlConnectFactory));
    }

    #endregion

    #region Public and private methods

    public virtual XmlWriterSettings GetXmlWriterSettings() => new()
    {
        ConformanceLevel = ConformanceLevel.Document,
        OmitXmlDeclaration = false, // не подавлять xml заголовок
        Encoding = Encoding.UTF8,   // кодировка // настройка не работает и UTF16 записывается в шапку XML, типа Visual Studio работает только с UTF16
        Indent = true,              // добавлять отступы
        IndentChars = "\t"          // сиволы отступа
    };

    public virtual string SerializeAsJson() => JsonConvert.SerializeObject(this);

    public virtual string SerializeAsXml<T>(bool isAddEmptyNamespace) where T : new()
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
        return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
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

    public virtual ArgumentException GetArgumentException(string argument) => new($"Argument {argument} must be setup!");

    public virtual string GetContentType(FormatType format) => format switch
    {
        FormatType.Xml => "application/xml",
        FormatType.Json => "application/json",
        FormatType.Html => "application/html",
        FormatType.Text => "application/text",
        FormatType.Raw => "application/text",
        _ => throw GetArgumentException(nameof(format)),
    };

    public virtual ContentResult GetResultInside(FormatType format, object content, HttpStatusCode statusCode) => new()
    {
        ContentType = GetContentType(format),
        StatusCode = (int)statusCode,
        Content = content is string ? content as string : content?.ToString()
    };

    public virtual ContentResult GetResult(FormatType format, object content, HttpStatusCode statusCode) => GetResultInside(format, content, statusCode);

    public virtual ContentResult GetResult<T>(FormatType format, HttpStatusCode statusCode) where T : new()
    {
        return format switch
        {
            FormatType.Json => GetResult(format, SerializeAsJson(), statusCode),
            FormatType.Xml => GetResult(format, SerializeAsXml<T>(false), statusCode),
            FormatType.Html => GetResult(format, SerializeAsHtml(), statusCode),
            FormatType.Text or FormatType.Raw => GetResult(format, SerializeAsText(), statusCode),
            _ => throw GetArgumentException(nameof(format)),
        };
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //info.AddValue(nameof(SqlConnect), SqlConnect);
    }

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
	    Type myObjectType = item.GetType();
	    object[] indexer = Array.Empty<object>();
	    PropertyInfo[] properties = myObjectType.GetProperties();
	    foreach (PropertyInfo info in properties)
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
}
