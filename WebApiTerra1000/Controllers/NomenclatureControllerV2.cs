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
using WsStorage.Utils;
using WsWebApi.Controllers;
using WsWebApi.Models;
using WsWebApi.Utils;

namespace WebApiTerra1000.Controllers;

public class NomenclatureControllerV2 : WebControllerBase
{
    #region Constructor and destructor

    public NomenclatureControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/nomenclature/")]
    public ContentResult GetNomenclatureFromCodeIdProd([FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "") =>
        GetNomenclatureFromCodeIdWork(code != null 
            ? SqlQueriesNomenclaturesV2.GetNomenclatureFromCodeProd : SqlQueriesNomenclaturesV2.GetNomenclatureFromIdProd,
            code, id, format);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/nomenclature_preview/")]
    public ContentResult GetNomenclatureFromCodeIdPreview([FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "") =>
        GetNomenclatureFromCodeIdWork(code != null 
            ? SqlQueriesNomenclaturesV2.GetNomenclatureFromCodePreview : SqlQueriesNomenclaturesV2.GetNomenclatureFromIdPreview,
            code, id, format);

    private ContentResult GetNomenclatureFromCodeIdWork([FromQuery] string url, [FromQuery] string code, [FromQuery] long id,
        [FromQuery(Name = "format")] string format = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url,
                code != null ? WebUtils.Sql.GetParametersV2(code) : WebUtils.Sql.GetParametersV2(id));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/nomenclatures/")]
    public ContentResult GetNomenclaturesProd([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesOffsetProd, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate != null)
            return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesProd, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate == null)
            return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromStartDateProd, startDate, endDate, offset, rowCount, format);
        return GetNomenclaturesEmptyWork(SqlQueriesNomenclaturesV2.GetNomenclaturesEmptyProd, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/nomenclaturescosts/")]
    public ContentResult GetNomenclaturesProdDeprecated([FromQuery(Name = "format")] string format = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            return new ServiceReplyModel("Deprecated method. Use: api/nomenclatures/")
            .GetContentResult<ServiceReplyModel>(format, HttpStatusCode.OK);
        }, format);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/nomenclatures_preview/")]
    public ContentResult GetNomenclaturesPreview([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate = null,
        [FromQuery] int? offset = null, [FromQuery] int? rowCount = null, [FromQuery(Name = "format")] string format = "")
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesOffsetPreview, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate != null)
            return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesPreview, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate == null)
            return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromStartDatePreview, startDate, endDate, offset, rowCount, format);
        return GetNomenclaturesEmptyWork(SqlQueriesNomenclaturesV2.GetNomenclaturesEmptyPreview, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("api/v2/nomenclaturescosts_preview/")]
    public ContentResult GetNomenclaturesPreviewDeprecated([FromQuery(Name = "format")] string format = "") =>
        ControllerHelp.GetContentResult(() =>
        {
            return new ServiceReplyModel("Deprecated method. Use: api/nomenclatures_preview/")
            .GetContentResult<ServiceReplyModel>(format, HttpStatusCode.OK);
        }, format);

    private ContentResult GetNomenclaturesEmptyWork(string url, string format = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url);
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    private ContentResult GetNomenclaturesWork(string url, DateTime? startDate = null, DateTime? endDate = null,
        int? offset = null, int? rowCount = null, [FromQuery(Name = "format")] string format = "")
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
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
