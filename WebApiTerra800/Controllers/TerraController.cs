// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using terra.Common;

namespace terra.Controllers;

public class TerraController : ApiController
{
    private readonly List<string> ErrorList = new();
    private readonly ErrorContainer errors = new();

    // GET: api/Terra
    public HttpResponseMessage Get()
    {
        ErrorList.Clear();

        using Microsoft.Data.SqlClient.SqlConnection conn = SqlHelper.GetConnection();

        XDocument xResponse = new(
            new XElement("response",
                new XElement("Message", "Current IIS DataTime"),
                new XElement("CurrentDate", DateTime.Now.ToString()),
                new XElement("ConnectTimeout", conn.ConnectionTimeout.ToString()),
                new XElement("ServerVersion", conn.ServerVersion.ToString()),
                new XElement("Database", conn.Database.ToString())
            )
        );

        errors.Add("Successful");
        xResponse.Root.Add(errors.GetXElement());
        return GetResponse(xResponse);
    }

    // GET: api/Terra/?test=0 (1,2 ...)
    public HttpResponseMessage Get(int test)
    {
        errors.Clear();

        if (test == 0)
        {
            return BuildIISResponse();
        }
        else if (test == 1)
        {
            return BuildSQLResponse();
        }

        return BuildDummyResponse();
    }

    private HttpResponseMessage BuildSQLResponse()
    {
        using Microsoft.Data.SqlClient.SqlConnection conn = SqlHelper.GetConnection();

        using Microsoft.Data.SqlClient.SqlCommand cmd = SqlHelper.GetCommand("SELECT SYSDATETIME() as CurrentTime ", conn, null);
        try
        {
            DateTime res = (DateTime)cmd.ExecuteScalar();
            XDocument doc = new(
                new XElement("response",
                    new XElement("Message", "Current SQL DataTime"),
                    new XElement("CurrentDate", res.ToString()),
                    new XElement("ConnectTimeout", conn.ConnectionTimeout.ToString()),
                    new XElement("CommndTimeout", cmd.CommandTimeout.ToString()),
                    new XElement("ServerVersion", conn.ServerVersion.ToString()),
                    new XElement("Database", conn.Database.ToString())

                )
            );

            return GetResponse(doc);
        }
        catch (Exception ex)
        {
            errors.Add(ex.Message);
            XDocument doc = new();
            doc.Root.Add(errors.GetXElement());
            return GetResponse(doc);
        }
    }

    private HttpResponseMessage BuildIISResponse()
    {
        XDocument doc = new(
            new XElement("response",
                new XElement("Message", "Current IIS DataTime"),
                new XElement("CurrentDate", DateTime.Now.ToString())
            )
        );
        return GetResponse(doc);
    }

    private HttpResponseMessage GetResponse(XDocument doc)
    {
        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        response.Content = new StringContent(doc.ToString(), Encoding.UTF8, "application/xml");
        return response;
    }

    private HttpResponseMessage BuildDummyResponse()
    {
        XDocument doc = new(
            new XElement("response",
                new XElement("Msg", "It is work!")
            )
        );
        return GetResponse(doc);
    }
}