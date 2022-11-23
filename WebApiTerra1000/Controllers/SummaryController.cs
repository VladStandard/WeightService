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
using WebApiCore.Models;

namespace WebApiTerra1000.Controllers;

public class SummaryController : BaseController
{
    #region Constructor and destructor

    public SummaryController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/summary/")]
    public ContentResult GetSummary(DateTime startDate, DateTime endDate, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetSummary,
                WebUtils.Sql.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
