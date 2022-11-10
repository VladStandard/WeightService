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

namespace WebApiTerra1000.Controllers;

public class DeliveryPlaceControllerV2 : BaseController
{
    #region Constructor and destructor

    public DeliveryPlaceControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/deliveryplaces/")]
    public ContentResult GetDeliveryPlaces(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
        FormatTypeEnum format = FormatTypeEnum.Xml) =>
        GetDeliveryPlacesWork(SqlQueriesV2.GetDeliveryPlaces, startDate, endDate, offset, rowCount, format);

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/deliveryplaces_preview/")]
    public ContentResult GetDeliveryPlacesPreview(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
        FormatTypeEnum format = FormatTypeEnum.Xml) =>
        GetDeliveryPlacesWork(SqlQueriesV2.GetDeliveryPlacesPreview, startDate, endDate, offset, rowCount, format);

    private ContentResult GetDeliveryPlacesWork(string url, DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
        FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url,
                WebUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
