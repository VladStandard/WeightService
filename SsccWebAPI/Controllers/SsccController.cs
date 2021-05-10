using SsccWebAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;


namespace SsccWebAPI.Controllers
{
    public class SsccController : ApiController
    {

        private string dbConnString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
        private ErrorContainer errors = new ErrorContainer();

        private HttpResponseMessage GetResponse(XDocument doc)
        {
            XDocument xdoc = XDocument.Parse(doc.ToString());
            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xdoc.ToString(), Encoding.UTF8, "application/xml");
            return response;
        }


        // GET: /api/Sscc/?GLN=460710023&UnitType=1&RowCount=10
        public HttpResponseMessage Get(Int32 GLN = 460710023, Int32 UnitType = 1, Int32 RowCount = 10)
        {

            errors.Clear();
            XDocument doc = new XDocument(new XElement("response"));

            using (SqlConnection conn = new SqlConnection(dbConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "DECLARE @xmldata xml;"+
                    "EXECUTE [db_sscc].[GetSSCCxml] @GLN, @UnitType, @Count, @xmldata OUTPUT;"+
                    "SELECT @xmldata ", conn))
                {
                    try
                    {

                        cmd.Parameters.Add(new SqlParameter($"@GLN", GLN));
                        cmd.Parameters.Add(new SqlParameter($"@UnitType", UnitType));
                        cmd.Parameters.Add(new SqlParameter($"@Count", RowCount));
                        using (XmlReader xmlReader = cmd.ExecuteXmlReader())
                        {

                            if (xmlReader.MoveToContent() != XmlNodeType.None)
                            {
                                XElement someElement = XElement.Load(xmlReader.ReadSubtree());
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
