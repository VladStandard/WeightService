// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            var xdoc = XDocument.Parse(doc.ToString());
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xdoc.ToString(), Encoding.UTF8, "application/xml");
            return response;
        }

        // GET: api/Nomenclature/?StartDate=2020-04-20T00:00:00&EndDate=2020-04-21T00:00:00&Offset=0&RowCount=10
        public HttpResponseMessage Get(DateTime startDate, DateTime endDate, int offset = 0, int rowCount = 10)
        {

            Errors.Clear();
            var doc = new XDocument(new XElement("response"));

            using (var conn = SqlHelper.GetConnection())
            {
                var sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter($"@StartDate", startDate));
                sqlParameters.Add(new SqlParameter($"@EndDate", endDate));
                sqlParameters.Add(new SqlParameter($"@Offset", offset));
                sqlParameters.Add(new SqlParameter($"@RowCount", rowCount));

                using (var cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetNomenclatureChangesList] (@StartDate, @EndDate, @Offset, @RowCount)", conn, sqlParameters))
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

            }

        }

        // GET: api/Nomenclature/?id=1
        public HttpResponseMessage Get(int id)
        {
            var doc = new XDocument(new XElement("response"));

            using (var conn = SqlHelper.GetConnection())
            {
                var sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter($"@ID", id));

                using (var cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetNomenclatureByID] (@ID)", conn, sqlParameters))
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

            }

        }

        #endregion
    }
}