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
    public class DeliveryPlaceControllerV2 : BaseController
    {
        #region Constructor and destructor

        public DeliveryPlaceControllerV2(ILogger<DeliveryPlaceControllerV2> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/deliveryplaces/")]
        public ContentResult GetDeliveryPlaces(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
            FormatType format = FormatType.Xml) =>
            GetDeliveryPlacesWork(SqlQueriesV2.GetDeliveryPlaces, startDate, endDate, offset, rowCount, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/deliveryplaces_preview/")]
        public ContentResult GetDeliveryPlacesPreview(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
            FormatType format = FormatType.Xml) =>
            GetDeliveryPlacesWork(SqlQueriesV2.GetDeliveryPlacesPreview, startDate, endDate, offset, rowCount, format);

        private ContentResult GetDeliveryPlacesWork(string url, DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
            FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url,
                    TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.DeliveryPlaces} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
