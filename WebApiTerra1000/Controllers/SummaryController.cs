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
using WebApiCore.Controllers;
using WebApiCore.Utils;
using WebApiCore.Models;
using DataCore.Enums;

namespace WebApiTerra1000.Controllers;

public class SummaryController : WebControllerBase
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
    public ContentResult GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetSummary,
                WebUtils.Sql.GetParameters(startDate, endDate));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Summary} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}
