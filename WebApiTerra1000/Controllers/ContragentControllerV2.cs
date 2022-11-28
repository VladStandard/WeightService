// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiCore.Controllers;
using WebApiCore.Utils;
using WebApiCore.Models;

namespace WebApiTerra1000.Controllers;

public class ContragentControllerV2 : WebControllerBase
{
    #region Constructor and destructor

    public ContragentControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragent/")]
    public ContentResult GetContragentFromCodeIdProd([FromQuery] string code, long id, 
        [FromQuery(Name = "format")] string formatString = "") =>
        GetContragentFromCodeIdWork(code != null
            ? SqlQueriesContragentsV2.GetContragentFromCodeProd : SqlQueriesContragentsV2.GetContragentFromIdProd,
            code, id, formatString);

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragent_preview/")]
    public ContentResult GetContragentFromCodeIdPreview([FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string formatString = "") =>
        GetContragentFromCodeIdWork(code != null
            ? SqlQueriesContragentsV2.GetContragentFromCodePreview : SqlQueriesContragentsV2.GetContragentFromIdPreview,
            code, id, formatString);

    private ContentResult GetContragentFromCodeIdWork([FromQuery] string url, [FromQuery] string code, 
        [FromQuery] long id, [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url,
                code != null ? WebUtils.Sql.GetParametersV2(code) : WebUtils.Sql.GetParametersV2(id));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragents/")]
    public ContentResult GetContragentsProd([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string formatString = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesOffsetProd, startDate, endDate, offset, rowCount, formatString);
        else if (startDate != null && endDate != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesProd, startDate, endDate, offset, rowCount, formatString);
        else if (startDate != null && endDate == null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromStartDateProd, startDate, endDate, offset, rowCount, formatString);
        return GetContragentsEmptyWork(SqlQueriesContragentsV2.GetContragentsEmptyProd, formatString);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragents_preview/")]
    public ContentResult GetContragentsPreview([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string formatString = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesOffsetPreview, startDate, endDate, offset, rowCount, formatString);
        else if (startDate != null && endDate != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesPreview, startDate, endDate, offset, rowCount, formatString);
        else if (startDate != null && endDate == null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromStartDatePreview, startDate, endDate, offset, rowCount, formatString);
        return GetContragentsEmptyWork(SqlQueriesContragentsV2.GetContragentsEmptyPreview, formatString);
    }

    private ContentResult GetContragentsEmptyWork([FromQuery] string url, [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url);
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);
    }

    private ContentResult GetContragentsWork([FromQuery] string url, [FromQuery] DateTime? startDate = null, 
        [FromQuery] DateTime? endDate = null, [FromQuery] int? offset = null, [FromQuery] int? rowCount = null,
        [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            List<SqlParameter> parameters = null;
            if (startDate != null && endDate != null && offset != null && rowCount != null)
                parameters = WebUtils.Sql.GetParametersV2(startDate, endDate, offset, rowCount);
            else if (startDate != null && endDate != null)
                parameters = WebUtils.Sql.GetParametersV2(startDate, endDate);
            else if (startDate != null && endDate == null)
                parameters = WebUtils.Sql.GetParametersV2(startDate);
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url, parameters);
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}
