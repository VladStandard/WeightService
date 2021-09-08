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
    public class NomenclatureController : BaseController
    {
        #region Constructor and destructor

        public NomenclatureController(ILogger<BaseController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclature/")]
        public ContentResult GetNomenclature(string code, int id)
        {
            return Task.RunTask(new Task<ContentResult>(() =>
            {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string sql = !string.IsNullOrEmpty(code)
                    ? "SELECT [IIS].[fnGetNomenclatureByCode] (:code)"
                    : "SELECT [IIS].[fnGetNomenclatureByID] (:id)";
                string response = !string.IsNullOrEmpty(code)
                    ? session.CreateSQLQuery(sql)
                        .SetParameter("code", code)
                        .UniqueResult<string>()
                    : session.CreateSQLQuery(sql)
                        .SetParameter("id", id)
                        .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Nomenclature />", LoadOptions.None);
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
        [Route("api/nomenclatures/")]
        public ContentResult GetNomenclatures(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10)
        {
            return Task.RunTask(new Task<ContentResult>(() => {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                const string sql = "SELECT [IIS].[fnGetNomenclatureChangesList] (:StartDate, :EndDate, :Offset, :RowCount)";
                string response = session.CreateSQLQuery(sql)
                    .SetParameter("StartDate", startDate)
                    .SetParameter("EndDate", endDate)
                    .SetParameter("Offset", offset)
                    .SetParameter("RowCount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Nomenclature />", LoadOptions.None);
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
        [Route("api/nomenclaturescosts/")]
        public ContentResult GetNomenclaturesCosts(int offset = 0, int rowCount = 10)
        {
            return Task.RunTask(new Task<ContentResult>(() => {
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                const string sql = "SELECT [IIS].[fnGetNomenclatureList] (:offset, :rowcount)";
                string response = session.CreateSQLQuery(sql)
                    .SetParameter("offset", offset)
                    .SetParameter("rowcount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();
                XDocument xml = XDocument.Parse(response ?? "<Nomenclature />", LoadOptions.None);
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
