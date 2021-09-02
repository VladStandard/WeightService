// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using terra.Common;

namespace terra.Controllers
{
    public class ShipmentController : ApiController
    {

        private ErrorContainer errors = new ErrorContainer();

        private HttpResponseMessage GetResponse(XDocument doc)
        {
            var xdoc = XDocument.Parse(doc.ToString());
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xdoc.ToString(), Encoding.UTF8, "application/xml");
            return response;
        }


        // GET: api/Shipment/?StartDate=2020-04-20T00:00:00&EndDate=2020-04-21T00:00:00&Offset=0&RowCount=10
        public HttpResponseMessage Get(DateTime StartDate, DateTime EndDate, int Offset = 0, int RowCount = 10)
        {

            errors.Clear();
            var doc = new XDocument(new XElement("response"));

            using (var conn = SqlHelper.GetConnection())
            {
                var sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter($"@StartDate", StartDate));
                sqlParameters.Add(new SqlParameter($"@EndDate", EndDate));
                sqlParameters.Add(new SqlParameter($"@Offset", Offset));
                sqlParameters.Add(new SqlParameter($"@RowCount", RowCount));
                using (var cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetShipmentChangesList] (@StartDate, @EndDate, @Offset, @RowCount)", conn, sqlParameters))
                {
                    try
                    {
                        using (var xmlReader = cmd.ExecuteXmlReader())
                        {

                            if (xmlReader.MoveToContent() != XmlNodeType.None)
                            {
                                var someElement = XElement.Load(xmlReader.ReadSubtree());
                                doc.Root.Add(someElement);
                            }
                            else
                            {
                                errors.Add("No objects.");
                            }

                        }

                        doc.Root.Add(errors.GetXElement());
                        return GetResponse(doc);

                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                        doc.Root.Add(errors.GetXElement());
                        return GetResponse(doc);
                    }

                }

            }

        }

        // GET: api/Shipment/id
        public HttpResponseMessage Get(Int64 id)
        {
            var doc = new XDocument(new XElement("response"));

            using (var conn = SqlHelper.GetConnection())
            {
                var sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter($"@ID", id));

                using (var cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetShipmentByID] (@ID)", conn, sqlParameters))
                {
                    try
                    {
                        using (var xmlReader = cmd.ExecuteXmlReader())
                        {
                            xmlReader.Read();
                            if (xmlReader.MoveToContent() != XmlNodeType.None)
                            {
                                var someElement = XElement.Load(xmlReader.ReadSubtree());
                                doc.Root.Add(someElement);
                            }
                            else
                            {
                                errors.Add("No objects.");
                            }

                        }
                        doc.Root.Add(errors.GetXElement());
                        return GetResponse(doc);

                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                        doc.Root.Add(errors.GetXElement());
                        return GetResponse(doc);
                    }

                }

            }

        }

    }

}
