// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Terra.Controllers
{
    public class TestController : BaseController
    {
        #region Constructor and destructor

        public TestController(ILogger<TestController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/terra/")]
        [Route("api/test/")]
        public ContentResult GetTest(string id)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() => {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                const string sql = "SELECT SYSDATETIME() [CURRENT_TIME]";
                DateTime response = session.CreateSQLQuery(sql).UniqueResult<DateTime>();
                transaction.Commit();
                XDocument doc = new(
                    new XElement("Response",
                        new XElement("Message", "Current IIS DateTime"),
                        new XElement("IisCurrentDate", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                        new XElement("SqlCurrentDate", response.ToString(CultureInfo.InvariantCulture)),
                        new XElement("ConnectTimeout", session.Connection.ConnectionTimeout.ToString()),
                        new XElement("DataSource", session.Connection.DataSource),
                        new XElement("ServerVersion", session.Connection.ServerVersion),
                        new XElement("Database", session.Connection.Database),
                        !string.IsNullOrEmpty(id) ? new XElement("ID", id) : new XElement("ID", "is empty"),
                        new XElement("PhysicalMegaBytes", (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576),
                        new XElement("VirtualMegaBytes", (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576)
                    ));
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
        [Route("api/deprecated/")]
        public ContentResult GetDeprecated()
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() => {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                const string sql = "SELECT [IIS].[fnGetDeprecated]() [fnGetDeprecated]";
                string response = session.CreateSQLQuery(sql).UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Summary />", LoadOptions.None);
                XDocument doc = new(xml.Root);
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
