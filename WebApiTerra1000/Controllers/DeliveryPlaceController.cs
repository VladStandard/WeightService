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
using DataCore.Models;
using WebApiCore.Controllers;
using WebApiCore.Utils;
using WebApiCore.Models;

namespace WebApiTerra1000.Controllers;

public class DeliveryPlaceController : BaseController
{
    #region Constructor and destructor

    public DeliveryPlaceController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/deliveryplaces/")]
    public ContentResult GetDeliveryPlaces(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
        FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetDeliveryPlaces,
                WebUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.DeliveryPlaces} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
