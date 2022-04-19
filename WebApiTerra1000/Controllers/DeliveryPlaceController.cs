// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiTerra1000.Common;
using WebApiTerra1000.Utils;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class DeliveryPlaceController : BaseController
    {
        #region Constructor and destructor

        public DeliveryPlaceController(ILogger<DeliveryPlaceController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/deliveryplaces/")]
        public ContentResult GetDeliveryPlaces(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
            FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetDeliveryPlaces,
                    TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.DeliveryPlaces} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
