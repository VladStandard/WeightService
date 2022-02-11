// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    public class NomenclatureController : BaseController
    {
        #region Constructor and destructor

        public NomenclatureController(ILogger<NomenclatureController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclature/")]
        public ContentResult GetNomenclature(string code, int id, FormatType format = FormatType.Xml) =>
            GetNomenclatureWork(SqlQueries.GetNomenclatureFromId, SqlQueries.GetNomenclatureFromCode, code, id, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclature_preview/")]
        public ContentResult GetNomenclaturePreview(string code, int id, FormatType format = FormatType.Xml) =>
            GetNomenclatureWork(SqlQueries.GetNomenclatureFromIdPreview, SqlQueries.GetNomenclatureFromCodePreview, code, id, format);

        private ContentResult GetNomenclatureWork(string urlId, string urlCode, string code, int id, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = string.IsNullOrEmpty(code)
                    ? TerraUtils.Sql.GetResponse<string>(SessionFactory, urlId, new SqlParameter("id", id))
                    : TerraUtils.Sql.GetResponse<string>(SessionFactory, urlCode, new SqlParameter("code", code));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclatures/")]
        public ContentResult GetNomenclatures(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) =>
            GetNomenclaturesWork(SqlQueries.GetNomenclatures, startDate, endDate, offset, rowCount, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclatures_preview/")]
        public ContentResult GetNomenclaturesPreview(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) =>
            GetNomenclaturesWork(SqlQueries.GetNomenclaturesPreview, startDate, endDate, offset, rowCount, format);

        private ContentResult GetNomenclaturesWork(string url, DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url,
                    TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclaturescosts/")]
        public ContentResult GetNomenclaturesCosts(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) => 
            GetNomenclaturesCostsWork(SqlQueries.GetNomenclaturesCosts, startDate, endDate, offset, rowCount, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/nomenclaturescosts_preview/")]
        public ContentResult GetNomenclaturesCostspreview(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) => 
            GetNomenclaturesCostsWork(SqlQueries.GetNomenclaturesCostsPreview, startDate, endDate, offset, rowCount, format);

        private ContentResult GetNomenclaturesCostsWork(string url, DateTime startDate, DateTime endDate, int offset = 0, 
            int rowCount = 10, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url,
                    TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
