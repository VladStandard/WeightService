// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using System.Runtime.CompilerServices;
using Azure;
using DataCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using NHibernate.Hql.Util;
using WebApiCore.Common;

namespace WebApiCore.Utils;

public class ControllerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static ControllerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static ControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Constructor and destructor

    public ControllerHelper() { }

    #endregion

    #region Public and private methods

    public ContentResult RunTask(Task<ContentResult>? task, FormatTypeEnum format,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            task?.Start();
            ContentResult result = task is not null ? task.GetAwaiter().GetResult() : new();
            return result;
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return serviceException.GetResult(format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    public ContentResult GetResponse1C(ISessionFactory sessionFactory, string query, 
        SqlParameter? sqlParameter, FormatTypeEnum format, bool isTransaction)
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        List<Response1CRecordModel> success = new();
        List<Response1CRecordModel> errors = new();
        ResponseQueryModel responseQuery = new();
        
        try
        {
            if (!string.IsNullOrEmpty(query))
            {
                responseQuery.Query = query;
                ISQLQuery sqlQuery = session.CreateSQLQuery(query);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                if (sqlParameter is not null)
                {
                    responseQuery.Parameters.Add(new(sqlParameter));
                    sqlQuery.SetParameter(sqlParameter.ParameterName, sqlParameter.Value);
                }
                string response = sqlQuery.UniqueResult<string>();
                success.Add(new(Guid.NewGuid(), response));
            }
            else
                success.Add(new(Guid.NewGuid(), "Empty query. Try to make some select from any table."));
            if (isTransaction)
                transaction.Commit();
        }
        catch (Exception ex)
        {
            errors.Add(new(Guid.NewGuid(), ex.Message));
            if (ex.InnerException is not null)
                errors.Add(new(Guid.NewGuid(), ex.InnerException.Message));
            if (isTransaction)
                transaction.Rollback();
        }
        return new Response1CModel(success, errors, responseQuery)
            .GetResult(format, HttpStatusCode.OK);
    }

    #endregion
}
