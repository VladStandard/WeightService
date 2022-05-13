// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Models
{
    [Serializable()]
    public class BaseSerializeEntity : ISerializable
    {
        #region Public and private fields and properties

        [XmlIgnore] public virtual SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Constructor and destructor

        public BaseSerializeEntity()
        {
            //
        }

        protected BaseSerializeEntity(SerializationInfo info, StreamingContext context)
        {
            //
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

        public virtual string SerializeAsXmlWithEmptyNamespaces<T>() where T : new()
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

        public virtual string SerializeAsXml<T>() where T : new()
        {
            // Don't use it.
            // XmlSerializer xmlSerializer = new(typeof(T));
            // Use it.
            XmlSerializer xmlSerializer = XmlSerializer.FromTypes(new[] { typeof(T) })[0];
            using StringWriter stringWriter = new();
            xmlSerializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
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
            T result;
            using (TextReader reader = new StringReader(xml))
            {
                result = (T)xmlSerializer.Deserialize(reader);
            }
            return result;
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
                FormatType.Xml => GetResult(format, SerializeAsXml<T>(), statusCode),
                FormatType.Html => GetResult(format, SerializeAsHtml(), statusCode),
                FormatType.Text or FormatType.Raw => GetResult(format, SerializeAsText(), statusCode),
                _ => throw GetArgumentException(nameof(format)),
            };
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue(nameof(SqlConnect), SqlConnect);
        }

        #endregion
    }
}
