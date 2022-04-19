// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using NHibernate;
using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApiTerra1000.Common;
using WebApiTerra1000.Utils;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class ShipmentControllerV2 : BaseController
    {
        #region Constructor and destructor

        public ShipmentControllerV2(ILogger<ShipmentControllerV2> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods - API

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/shipment/")]
        public ContentResult GetShipment(long id, FormatType format = FormatType.Xml) => 
            GetShipmentWork(SqlQueriesV2.GetShipment, id, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/shipment_preview/")]
        public ContentResult GetShipmentPreview(long id, FormatType format = FormatType.Xml) => 
            GetShipmentWork(SqlQueriesV2.GetShipmentPreview, id, format);

        private ContentResult GetShipmentWork(string url, long id, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url, new SqlParameter("ID", id));
                XDocument xml = XDocument.Parse($"<{TerraConsts.Shipments} />", LoadOptions.None);
                if (response != null)
                {
                    using ISession session = SessionFactory.OpenSession();
                    using ITransaction transaction = session.BeginTransaction();
                    IDbCommand command = new SqlCommand()
                    {
                        Connection = session.Connection as SqlConnection,
                        CommandTimeout = session.Connection.ConnectionTimeout,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "[IIS].[GetShipments]",
                    };
                    transaction.Enlist((System.Data.Common.DbCommand)command);
                    // Parameters.
                    command.Parameters.Add(new SqlParameter("@jsonListId", SqlDbType.NVarChar) { Direction = ParameterDirection.Input, Value = response });
                    SqlParameter xmlOutput = new("@xml", SqlDbType.Xml) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(xmlOutput);
                    command.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });
                    // Execute.
                    command.ExecuteNonQuery();
                    if (xmlOutput.Value != DBNull.Value)
                    {
                        xml = XDocument.Parse(xmlOutput.Value.ToString() ?? $"<{TerraConsts.Shipments} />", LoadOptions.None);
                    }
                }
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
            }), format);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/shipments/")]
        public ContentResult GetShipments(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) =>
            GetShipmentsWork(SqlQueriesV2.GetShipments, startDate, endDate, offset, rowCount, format);

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/v2/shipments_preview/")]
        public ContentResult GetShipmentsPreview(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml) =>
            GetShipmentsWork(SqlQueriesV2.GetShipmentsPreview, startDate, endDate, offset, rowCount, format);

        private ContentResult GetShipmentsWork(string url, DateTime startDate, DateTime endDate, 
            int offset = 0, int rowCount = 10, FormatType format = FormatType.Xml)
        {
            return Controller.RunTask(new Task<ContentResult>(() =>
            {
                string response = TerraUtils.Sql.GetResponse<string>(SessionFactory, url, 
                    TerraUtils.Sql.GetParameters(startDate, endDate, offset, rowCount));
                XDocument xml = xml = XDocument.Parse($"<{TerraConsts.Shipments} />", LoadOptions.None);
                if (response != null)
                {
                    using ISession session = SessionFactory.OpenSession();
                    using ITransaction transaction = session.BeginTransaction();
                    IDbCommand command = new SqlCommand()
                    {
                        Connection = session.Connection as SqlConnection,
                        CommandTimeout = session.Connection.ConnectionTimeout,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "[IIS].[GetShipments]",
                    };
                    transaction.Enlist((System.Data.Common.DbCommand)command);
                    // Parameters.
                    command.Parameters.Add(new SqlParameter("@jsonListId", SqlDbType.NVarChar) { Direction = ParameterDirection.Input, Value = response });
                    SqlParameter xmlOutput = new("@xml", SqlDbType.Xml) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(xmlOutput);
                    command.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });
                    // Execute.
                    command.ExecuteNonQuery();
                    if (xmlOutput.Value != DBNull.Value)
                    {
                        xml = XDocument.Parse(xmlOutput.Value.ToString() ?? $"<{TerraConsts.Shipments} />", LoadOptions.None);
                    }
                }
                XDocument doc = new(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeDeprecatedEntity<XDocument>.GetResult(format, doc.ToString(), HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}
