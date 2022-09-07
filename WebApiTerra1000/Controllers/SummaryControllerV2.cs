// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataCore.Models;
using WebApiCore.Controllers;
using WebApiCore.Utils;

namespace WebApiTerra1000.Controllers;

public class SummaryControllerV2 : BaseController
{
    #region Constructor and destructor

    public SummaryControllerV2(ILogger<SummaryControllerV2> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/summary/")]
    public ContentResult GetSummary(DateTime startDate, DateTime endDate, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return GetSummaryWork(SqlQueriesV2.GetSummary, startDate, endDate, format);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/summary_preview/")]
    public ContentResult GetSummaryPreview(DateTime startDate, DateTime endDate, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return GetSummaryWork(SqlQueriesV2.GetSummaryPreview, startDate, endDate, format);
    }

    private ContentResult GetSummaryWork(string url, DateTime startDate, DateTime endDate, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url,
                TerraUtils.Sql.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
