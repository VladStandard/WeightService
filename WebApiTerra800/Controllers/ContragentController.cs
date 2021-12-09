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
    public class ContragentController : ApiController
    {

        private readonly ErrorContainer errors = new();

        private HttpResponseMessage GetResponse(XDocument doc)
        {
            XDocument xdoc =  XDocument.Parse(doc.ToString() );
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(xdoc.ToString(), Encoding.UTF8, "application/xml");
            return response;
        }


        // GET: api/Contragent/?StartDate=2020-04-20T00:00:00&EndDate=2020-04-21T00:00:00&Offset=0&RowCount=10
        public HttpResponseMessage Get(DateTime StartDate, DateTime EndDate, int Offset = 0, int RowCount = 10)
        {

            errors.Clear();
            XDocument doc = new(new XElement("response"));

            using SqlConnection conn = SqlHelper.GetConnection();

            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter($"@StartDate", StartDate),
                new SqlParameter($"@EndDate", EndDate),
                new SqlParameter($"@Offset", Offset),
                new SqlParameter($"@RowCount", RowCount)
            };

            using SqlCommand cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetContragentChangesList] (@StartDate, @EndDate, @Offset, @RowCount)", conn, sqlParameters);
            try
            {

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

        // GET: api/Contragent/id
        public HttpResponseMessage Get(int id)
        {
            XDocument doc = new(new XElement("response"));

            using SqlConnection conn = SqlHelper.GetConnection();

            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter($"@ID", id)
            };

            using SqlCommand cmd = SqlHelper.GetCommand("SELECT [IIS].[fnGetContragentByID] (@ID)", conn, sqlParameters);
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
