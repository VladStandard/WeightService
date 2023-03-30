// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using WsLocalization.Utils;
using WsStorage.Utils;
using WsWebApi.Base;
using WsWebApi.Utils;

namespace WebApiTerra1000.Controllers;

internal sealed class NomenclatureController : WsWebControllerBase
{
    #region Constructor and destructor

    public NomenclatureController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetNomenclature)]
    public ContentResult GetNomenclature([FromQuery] string code, [FromQuery] long id, [FromQuery(Name = "format")] string format = "")
    {
        return GetContentResult(() =>
        {
            string response = string.IsNullOrEmpty(code)
                ? WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclatureFromId, new SqlParameter("id", id))
                : WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclatureFromCode, new SqlParameter("code", code));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route( UrlWebService.GetNomenclatures)]
    public ContentResult GetNomenclatures([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "")
    {
        return GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclatures,
                WsWebSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route( UrlWebService.GetNomenclaturesCosts)]
    public ContentResult GetNomenclaturesCosts([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "")
    {
        return GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, SqlQueries.GetNomenclaturesCosts,
                WsWebSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
