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
using WebApiTerra1000.Common;

namespace WebApiTerra1000.Controllers
{
    public class ContragentController : BaseController
    {
        #region Constructor and destructor

        public ContragentController(ILogger<ContragentController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragents-test/")]
        public ContentResult GetContragentsTest()
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                XDocument response = new(
                    new XElement("Response",
                        new XElement("Contragents",
                            new XElement("Result") { Value = "Success" }
                        )
                    ));
                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = response.ToString()
                };
            }));
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragent/")]
        public ContentResult GetContragent(int id)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetContragent)
                    .SetParameter("ID", id)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Contragent />", LoadOptions.None);
                XDocument doc = new(new XElement("response", xml.Root));
                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = doc.ToString()
                };
            }));
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragents/")]
        public ContentResult GetContragents(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetContragents)
                    .SetParameter("StartDate", startDate)
                    .SetParameter("EndDate", endDate)
                    .SetParameter("Offset", offset)
                    .SetParameter("RowCount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Contragents />", LoadOptions.None);
                XDocument doc = new(new XElement("response", xml.Root));
                return new ContentResult
                {
                    ContentType = "application/xml",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = doc.ToString()
                };
            }));
        }

        #endregion
    }
}
