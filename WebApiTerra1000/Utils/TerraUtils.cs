// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using static DataShareCore.ShareEnums;

namespace WebApiTerra1000.Common
{
    public static class TerraUtils
    {
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
                    SqlSimpleV1Entity error = JsonConvert.DeserializeObject<SqlSimpleV1Entity>(response);
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
        }

        //public static class Content
        //{
        //    public static string GetMessage(FormatType format, string message)
        //    {
        //        switch (format)
        //        {
        //            case FormatType.Json:
        //                break;
        //            case FormatType.Xml:
        //                break;
        //            case FormatType.Html:
        //                break;
        //            case FormatType.Text:
        //                break;
        //            case FormatType.Raw:
        //                break;
        //            default:
        //                throw GetArgumentException(nameof(format));
        //        }
        //        return new XDocument(
        //            new XElement(TerraConsts.Response,
        //                new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, message))
        //        )).ToString();
        //    }
        //}

        //public static ContentResult GetResultWithWrap(FormatType format, object content, HttpStatusCode statusCode)
        //{
        //    string contentString = string.Empty;
        //    if (content != null)
        //    {
        //        switch (format)
        //        {
        //            case FormatType.Json:
        //                {
        //                    if (content is SqlSimpleV1Entity simpleV1)
        //                        contentString = simpleV1.SerializeAsJson();
        //                    else if (content is SqlSimpleV2Entity simpleV2)
        //                        contentString = simpleV2.SerializeAsJson();
        //                    else if (content is SqlSimpleV3Entity simpleV3)
        //                        contentString = simpleV3.SerializeAsJson();
        //                    else if (content is SqlSimpleV4Entity simpleV4)
        //                        contentString = simpleV4.SerializeAsJson();
        //                    else if (content is ServiceInfoEntity serviceInfo)
        //                        contentString = serviceInfo.SerializeAsJson();
        //                    else if (content is ServiceExceptionEntity serviceException)
        //                        contentString = serviceException.SerializeAsJson();
        //                }
        //                break;
        //            case FormatType.Xml:
        //                {
        //                    if (content is SqlSimpleV1Entity simpleV1)
        //                        contentString = simpleV1.SerializeAsXml();
        //                    else if (content is SqlSimpleV2Entity simpleV2)
        //                        contentString = simpleV2.SerializeAsXml();
        //                    else if (content is SqlSimpleV3Entity simpleV3)
        //                        contentString = simpleV3.SerializeAsXml();
        //                    else if (content is SqlSimpleV4Entity simpleV4)
        //                        contentString = simpleV4.SerializeAsXml();
        //                    else if (content is ServiceInfoEntity serviceInfo)
        //                        contentString = serviceInfo.SerializeAsXml();
        //                    else if (content is ServiceExceptionEntity serviceException)
        //                        contentString = serviceException.SerializeAsXml();
        //                }
        //                break;
        //            case FormatType.Html:
        //                {
        //                    if (content is SqlSimpleV1Entity simpleV1)
        //                        contentString = simpleV1.SerializeAsHtml();
        //                    else if (content is SqlSimpleV2Entity simpleV2)
        //                        contentString = simpleV2.SerializeAsHtml();
        //                    else if (content is SqlSimpleV3Entity simpleV3)
        //                        contentString = simpleV3.SerializeAsHtml();
        //                    else if (content is SqlSimpleV4Entity simpleV4)
        //                        contentString = simpleV4.SerializeAsHtml();
        //                    else if (content is ServiceInfoEntity serviceInfo)
        //                        contentString = serviceInfo.SerializeAsHtml();
        //                    else if (content is ServiceExceptionEntity serviceException)
        //                        contentString = serviceException.SerializeAsHtml();
        //                }
        //                break;
        //            case FormatType.Text:
        //                {
        //                    if (content is SqlSimpleV1Entity simpleV1)
        //                        contentString = simpleV1.SerializeAsText();
        //                    else if (content is SqlSimpleV2Entity simpleV2)
        //                        contentString = simpleV2.SerializeAsText();
        //                    else if (content is SqlSimpleV3Entity simpleV3)
        //                        contentString = simpleV3.SerializeAsText();
        //                    else if (content is SqlSimpleV4Entity simpleV4)
        //                        contentString = simpleV4.SerializeAsText();
        //                    else if (content is ServiceInfoEntity serviceInfo)
        //                        contentString = serviceInfo.SerializeAsText();
        //                    else if (content is ServiceExceptionEntity serviceException)
        //                        contentString = serviceException.SerializeAsText();
        //                }
        //                break;
        //            case FormatType.Raw:
        //                contentString = content.ToString();
        //                break;
        //            default:
        //                throw GetArgumentException(nameof(format));
        //        }
        //    }
        //    return GetResultInside(format, contentString, statusCode);
        //}

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
