// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Configuration;
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
    public class ContragentController : ApiController
    {

        private string dbConnString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
        private ErrorContainer errors = new ErrorContainer();

        private HttpResponseMessage GetResponse(XDocument doc)
        {
            var xdoc =  XDocument.Parse(doc.ToString() );
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xdoc.ToString(), Encoding.UTF8, "application/xml");
            return response;
        }


        // GET: api/Contragent/?StartDate=2020-04-20T00:00:00&EndDate=2020-04-21T00:00:00&Offset=0&RowCount=10
        public HttpResponseMessage Get(DateTime StartDate, DateTime EndDate, int Offset = 0, int RowCount = 10)
        {

            errors.Clear();
            var doc = new XDocument(new XElement("response"));

            using (var conn = new SqlConnection(dbConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT [IIS].[fnGetContragentChangesList] (@StartDate, @EndDate, @Offset, @RowCount)", conn))
                {
                    try
                    {
                        cmd.Parameters.Add(new SqlParameter($"@StartDate",      StartDate));
                        cmd.Parameters.Add(new SqlParameter($"@EndDate",        EndDate));
                        cmd.Parameters.Add(new SqlParameter($"@Offset",         Offset));
                        cmd.Parameters.Add(new SqlParameter($"@RowCount",       RowCount));

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

        // GET: api/Contragent/id
        public HttpResponseMessage Get(int id)
        {
            var doc = new XDocument(new XElement("response"));

            using (var conn = new SqlConnection(dbConnString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT [IIS].[fnGetContragentByID] (@ID)", conn))
                {
                    try
                    {
                        cmd.Parameters.Add(new SqlParameter($"@ID", id));
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
