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
using static WebApiTerra1000.Utils.TerraEnums;

namespace WebApiTerra1000.Controllers
{
    public class ContragentController : BaseController
    {
        #region Constructor and destructor

        public ContragentController(ILogger<ContragentController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragents-test/")]
        public ContentResult GetContragentsTest(FormatType format = FormatType.Raw)
        {
            //return TaskHelper.RunTask(new Task<ContentResult>(() =>
            //{
            //    string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, SqlQueries.GetException);
            //    SqlSimpleEntity simple = new(response);
            //    return format switch
            //    {
            //        FormatType.Json => TerraUtils.GetResult(FormatType.Json, simple.SerializeAsJson(), HttpStatusCode.OK),
            //        FormatType.Xml => TerraUtils.GetResult(FormatType.Xml, simple.SerializeAsXml(), HttpStatusCode.OK),
            //        FormatType.Html => TerraUtils.GetResult(FormatType.Html, simple.SerializeAsHtml(), HttpStatusCode.OK),
            //        FormatType.Text => TerraUtils.GetResult(FormatType.Text, simple.SerializeAsText(), HttpStatusCode.OK),
            //        FormatType.Raw => TerraUtils.GetResult(FormatType.Text, simple.SerializeAsText(), HttpStatusCode.OK),
            //        _ => throw TerraUtils.GetArgumentException(nameof(format)),
            //    };
            //}), format);
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                XDocument response = new(
                    new XElement(TerraConsts.Response,
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
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragent/")]
        public ContentResult GetContragent(int id, FormatType format = FormatType.Raw)
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
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/contragents/")]
        public ContentResult GetContragents(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10, 
            FormatType format = FormatType.Raw)
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
            }), format);
        }

        #endregion
    }
}
