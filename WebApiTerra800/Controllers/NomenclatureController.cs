// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;
using terra.Common;

namespace terra.Controllers
{
    public class NomenclatureController : ApiController
    {
        #region Public and private fields and properties

        private ErrorContainer Errors { get; set; } = new ErrorContainer();

        #endregion

        #region Public and private methods

        private HttpResponseMessage GetResponse(XDocument doc)
        {
            XDocument xdoc = XDocument.Parse(doc.ToString());
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xdoc.ToString(), Encoding.UTF8, "application/xml");
            return response;
        }

        // GET: api/Nomenclature/?StartDate=2020-04-20T00:00:00&EndDate=2020-04-21T00:00:00&Offset=0&RowCount=10
        public HttpResponseMessage Get(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10)
        {

            Errors.Clear();
            XDocument doc = new(new XElement("response"));

            using SqlConnection conn = SqlHelper.GetConnection();
            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter($"@StartDate", startDate),
                new SqlParameter($"@EndDate", endDate),
                new SqlParameter($"@Offset", offset),
                new SqlParameter($"@RowCount", rowCount)
            };

            using SqlCommand cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetNomenclatureChangesList] (@StartDate, @EndDate, @Offset, @RowCount)", conn, sqlParameters);
            try
            {
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {
                    xmlReader.Read();
                    if (xmlReader.MoveToContent() != XmlNodeType.None)
                    {
                        XElement someElement = XElement.Load(xmlReader.ReadSubtree());
                        doc.Root.Add(someElement);
                    }
                    else
                    {
                        Errors.Add("No objects.");
                    }
                }

                doc.Root.Add(Errors.GetXElement());
                return GetResponse(doc);

            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
                doc.Root.Add(Errors.GetXElement());
                return GetResponse(doc);
            }

        }

        // GET: api/Nomenclature/?id=1
        public HttpResponseMessage Get(long id)
        {
            XDocument doc = new(new XElement("response"));

            using SqlConnection conn = SqlHelper.GetConnection();
            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter($"@ID", id)
            };

            using SqlCommand cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetNomenclatureByID] (@ID)", conn, sqlParameters);
            try
            {
                using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                {

                    xmlReader.Read();
                    if (xmlReader.MoveToContent() != XmlNodeType.None)
                    {
                        XElement someElement = XElement.Load(xmlReader.ReadSubtree());
                        doc.Root.Add(someElement);
                    }
                    else
                    {
                        Errors.Add("No objects.");
                    }

                }

                doc.Root.Add(Errors.GetXElement());
                return GetResponse(doc);

            }
            catch (Exception ex)
            {
                Errors.Add(ex.Message);
                doc.Root.Add(Errors.GetXElement());
                return GetResponse(doc);
            }

        }

        #endregion
    }
}