// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    public static class TerraUtils
    {
        public static class Xml
        {
            public static XDocument GetNullOrEmpty(string response)
            {
                XDocument doc = null;
                if (string.IsNullOrEmpty(response))
                {
                    doc = new(
                        new XElement(TerraConsts.Response,
                            new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, "Result is null or empty!"))
                        ));
                }
                return doc;
            }

            public static XDocument GetError(string response)
            {
                XDocument doc = null;
                if (response.Contains("<Error "))
                {
                    SqlSimpleV1Entity error = JsonConvert.DeserializeObject<SqlSimpleV1Entity>(response);
                    doc = new(
                        new XElement(TerraConsts.Response,
                            new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, error.Description))
                        ));
                }
                return doc;
            }

            public static XDocument GetErrorUnknown() => new(
                new XElement(TerraConsts.Response,
                    new XElement(TerraConsts.Error, new XAttribute(TerraConsts.Description, "Unknown error!"))));
        }

        public static class Sql
        {
            public static T GetResponse<T>(ISessionFactory sessionFactory, string query)
            {
                using ISession session = sessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                ISQLQuery sqlQuery = session.CreateSQLQuery(query);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                T response = sqlQuery.UniqueResult<T>();
                transaction.Commit();
                return response;
            }
            
            public static T GetResponse<T>(ISessionFactory sessionFactory, string query, List<SqlParameter> parameters)
            {
                using ISession session = sessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                ISQLQuery sqlQuery = session.CreateSQLQuery(query);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                if (parameters?.Count > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
                    }
                }
                T response = sqlQuery.UniqueResult<T>();
                transaction.Commit();
                return response;
            }
            
            public static T GetResponse<T>(ISessionFactory sessionFactory, string query, SqlParameter parameter)
            {
                using ISession session = sessionFactory.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                ISQLQuery sqlQuery = session.CreateSQLQuery(query);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                if (parameter != null)
                {
                    sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
                }
                T response = sqlQuery.UniqueResult<T>();
                transaction.Commit();
                return response;
            }

            public static List<SqlParameter> GetParameters(DateTime startDate, DateTime endDate, int offset, int rowCount) => new()
            {
                new SqlParameter("StartDate", startDate),
                new SqlParameter("EndDate", endDate),
                new SqlParameter("Offset", offset),
                new SqlParameter("RowCount", rowCount),
            };

            public static List<SqlParameter> GetParameters(DateTime startDate, DateTime endDate) => new()
            {
                new SqlParameter("StartDate", startDate),
                new SqlParameter("EndDate", endDate),
            };

            public static List<SqlParameter> GetParameters(int offset, int rowCount) => new()
            {
                new SqlParameter("Offset", offset),
                new SqlParameter("RowCount", rowCount),
            };

            public static List<SqlParameter> GetParametersV2(DateTime? startDate, DateTime? endDate, int? offset, int? rowCount) => new()
            {
                new SqlParameter("start_date", startDate == null ? DBNull.Value : startDate),
                new SqlParameter("end_date", endDate == null ? DBNull.Value : endDate),
                new SqlParameter("offset", offset == null ? DBNull.Value : offset),
                new SqlParameter("row_count", rowCount == null ? DBNull.Value : rowCount),
            };

            public static List<SqlParameter> GetParametersV2(DateTime? startDate) => new()
            {
                new SqlParameter("start_date", startDate == null ? DBNull.Value : startDate),
            };

            public static List<SqlParameter> GetParametersV2(DateTime? startDate, DateTime? endDate) => new()
            {
                new SqlParameter("start_date", startDate == null ? DBNull.Value : startDate),
                new SqlParameter("end_date", endDate == null ? DBNull.Value : endDate),
            };

            public static List<SqlParameter> GetParametersV2(string code) => new()
                {
                    new SqlParameter("code", code == null ? DBNull.Value : code),
                };
            
            public static List<SqlParameter> GetParametersV2(long? id) => new()
                {
                    new SqlParameter("id", id == null ? DBNull.Value : id),
                };
        }
    }
}
