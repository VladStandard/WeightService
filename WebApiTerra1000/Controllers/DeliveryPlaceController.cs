// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using WsLocalization.Utils;
using WsStorage.Utils;
using WsWebApi.Helpers;
using WsWebApi.Utils;

namespace WebApiTerra1000.Controllers;

public class DeliveryPlaceController : WsWebControllerBase
{
    #region Constructor and destructor

    public DeliveryPlaceController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetDeliveryPlaces)]
    public ContentResult GetDeliveryPlaces([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string format = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WsWebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetDeliveryPlaces,
                WsWebUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
