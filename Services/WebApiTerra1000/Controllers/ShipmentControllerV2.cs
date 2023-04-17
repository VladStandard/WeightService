// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Data;
using System.Net;
using System.Xml.Linq;
using WebApiTerra1000.Utils;
using WsLocalizationCore.Utils;
using WsStorageCore.Utils;
using WsWebApiCore.Base;
using WsWebApiCore.Utils;

namespace WebApiTerra1000.Controllers;

public sealed class ShipmentControllerV2 : WsControllerBase
{
    #region Constructor and destructor

    public ShipmentControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods - API

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetShipmentV2)]
    public ContentResult GetShipment([FromQuery] long id, [FromQuery(Name = "format")] string format = "") => 
        GetShipmentWork(WsWebSqlQueriesV2.GetShipment, id, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetShipmentV2Preview)]
    public ContentResult GetShipmentPreview([FromQuery] long id, [FromQuery(Name = "format")] string format = "") => 
        GetShipmentWork(WsWebSqlQueriesV2.GetShipmentPreview, id, format);

    private ContentResult GetShipmentWork(string url, long id, string format)
    {
        return GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, url, new SqlParameter("ID", id));
            XDocument xml = XDocument.Parse($"<{WsWebConstants.Shipments} />", LoadOptions.None);
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
                    xml = XDocument.Parse(xmlOutput.Value.ToString() ?? $"<{WsWebConstants.Shipments} />", LoadOptions.None);
                }
            }
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc, HttpStatusCode.OK);
        }, format);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetShipmentsV2)]
    public ContentResult GetShipments([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "") =>
        GetShipmentsCore(WsWebSqlQueriesV2.GetShipments, startDate, endDate, offset, rowCount, format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetShipmentsV2Preview)]
    public ContentResult GetShipmentsPreview([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, 
        [FromQuery] int offset = 0, [FromQuery] int rowCount = 10, [FromQuery(Name = "format")] string format = "") =>
        GetShipmentsCore(WsWebSqlQueriesV2.GetShipmentsPreview, startDate, endDate, offset, rowCount, format);

    private ContentResult GetShipmentsCore(string url, DateTime startDate, DateTime endDate, 
        int offset = 0, int rowCount = 10, string format = "")
    {
        return GetContentResult(() =>
        {
            string response = WsWebSqlUtils.GetResponse<string>(SessionFactory, url, 
                WsWebSqlUtils.GetParameters(startDate, endDate, offset, rowCount));
            XDocument xml = xml = XDocument.Parse($"<{WsWebConstants.Shipments} />", LoadOptions.None);
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
                    xml = XDocument.Parse(xmlOutput.Value.ToString() ?? $"<{WsWebConstants.Shipments} />", LoadOptions.None);
                }
            }
            XDocument doc = new(new XElement(WsWebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetContentResult(format, doc.ToString(), HttpStatusCode.OK);
        }, format);
    }

    #endregion
}
