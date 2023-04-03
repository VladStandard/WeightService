// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Utils;

internal static class WsWebSqlUtils
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
        if (parameters.Count > 0)
        {
            foreach (SqlParameter parameter in parameters)
            {
                sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
            }
        }
        T response = sqlQuery.UniqueResult<T>();
        transaction.Commit();
        return response;
    }

    public static T GetResponse<T>(ISessionFactory sessionFactory, string query, SqlParameter? parameter)
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        ISQLQuery sqlQuery = session.CreateSQLQuery(query);
        sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
        if (parameter is not null)
        {
            sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
        }
        T response = sqlQuery.UniqueResult<T>();
        transaction.Commit();
        return response;
    }

    public static List<T> GetResponseList<T>(ISessionFactory sessionFactory, string query, List<SqlParameter> parameters)
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        ISQLQuery sqlQuery = session.CreateSQLQuery(query);
        sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
        if (parameters.Count > 0)
        {
            foreach (SqlParameter parameter in parameters)
            {
                sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
            }
        }
        IList<T> response = sqlQuery.List<T>();
        transaction.Commit();
        return response.Where(item => item is not null).ToList();
    }

    public static List<T> GetResponseList<T>(ISessionFactory sessionFactory, string query, SqlParameter? parameter)
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        ISQLQuery sqlQuery = session.CreateSQLQuery(query);
        sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
        if (parameter is not null)
        {
            sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
        }
        IList<T> response = sqlQuery.List<T>();
        transaction.Commit();
        return response.Where(item => item is not null).ToList();
    }

    public static List<SqlParameter> GetParameters(DateTime startDate, DateTime endDate, int offset, int rowCount) => new()
    {
        new("StartDate", startDate),
        new("EndDate", endDate),
        new("Offset", offset),
        new("RowCount", rowCount),
    };

    public static List<SqlParameter> GetParameters(DateTime startDate, DateTime endDate) => new()
    {
        new("StartDate", startDate),
        new("EndDate", endDate),
    };

    public static List<SqlParameter> GetParameters(int offset, int rowCount) => new()
    {
        new("Offset", offset),
        new("RowCount", rowCount),
    };

    public static List<SqlParameter> GetParametersV2(DateTime? startDate, DateTime? endDate, int? offset, int? rowCount) => new()
    {
        new("start_date", startDate == null ? DBNull.Value : startDate),
        new("end_date", endDate == null ? DBNull.Value : endDate),
        new("offset", offset == null ? DBNull.Value : offset),
        new("row_count", rowCount == null ? DBNull.Value : rowCount),
    };

    public static List<SqlParameter> GetParametersV2(DateTime? startDate) => new()
    {
        new("start_date", startDate == null ? DBNull.Value : startDate),
    };

    public static List<SqlParameter> GetParametersV2(DateTime? startDate, DateTime? endDate) => new()
    {
        new("start_date", startDate == null ? DBNull.Value : startDate),
        new("end_date", endDate == null ? DBNull.Value : endDate),
    };

    public static List<SqlParameter> GetParametersV2(string code) => new()
        {
            new("code", code == null ? DBNull.Value : code),
        };

    public static List<SqlParameter> GetParametersV2(long? id) => new()
        {
            new("id", id == null ? DBNull.Value : id),
        };
}
