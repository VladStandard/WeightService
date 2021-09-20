// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Linq;
using static Terra.Common.Enums;

namespace Terra.Common
{
    public static class ResponseUtils
    {
        //private static async Task SendZabbixAsync(string method, XDocument doc)
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        //    using var zabbixServer = _zabbixServer.OpenSession();
        //    zabbixServer.ZabbixNoticeAsync(ZabbixHost, ZabbixKey,
        //        new Dictionary<string, string> {
        //            { "method", method},
        //            { "date", DateTime.Now.ToString("O") },
        //            { "size", doc.ToString().Length.ToString() }
        //            //{ "start", StartDate.ToString("O") },
        //            //{ "end", EndDate.ToString("O") }
        //        }
        //    );
        //}

        //private static async Task SendZabbixAsync(string method, XDocument doc, string start, string end)
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        //    using var zabbixServer = _zabbixServer.OpenSession();
        //    zabbixServer.ZabbixNoticeAsync(ZabbixHost, ZabbixKey,
        //        new Dictionary<string, string> {
        //            { "method", method},
        //            { "date", DateTime.Now.ToString("O") },
        //            { "size", doc.ToString().Length.ToString() },
        //            { "start", start },
        //            { "end", end }
        //        }
        //    );
        //}

        //private static async Task SendZabbixAsync(string method, string message)
        //{
        //    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        //    using var zabbixServer = _zabbixServer.OpenSession();
        //    zabbixServer.ZabbixNoticeAsync(ZabbixHost, ZabbixKey,
        //        new Dictionary<string, string> {
        //            { "method", method },
        //            { "error", message }
        //        });
        //}

        public static XDocument GetNullOrEmpty(string response)
        {
            XDocument doc = null;
            if (string.IsNullOrEmpty(response))
            {
                doc = new(
                    new XElement("Response",
                        new XElement("Error", new XAttribute("Description", "Result is null or empty!"))
                    ));
            }
            return doc;
        }

        public static XDocument GetError(string response)
        {
            XDocument doc = null;
            if (response.Contains("<Error "))
            {
                ErrorEntity error = JsonConvert.DeserializeObject<ErrorEntity>(response);
                doc = new(
                    new XElement("Response",
                        new XElement("Error", new XAttribute("Description", error.Description))
                    ));
            }
            return doc;
        }

        public static XDocument GetUnknownError(string response) => new(
                new XElement("Response",
                    new XElement("Error", new XAttribute("Description", "Unknown error!"))
                ));

        public static ContentResult GetContentResult(FormatType formatType, string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            string contentType = formatType switch
            {
                FormatType.Xml => "application/xml",
                FormatType.Json => "application/json",
                FormatType.Html => "application/html",
                FormatType.Text => "application/xml",
                _ => "application/auto",
            };
            return new ContentResult
            {
                ContentType = contentType,
                StatusCode = (int)statusCode,
                Content = content
            };
        }
    }
}
