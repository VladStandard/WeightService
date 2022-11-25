// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
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

    private ContentResult NewResponse1CCore(ISessionFactory sessionFactory, Action<ISession, Response1CModel> action,
        FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        Response1CModel response = new();

        try
        {
            action(session, response);
            //response.ResponseQuery = isShowQuery ? new() : null;
            if (isTransaction)
                transaction.Commit();
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            response.Errors.Add(new Response1CRecordModel(ex));
            if (isTransaction)
                transaction.Rollback();
        }

        return response.GetResult<Response1CModel>(format, httpStatusCode);
    }

    public ContentResult NewResponse1CFromQuery(ISessionFactory sessionFactory, string query,
        SqlParameter? sqlParameter, FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        return NewResponse1CCore(sessionFactory, (session, response) =>
        {
            //response.ResponseQuery?.Query = "";
            if (!string.IsNullOrEmpty(query))
            {
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = query;
                ISQLQuery sqlQuery = session.CreateSQLQuery(query);
                sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
                if (sqlParameter is not null)
                {
                    if (response.ResponseQuery is not null)
                        response.ResponseQuery.Parameters.Add(new(sqlParameter));
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
                string str = result[^1] as string ?? string.Empty;
                response.Infos.Add(new(str));
            }
            else
                response.Infos.Add(new("Empty query. Try to make some select from any table."));
        }, format, isShowQuery, isTransaction);
    }

    public ContentResult NewResponse1CFromAction(ISessionFactory sessionFactory,
        XElement request, FormatTypeEnum format, bool isShowQuery, bool isTransaction)
    {
        return NewResponse1CCore(sessionFactory, (session, response) =>
        {
            List<BrandModel> brands = GetBrandList(request);
            foreach (BrandModel brand in brands)
            {
                string xml = brand.SerializeAsXmlString<BrandModel>(false);
                switch (brand.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        response.Successes.Add(new(brand.IdentityValueUid, brand.ParseResult.Message));
                        break;
                    case ParseStatus.Error:
                        Response1CRecordModel response1CRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception);
                        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
                            response1CRecord.InnerMessage = brand.ParseResult.InnerException;
                        response.Errors.Add(response1CRecord);
                        break;
                }
            }
            //response.Infos.Add(new($"Parse attribute {nameof(response.Count)}: {response.Count}"));
            response.Infos.Add(new($"Proced input {brands.Count} items of {nameof(brands)}"));
        }, format, isShowQuery, isTransaction);
    }

    //public ContentResult NewResponse1CFromAction(ISessionFactory sessionFactory, BrandModel brand,
    //    FormatTypeEnum format, bool isShowQuery, bool isTransaction) => 
    //    NewResponse1CFromAction(sessionFactory, new BrandListModel(new List<BrandModel>() { brand }), format, isShowQuery, isTransaction);

    //public ContentResult NewResponse1CFromAction(ISessionFactory sessionFactory, List<BrandModel> brands, 
    //    FormatTypeEnum format, bool isShowQuery, bool isTransaction) =>
    //    NewResponse1CFromAction(sessionFactory, new BrandListModel(brands), format, isShowQuery, isTransaction);

    public List<BrandModel> GetBrandList(BrandModel brand) =>
        new List<BrandModel>() { brand };

    public List<BrandModel> GetBrandList(XElement xml)
    {
        List<BrandModel> brands = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return brands;

        //// Root node.
        //try
        //{
        //    if (int.TryParse(GetAttributeValue(xmlDocument.DocumentElement, "Count"), out int count))
        //        response.Count = count;
        //}
        //catch (Exception ex)
        //{
        //    response.Errors.Add(new(ex));
        //}

        XmlNodeList list = xmlDocument.DocumentElement.GetElementsByTagName("Brand");
        foreach (XmlNode node in list)
        {
            BrandModel brand = new();
            try
            {
                brand.ParseResult.Status = ParseStatus.Success;
                // Guid.
                if (Guid.TryParse(GetAttributeValue(node, "Guid"), out Guid uid))
                {
                    brand.IdentityValueUid = uid;
                }
                else
                {
                    brand.ParseResult.Status = ParseStatus.Error;
                    brand.ParseResult.Exception = $"Guid is Empty!";
                    //continue;
                }
                if (brand.IdentityValueUid.Equals(Guid.Empty))
                {
                    brand.ParseResult.Status = ParseStatus.Error;
                    brand.ParseResult.Exception = $"Guid is Empty!";
                    //continue;
                }
                
                // IsMarked.
                string isMarkedStr = GetAttributeValue(node, nameof(brand.IsMarked));
                if (bool.TryParse(isMarkedStr, out bool isMarked))
                {
                    brand.IsMarked = isMarked;
                }
                else
                {
                    if (isMarkedStr == "1" || isMarkedStr == "0")
                    {
                        brand.IsMarked = isMarked;
                    }
                    else
                    {
                        brand.ParseResult.Status = ParseStatus.Error;
                        brand.ParseResult.Exception = $"IsMarked is Empty!";
                        //continue;
                    }
                }

                // Name.
                brand.Name = GetAttributeValue(node, nameof(brand.Name));
                if (string.IsNullOrEmpty(brand.Name))
                {
                    brand.ParseResult.Status = ParseStatus.Error;
                    brand.ParseResult.Exception = $"Name is Empty!";
                    //continue;
                }

                // Code.
                brand.Code = GetAttributeValue(node, nameof(brand.Code));
                if (string.IsNullOrEmpty(brand.Code))
                {
                    brand.ParseResult.Status = ParseStatus.Error;
                    brand.ParseResult.Exception = $"Code is Empty!";
                    //continue;
                }

                if (string.IsNullOrEmpty(brand.ParseResult.Exception))
                    brand.ParseResult.Message = $"Is success";
            }
            catch (Exception ex)
            {
                brand.ParseResult.Status = ParseStatus.Error;
                brand.ParseResult.Exception = ex.Message;
                if (ex.InnerException is not null)
                    brand.ParseResult.InnerException = ex.InnerException.Message;
            }
            brands.Add(brand);
        }

        return brands;
    }

    private string GetAttributeValue(XmlElement? xmlElement, string nameAttribute)
    {
        string result = string.Empty;
        if (xmlElement is null) return result;
        if (xmlElement.Attributes is null) return result;

        foreach (XmlAttribute? attribute in xmlElement.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(nameAttribute.ToUpper()))
                {
                    return attribute.Value;
                }
            }
        }
        return result;
    }

    private string GetAttributeValue(XmlNode? xmlNode, string nameAttribute)
    {
        string result = string.Empty;
        if (xmlNode is null) return result;
        if (xmlNode.Attributes is null) return result;

        foreach (XmlAttribute? attribute in xmlNode.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(nameAttribute.ToUpper()))
                {
                    return attribute.Value;
                }
            }
        }
        return result;
    }

    #endregion
}
