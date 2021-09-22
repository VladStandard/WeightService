// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using static DataShareCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class DeliveryPlaceController : BaseController
    {
        #region Constructor and destructor

        public DeliveryPlaceController(ILogger<DeliveryPlaceController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/deliveryplaces/")]
        public ContentResult GetDeliveryPlaces(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 100,
            FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetDeliveryPlaces)
                    .SetParameter("StartDate", startDate)
                    .SetParameter("EndDate", endDate)
                    .SetParameter("Offset", offset)
                    .SetParameter("RowCount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Response />", LoadOptions.None);
                //XDocument doc = new(new XElement(TerraConsts.ResponseRoot, xml.Root));
                XDocument doc = new(xml.Root);
                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = doc.ToString()
                };
            }), format);
        }

        #endregion
    }
}
