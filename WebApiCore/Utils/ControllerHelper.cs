// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Net;
using System.Runtime.CompilerServices;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using FluentNHibernate.Testing.Values;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using WebApiCore.Models;
using WebApiCore.Models.WebResponses;

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
            return task is not null ? task.GetAwaiter().GetResult() : new();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return serviceException.GetResult<ServiceExceptionModel>(format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    private ContentResult NewResponse1CCore(ISessionFactory sessionFactory, 
        Action<ISession, ResponseQueryModel?, List<Response1CRecordModel>> action, 
        FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        List<Response1CRecordModel> errors = new();
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        ResponseQueryModel? responseQuery = isShowQuery ? new() : null;
        List<Response1CRecordModel> success = new();

        try
        {
            action(session, responseQuery, success);
            if (isTransaction)
                transaction.Commit();
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            errors.Add(new(Guid.NewGuid(), ex.Message));
            if (ex.InnerException is not null)
                errors.Add(new(Guid.NewGuid(), ex.InnerException.Message));
            if (isTransaction)
                transaction.Rollback();
        }
        
        return new Response1CModel(success, errors, responseQuery).GetResult<Response1CModel>(format, httpStatusCode);
    }

    public ContentResult NewResponse1CFromQuery(ISessionFactory sessionFactory, string query, 
        SqlParameter? sqlParameter, FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        return NewResponse1CCore(sessionFactory, (session, responseQuery, success) => {
            if (!string.IsNullOrEmpty(query))
            {
                if (responseQuery is not null)
                    responseQuery.Query = query;
                ISQLQuery sqlQuery = session.CreateSQLQuery(query);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                if (sqlParameter is not null)
                {
                    if (responseQuery is not null)
                        responseQuery.Parameters.Add(new(sqlParameter));
                    sqlQuery.SetParameter(sqlParameter.ParameterName, sqlParameter.Value);
                }

                IList? list = sqlQuery.List();
                object?[] result = new object?[list.Count];
                if (list.Count == 1 && list[0] is object[] records)
                {
                    result = records;
                }
                else
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (list[i] is object[] records2)
                            result[i] = records2;
                        else
                            result[i] = list[i];
                    }
                }
                string response = result[^1] as string ?? string.Empty;
                success.Add(new(Guid.NewGuid(), response));
            }
            else
                success.Add(new(Guid.NewGuid(), "Empty query. Try to make some select from any table."));
        }, format, isShowQuery, isTransaction);
    }

    public ContentResult NewResponse1CFromAction(ISessionFactory sessionFactory, BrandModel brand, 
        FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        return NewResponse1CCore(sessionFactory, (session, responseQuery, success) => {
            string response = brand.SerializeAsXmlString<BrandModel>(false);
            success.Add(new(Guid.NewGuid(), response));
        }, format, isShowQuery, isTransaction);
    }

    public ContentResult NewResponse1CFromAction(ISessionFactory sessionFactory, BrandListModel brands, 
        FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        return NewResponse1CCore(sessionFactory, (session, responseQuery, success) => {
            string response = string.Empty;
            response = $"{nameof(brands.Brands)}.{nameof(brands.Brands.Count)}: {brands.Brands.Count}";
            foreach (BrandModel brand in brands.Brands)
            {
                if (string.IsNullOrEmpty(response))
                    response = brand.SerializeAsXmlString<BrandModel>(false);
                else
                    response += Environment.NewLine + brand.SerializeAsXmlString<BrandModel>(false);
            }
            success.Add(new(Guid.NewGuid(), response));
        }, format, isShowQuery, isTransaction);
    }

    #endregion
}
