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

namespace Terra.Controllers
{
    public class SummaryController : BaseController
    {
        #region Constructor and destructor

        public SummaryController(ILogger<BaseController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/summary/")]
        public ContentResult GetSummary(DateTime startDate, DateTime endDate)
        {
            return Task.RunTask(new Task<ContentResult>(() => {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                const string sql = "SELECT [IIS].[fnGetSummaryList] (:StartDate, :EndDate)";
                string response = session.CreateSQLQuery(sql)
                    .SetParameter("StartDate", startDate)
                    .SetParameter("EndDate", endDate)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Summary />", LoadOptions.None);
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
