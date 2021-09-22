// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using static DataShareCore.ShareEnums;

namespace DataShareCore.DAL.Models
{
    [Serializable()]
    public class BaseSerializeEntity<T> where T : new()
    {
        #region Public and private methods

        public string SerializeAsJson() => JsonConvert.SerializeObject(this);

        public string SerializeAsXml()
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            using StringWriter textWriter = new();
            xmlSerializer.Serialize(textWriter, this);
            return textWriter.ToString();
        }

        public static T DeserializeFromXml(string xml)
        {
            XmlSerializer xmlSerializer = new(typeof(T));
            return (T)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xml)));
        }

        public static T DeserializeFromXmlVersion2(string xml)
        {
            T result = new();
            XmlSerializer xmlSerializer = new(typeof(T));
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

        private static string GetContentType(FormatType format) => format switch
        {
            FormatType.Xml => "application/xml",
            FormatType.Json => "application/json",
            FormatType.Html => "application/html",
            FormatType.Text => "application/text",
            FormatType.Raw => "application/text",
            _ => throw GetArgumentException(nameof(format)),
        };

        private static ContentResult GetResultInside(FormatType format, object content, HttpStatusCode statusCode) => new()
        {
            ContentType = GetContentType(format),
            StatusCode = (int)statusCode,
            Content = content is string ? content as string : content?.ToString()
        };

        public static ContentResult GetResult(FormatType format, object content, HttpStatusCode statusCode) => GetResultInside(format, content, statusCode);

        public ContentResult GetResult(FormatType format, HttpStatusCode statusCode)
        {
            return format switch
            {
                FormatType.Json => GetResult(format, SerializeAsJson(), statusCode),
                FormatType.Xml => GetResult(format, SerializeAsXml(), statusCode),
                FormatType.Html => GetResult(format, SerializeAsHtml(), statusCode),
                FormatType.Text or FormatType.Raw => GetResult(format, SerializeAsText(), statusCode),
                _ => throw GetArgumentException(nameof(format)),
            };
        }
        
        #endregion
    }
}
