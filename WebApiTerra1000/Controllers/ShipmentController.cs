// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
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
using WebApiTerra1000.Utils;
using static DataShareCore.ShareEnums;

namespace WebApiTerra1000.Controllers
{
    public class ShipmentController : BaseController
    {
        #region Constructor and destructor

        public ShipmentController(ILogger<ShipmentController> logger, ISessionFactory sessionFactory) : base(logger, sessionFactory)
        {
        }

        #endregion

        #region Public and private methods - API

        [AllowAnonymous]
        [HttpGet()]
        [Route("api/shipment/")]
        public ContentResult GetShipment(long id, FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                XDocument doc;
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetShipment)
                    .SetParameter("ID", id)
                    .UniqueResult<string>();
                transaction.Commit();
                if (response != null)
                {
                    IDbCommand command = new SqlCommand();
                    command.Connection = session.Connection;
                    command.CommandTimeout = session.Connection.ConnectionTimeout;
                    transaction.Enlist((System.Data.Common.DbCommand)command);

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[IIS].[GetShipments]";

                    // Set input parameter
                    SqlParameter IdInput = new("@jsonListId", SqlDbType.NVarChar);
                    IdInput.Direction = ParameterDirection.Input;
                    IdInput.Value = response;
                    command.Parameters.Add(IdInput);

                    // Set output parameter
                    SqlParameter XmlOutput = new("@xml", SqlDbType.Xml);
                    XmlOutput.Direction = ParameterDirection.Output;
                    command.Parameters.Add(XmlOutput);

                    SqlParameter returnParameter = new("@RETURN_VALUE", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    // Execute the stored procedure
                    command.ExecuteNonQuery();

                    XDocument xml;
                    if (XmlOutput.Value != DBNull.Value)
                    {
                        xml = XDocument.Parse(XmlOutput.Value.ToString() ?? "<Shipment />", LoadOptions.None);
                    }
                    else
                    {
                        xml = XDocument.Parse("<Shipment />", LoadOptions.None);
                    }
                    doc = new XDocument(new XElement("response", xml.Root));
                }
                else
                {
                    XDocument xml = XDocument.Parse("<Shipment />", LoadOptions.None);
                    doc = new XDocument(new XElement(TerraConsts.Response, xml.Root));
                }
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
        [Route("api/shipmentsbydocdate/")]
        [Route("api/shipments/")]
        public ContentResult GetShipments(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10,
            FormatType format = FormatType.Xml)
        {
            return TaskHelper.RunTask(new Task<ContentResult>(() =>
            {
                XDocument xml = null;
                XDocument doc = null;
                using ISession session = SessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                string response = session.CreateSQLQuery(SqlQueries.GetShipments)
                    .SetParameter("StartDate", startDate)
                    .SetParameter("EndDate", endDate)
                    .SetParameter("Offset", offset)
                    .SetParameter("RowCount", rowCount)
                    .UniqueResult<string>();
                transaction.Commit();

                //if ((doc = TerraUtils.Xml.GetNullOrEmpty(response)) != null)
                //    return TerraUtils.GetResult(FormatType.Xml, doc.ToString(), HttpStatusCode.OK);
                //if ((doc = TerraUtils.Xml.GetError(response)) != null)
                //    return TerraUtils.GetResult(FormatType.Xml, doc.ToString(), HttpStatusCode.OK);

                IDbCommand command = new SqlCommand();
                command.Connection = session.Connection;
                command.CommandTimeout = session.Connection.ConnectionTimeout;
                transaction.Enlist((System.Data.Common.DbCommand)command);

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[IIS].[GetShipments]";

                // Set input parameter
                SqlParameter IdInput = new("@jsonListId", SqlDbType.NVarChar);
                IdInput.Direction = ParameterDirection.Input;
                IdInput.Value = response;
                command.Parameters.Add(IdInput);

                // Set output parameter
                SqlParameter XmlOutput = new("@xml", SqlDbType.Xml);
                XmlOutput.Direction = ParameterDirection.Output;
                command.Parameters.Add(XmlOutput);

                SqlParameter returnParameter = new("@RETURN_VALUE", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(returnParameter);

                // Execute the stored procedure
                command.ExecuteNonQuery();

                if (XmlOutput.Value != DBNull.Value)
                {
                    xml = XDocument.Parse(XmlOutput.Value.ToString() ?? "<Shipments />", LoadOptions.None);
                }
                else
                {
                    xml = XDocument.Parse("<Shipments />", LoadOptions.None);
                }
                doc = new XDocument(new XElement(TerraConsts.Response, xml.Root));
                return BaseSerializeEntity<XDocument>.GetResult(format, doc.ToString(), HttpStatusCode.OK);
            }), format);
        }

        #endregion
    }
}