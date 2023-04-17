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

public sealed class DeliveryPlaceControllerV2 : WsControllerBase
{
    #region Constructor and destructor

    public DeliveryPlaceControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetDeliveryPlacesV2)]
    public ContentResult GetDeliveryPlaces([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int offset = 0,
        [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "") =>
        GetDeliveryPlacesWork(WsWebSqlQueriesV2.GetDeliveryPlaces, startDate, endDate, offset, rowCount, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetDeliveryPlacesV2Preview)]
    public ContentResult GetDeliveryPlacesPreview([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int offset = 0, 
        [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "") =>
        GetDeliveryPlacesWork(WsWebSqlQueriesV2.GetDeliveryPlacesPreview, startDate, endDate, offset, rowCount, format);

    private ContentResult GetDeliveryPlacesWork([FromQuery] string url, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "")
    {
        return GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, url,
                WsWebSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WsWebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
