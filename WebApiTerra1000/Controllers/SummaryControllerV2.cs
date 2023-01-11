// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataCore.Enums;
using WsStorageCore.Utils;
using WsWebApiCore.Controllers;
using WsWebApiCore.Models;

namespace WebApiTerra1000.Controllers;

public class SummaryControllerV2 : WebControllerBase
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
    [Route("api/v2/summary/")]
    public ContentResult GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery(Name = "format")] string formatString = "") =>
        GetSummaryCore(SqlQueriesV2.GetSummary, startDate, endDate, formatString);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/summary_preview/")]
    public ContentResult GetSummaryPreview([FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery(Name = "format")] string formatString = "") =>
        GetSummaryCore(SqlQueriesV2.GetSummaryPreview, startDate, endDate, formatString);

    private ContentResult GetSummaryCore(string url, DateTime startDate, DateTime endDate, string formatString) => 
        ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url,
                WebUtils.Sql.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);

    #endregion
}
