// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using WsLocalization.Utils;
using WsStorage.Utils;
using WsWebApi.Controllers;
using WsWebApi.Models;
using WsWebApi.Utils;

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
    [HttpGet]
    [Route(UrlWebService.GetContragentV2)]
    public ContentResult GetContragentFromCodeIdProd([FromQuery] string code, long id, 
        [FromQuery(Name = "format")] string format = "") =>
        GetContragentFromCodeIdWork(code != null
            ? SqlQueriesContragentsV2.GetContragentFromCodeProd : SqlQueriesContragentsV2.GetContragentFromIdProd,
            code, id, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetContragentV2Preview)]
    public ContentResult GetContragentFromCodeIdPreview([FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "") =>
        GetContragentFromCodeIdWork(code != null
            ? SqlQueriesContragentsV2.GetContragentFromCodePreview : SqlQueriesContragentsV2.GetContragentFromIdPreview,
            code, id, format);

    private ContentResult GetContragentFromCodeIdWork([FromQuery] string url, [FromQuery] string code, 
        [FromQuery] long id, [FromQuery(Name = "format")] string format = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url,
                code != null ? WebUtils.Sql.GetParametersV2(code) : WebUtils.Sql.GetParametersV2(id));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetContragentsV2)]
    public ContentResult GetContragentsProd([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesOffsetProd, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesProd, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate == null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromStartDateProd, startDate, endDate, offset, rowCount, format);

        return GetContragentsEmptyWork(SqlQueriesContragentsV2.GetContragentsEmptyProd, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetContragentsV2Preview)]
    public ContentResult GetContragentsPreview([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesOffsetPreview, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesPreview, startDate, endDate, offset, rowCount, format);
        if (startDate != null && endDate == null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromStartDatePreview, startDate, endDate, offset, rowCount, format);

        return GetContragentsEmptyWork(SqlQueriesContragentsV2.GetContragentsEmptyPreview, format);
    }

    private ContentResult GetContragentsEmptyWork([FromQuery] string url, [FromQuery(Name = "format")] string format = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url);
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    private ContentResult GetContragentsWork([FromQuery] string url, [FromQuery] DateTime? startDate = null, 
        [FromQuery] DateTime? endDate = null, [FromQuery] int? offset = null, [FromQuery] int? rowCount = null,
        [FromQuery(Name = "format")] string format = "")
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
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
