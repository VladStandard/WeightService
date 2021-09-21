// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using static WebApiTerra1000.Utils.TerraEnums;

namespace WebApiTerra1000.Common
{
    public static class TerraUtils
    {
        public static class Json
        {
            public static ServiceExceptionEntity GetException(string filePath, int lineNumber, string memberName, Exception ex) =>
                new(filePath, lineNumber, memberName, ex);
        }

        public static class Xml
        {
            public static XDocument GetNullOrEmpty(string response)
            {
                XDocument doc = null;
                if (string.IsNullOrEmpty(response))
                {
                    doc = new(
                        new XElement(TerraConsts.Response,
                            new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, "Result is null or empty!"))
                        ));
                }
                return doc;
            }

            public static XDocument GetError(string response)
            {
                XDocument doc = null;
                if (response.Contains("<Error "))
                {
                    SqlSimpleEntity error = JsonConvert.DeserializeObject<SqlSimpleEntity>(response);
                    doc = new(
                        new XElement(TerraConsts.Response,
                            new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, error.Description))
                        ));
                }
                return doc;
            }

            public static XDocument GetErrorUnknown() => new(
                new XElement(TerraConsts.Response,
                    new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, "Unknown error!"))));

            public static XDocument GetException(string filePath, int lineNumber, string memberName, Exception ex) => new(
                new XElement(TerraConsts.Response,
                    new XElement(TerraConsts.Error,
                        new XAttribute(TerraConsts.FilePath, filePath),
                        new XAttribute(TerraConsts.LineNumber, lineNumber),
                        new XAttribute(TerraConsts.MemberName, memberName),
                        new XAttribute(TerraConsts.Exception, ex.Message),
                        new XAttribute(TerraConsts.InnerException,
                            ex.InnerException != null ? ex.InnerException.Message : string.Empty))));
        }

        public static class Text
        {
            public static string GetException(string filePath, int lineNumber, string memberName, Exception ex) =>
                $"{TerraConsts.Response}." + Environment.NewLine +
                $"{TerraConsts.FilePath}: {filePath}" + Environment.NewLine +
                $"{TerraConsts.LineNumber}: {lineNumber}" + Environment.NewLine +
                $"{TerraConsts.MemberName}: {memberName}" + Environment.NewLine +
                $"{TerraConsts.Exception}: {ex.Message}" + Environment.NewLine +
                $"{TerraConsts.InnerException}: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}";
        }

        public static class Html
        {
            public static string GetException(string filePath, int lineNumber, string memberName, Exception ex) => @$"
<html>
    <body>
        {TerraConsts.Response}.
        {TerraConsts.FilePath}: {filePath}.
        {TerraConsts.LineNumber}: {lineNumber}.
        {TerraConsts.MemberName}: {memberName}.
        {TerraConsts.Exception}: {ex.Message}.
        {TerraConsts.InnerException}: {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}.
    </body>
</html>"
                .TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        }

        public static class Content
        {
            public static string GetMessage(FormatType format, string message)
            {
                switch (format)
                {
                    case FormatType.Json:
                        break;
                    case FormatType.Xml:
                        break;
                    case FormatType.Html:
                        break;
                    case FormatType.Text:
                        break;
                    case FormatType.Raw:
                        break;
                    default:
                        throw GetArgumentException(nameof(format));
                }
                return new XDocument(
                    new XElement(TerraConsts.Response,
                        new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, message))
                )).ToString();
            }
        }

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

        private static ContentResult GetResultInside(FormatType format, string contentString, HttpStatusCode statusCode) => new()
            {
                ContentType = GetContentType(format),
                StatusCode = (int)statusCode,
                Content = contentString
            };

        public static ContentResult GetResult(FormatType format, string content, HttpStatusCode statusCode) =>
            GetResultInside(format, content, statusCode);

        public static ContentResult GetResultWithWrap(FormatType format, object content, HttpStatusCode statusCode)
        {
            string contentString = string.Empty;
            if (content != null)
            { 
                contentString = format switch
                {
                    FormatType.Json => JsonConvert.SerializeObject(content),
                    FormatType.Xml => new XDocument(
                        new XElement(TerraConsts.Response,
                            new XElement(TerraConsts.Message,
                                new XAttribute(TerraConsts.Value, content.ToString())))).ToString(),
                    FormatType.Html => @$"
<html>
    <body>
        {content}
    </body>
</html>"
                        .TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t'),
                    FormatType.Text => @$"{TerraConsts.Response}. {TerraConsts.Message}. {TerraConsts.Value}:" + Environment.NewLine + @$"{content}",
                    FormatType.Raw => content.ToString(),
                    _ => throw GetArgumentException(nameof(format)),
                };
            }
            return GetResultInside(format, contentString, statusCode);
        }

        public static class Sql
        {
            public static T GetResponse<T>(ISessionFactory sessionFactory, string query)
            {
                using ISession session = sessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                T response = session.CreateSQLQuery(query).UniqueResult<T>();
                transaction.Commit();
                return response;
            }
        }
    }
}
