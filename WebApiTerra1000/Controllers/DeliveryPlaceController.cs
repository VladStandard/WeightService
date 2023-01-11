// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using WsStorageCore.Utils;
using WsWebApiCore.Controllers;
using WsWebApiCore.Models;

namespace WebApiTerra1000.Controllers;

public class DeliveryPlaceController : WebControllerBase
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
    [Route("api/deliveryplaces/")]
    public ContentResult GetDeliveryPlaces([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 100, [FromQuery(Name = "format")] string formatString = "")
    {
        return ControllerHelp.GetContentResult(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetDeliveryPlaces,
                WebUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(formatString, doc, HttpStatusCode.OK);
        }, formatString);
    }

    #endregion
}
