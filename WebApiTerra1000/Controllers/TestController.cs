// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Terra.Common;
using WebApiTerra1000.Common;

namespace WebApiTerra1000.Controllers
{
    public class TestController : BaseController
    {
        #region Public and private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;

        #endregion

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
        public ContentResult GetTest()
        {
            _appVersion.Setup(Assembly.GetExecutingAssembly());
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                XDocument doc = null;
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetTest)
                    .UniqueResult<string>();
                transaction.Commit();

                if ((doc = ResponseUtils.GetNullOrEmpty(response)) != null)
                    return ResponseUtils.GetContentResult(Enums.FormatType.Xml, doc.ToString());
                if ((doc = ResponseUtils.GetError(response)) != null)
                    return ResponseUtils.GetContentResult(Enums.FormatType.Xml, doc.ToString());
                
                doc = new(
                    new XElement("Response",
                        new XElement("App", _appVersion.App),
                        new XElement("Version", _appVersion.Version),
                        new XElement("WinCurrentDate", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                        new XElement("SqlCurrentDate", response.ToString(CultureInfo.InvariantCulture)),
                        new XElement("ConnectionString", session.Connection.ConnectionString.ToString()),
                        new XElement("ConnectTimeout", session.Connection.ConnectionTimeout.ToString()),
                        new XElement("DataSource", session.Connection.DataSource),
                        new XElement("ServerVersion", session.Connection.ServerVersion),
                        new XElement("Database", session.Connection.Database),
                        new XElement("PhysicalMegaBytes", (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576),
                        new XElement("VirtualMegaBytes", (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576)
                    ));
                return ResponseUtils.GetContentResult(Enums.FormatType.Xml, doc.ToString());
            }));
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/deprecated/")]
        public ContentResult GetDeprecated()
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                XDocument doc = null;
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetDeprecated)
                    .UniqueResult<string>();
                transaction.Commit();

                if ((doc = ResponseUtils.GetNullOrEmpty(response)) != null)
                    return ResponseUtils.GetContentResult(Enums.FormatType.Xml, doc.ToString());
                if ((doc = ResponseUtils.GetError(response)) != null)
                    return ResponseUtils.GetContentResult(Enums.FormatType.Xml, doc.ToString());

                doc = ResponseUtils.GetUnknownError(response);
                return ResponseUtils.GetContentResult(Enums.FormatType.Xml, doc.ToString());
            }));
        }

        #endregion
    }
}
