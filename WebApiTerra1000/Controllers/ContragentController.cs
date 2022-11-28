// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiCore.Controllers;
using WebApiCore.Utils;
using WebApiCore.Models;

namespace WebApiTerra1000.Controllers;

public class ContragentController : WebControllerBase
{
    #region Constructor and destructor

    public ContragentController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/contragent/")]
    public ContentResult GetContragent([FromQuery] long id, [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetContragent, new SqlParameter("ID", id));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
            //return SerializeBase.GetResult<XDocument>(format, doc, HttpStatusCode.OK);
        }, formatString);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/contragents/")]
    public ContentResult GetContragents([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int offset = 0, 
        [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetContragents,
                WebUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}
