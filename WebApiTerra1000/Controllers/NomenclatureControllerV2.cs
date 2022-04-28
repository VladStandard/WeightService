// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiTerra1000.Common;
using WebApiTerra1000.Utils;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class NomenclatureControllerV2 : BaseController
    {
        #region Constructor and destructor

        //public NomenclatureControllerV2(ILogger<NomenclatureController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        public NomenclatureControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/nomenclature/")]
        public ContentResult GetNomenclatureFromCodeIdProd(string code, long id, FormatType format = FormatType.Xml) =>
            GetNomenclatureFromCodeIdWork(code != null 
                ? SqlQueriesNomenclaturesV2.GetNomenclatureFromCodeProd : SqlQueriesNomenclaturesV2.GetNomenclatureFromIdProd,
                code, id, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/nomenclature_preview/")]
        public ContentResult GetNomenclatureFromCodeIdPreview(string code, long id, FormatType format = FormatType.Xml) =>
            GetNomenclatureFromCodeIdWork(code != null 
                ? SqlQueriesNomenclaturesV2.GetNomenclatureFromCodePreview : SqlQueriesNomenclaturesV2.GetNomenclatureFromIdPreview,
                code, id, format);

        private ContentResult GetNomenclatureFromCodeIdWork(string url, string code, long id, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url,
                    code != null ? TerraUtils.Sql.GetParametersV2(code) : TerraUtils.Sql.GetParametersV2(id));
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/nomenclatures/")]
        public ContentResult GetNomenclaturesProd(DateTime? startDate, DateTime? endDate = null,
            int? offset = null, int? rowCount = null, FormatType format = FormatType.Xml)
        {
            if (startDate != null && endDate != null && offset != null && rowCount != null)
                return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesOffsetProd, startDate, endDate, offset, rowCount, format);
            else if (startDate != null && endDate != null)
                return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesProd, startDate, endDate, offset, rowCount, format);
            else if (startDate != null && endDate == null)
                return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromStartDateProd, startDate, endDate, offset, rowCount, format);
            return GetNomenclaturesEmptyWork(SqlQueriesNomenclaturesV2.GetNomenclaturesEmptyProd, format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/nomenclaturescosts/")]
        public ContentResult GetNomenclaturesProdDeprecated(FormatType format = FormatType.Xml) =>
            Controller.RunTask(new Task<ContentResult>(() =>
            {
                return new ServiceReplyEntity("Deprecated method. Use: api/nomenclatures/")
                .GetResult(format, HttpStatusCode.OK);
            }), format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/nomenclatures_preview/")]
        public ContentResult GetNomenclaturesPreview(DateTime? startDate, DateTime? endDate = null,
            int? offset = null, int? rowCount = null, FormatType format = FormatType.Xml)
        {
            if (startDate != null && endDate != null && offset != null && rowCount != null)
                return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesOffsetPreview, startDate, endDate, offset, rowCount, format);
            else if (startDate != null && endDate != null)
                return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromDatesPreview, startDate, endDate, offset, rowCount, format);
            else if (startDate != null && endDate == null)
                return GetNomenclaturesWork(SqlQueriesNomenclaturesV2.GetNomenclaturesFromStartDatePreview, startDate, endDate, offset, rowCount, format);
            return GetNomenclaturesEmptyWork(SqlQueriesNomenclaturesV2.GetNomenclaturesEmptyPreview, format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/nomenclaturescosts_preview/")]
        public ContentResult GetNomenclaturesPreviewDeprecated(FormatType format = FormatType.Xml) =>
            Controller.RunTask(new Task<ContentResult>(() =>
            {
                return new ServiceReplyEntity("Deprecated method. Use: api/nomenclatures_preview/")
                .GetResult(format, HttpStatusCode.OK);
            }), format);

        private ContentResult GetNomenclaturesEmptyWork(string url, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url);
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        private ContentResult GetNomenclaturesWork(string url, DateTime? startDate = null, DateTime? endDate = null,
            int? offset = null, int? rowCount = null, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                List<SqlParameter> parameters = null;
                if (startDate != null && endDate != null && offset != null && rowCount != null)
                    parameters = TerraUtils.Sql.GetParametersV2(startDate, endDate, offset, rowCount);
                else if (startDate != null && endDate != null)
                    parameters = TerraUtils.Sql.GetParametersV2(startDate, endDate);
                else if (startDate != null && endDate == null)
                    parameters = TerraUtils.Sql.GetParametersV2(startDate);
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url, parameters);
                XDocument xml = XDocument.Parse(response ?? $"<{TerraConsts.Goods} />", LoadOptions.None);
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
