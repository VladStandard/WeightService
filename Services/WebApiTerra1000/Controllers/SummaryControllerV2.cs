// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using WsLocalizationCore.Utils;
using WsStorageCore.Utils;
using WsWebApiCore.Base;
using WsWebApiCore.Utils;

namespace WebApiTerra1000.Controllers;

public sealed class SummaryControllerV2 : WsControllerBase
{
    #region Constructor and destructor

    public SummaryControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetSummaryV2)]
    public ContentResult GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery(Name = "format")] string format = "") =>
        GetSummaryCore(WsWebSqlQueriesV2.GetSummary, startDate, endDate, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetSummaryV2Preview)]
    public ContentResult GetSummaryPreview([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery(Name = "format")] string format = "") =>
        GetSummaryCore(WsWebSqlQueriesV2.GetSummaryPreview, startDate, endDate, format);

    private ContentResult GetSummaryCore(string url, DateTime startDate, DateTime endDate, string format) => 
        GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, url,
                WsWebSqlUtils.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);

    #endregion
}
