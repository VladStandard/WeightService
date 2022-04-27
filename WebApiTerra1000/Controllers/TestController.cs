// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Settings;
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
using WebApiTerra1000.Common;
using WebApiTerra1000.Utils;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class TestController : BaseController
    {
        #region Public and private fields and properties

        private readonly AppVersionHelper AppVersion = AppVersionHelper.Instance;

        #endregion

        #region Constructor and destructor

        public TestController(ILogger<TestController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/info/")]
        public ContentResult GetInfo(FormatType format = FormatType.Xml) =>
            Controller.RunTask(new Task<ContentResult>(() =>
            {
                AppVersion.Setup(Assembly.GetExecutingAssembly());

                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueries.GetDateTimeNow);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                string response = sqlQuery.UniqueResult<string>();
                transaction.Commit();

                return new ServiceInfoEntity(AppVersion.App, AppVersion.Version,
                    DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    response.ToString(CultureInfo.InvariantCulture),
                    session.Connection.ConnectionString.ToString(),
                    session.Connection.ConnectionTimeout,
                    session.Connection.DataSource,
                    session.Connection.ServerVersion,
                    session.Connection.Database,
                    (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
                    (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576)
                .GetResult(format, HttpStatusCode.OK);
            }), format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/exception/")]
        public ContentResult GetException(FormatType format = FormatType.Xml) =>
            Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetException);

                return new SqlSimpleV1Entity(response).GetResult(format, HttpStatusCode.OK);
            }), format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/simple/")]
        public ContentResult GetSimple(FormatType format = FormatType.Xml, int version = 0)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                switch (version)
                {
                    case 1:
                        string response1 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV1);
                        return SqlSimpleV1Entity.DeserializeFromXml(response1).GetResult(format, HttpStatusCode.OK);
                    case 2:
                        string response2 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV2);
                        return SqlSimpleV2Entity.DeserializeFromXml(response2).GetResult(format, HttpStatusCode.OK);
                    case 3:
                        string response3 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV3);
                        return SqlSimpleV3Entity.DeserializeFromXml(response3).GetResult(format, HttpStatusCode.OK);
                    case 4:
                        string response4 = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetXmlSimpleV4);
                        return SqlSimpleV4Entity.DeserializeFromXml(response4).GetResult(format, HttpStatusCode.OK);
                }
                
                return new SqlSimpleV1Entity("Simple method from C Sharp").GetResult(format, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
