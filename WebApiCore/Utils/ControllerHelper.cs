// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.Core;
using DataCore.Sql.Fields;
using DataCore.Sql.Models;
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

    #region Public and private fields, properties, constructor

    public DataContextModel DataContext { get; } = new();

    public ControllerHelper() { }

    #endregion

    #region Public and private methods

    public ContentResult GetContentResult(Task<ContentResult> task, string formatString,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            task.Start();
            return task.GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return serviceException.GetContentResult<ServiceExceptionModel>(formatString, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    public delegate ContentResult GetContentResultDelegate();

    public ContentResult GetContentResult(GetContentResultDelegate getContentResult, string formatString,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            return getContentResult();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return serviceException.GetContentResult<ServiceExceptionModel>(formatString, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    //private ContentResult NewResponse1CCore(ISessionFactory sessionFactory, Action<Response1CModel> action,
    //        string formatString, bool isTransaction)
    private ContentResult NewResponse1CCore<T>(ISessionFactory sessionFactory, Action<T> action,
            string formatString, bool isTransaction) where T : SerializeBase, new()
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        HttpStatusCode httpStatusCode = HttpStatusCode.OK;
        T response = new();

        try
        {
            action(response);
            if (isTransaction)
                transaction.Commit();
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            if (response is Response1CModel response1C)
                response1C.Errors.Add(new(ex));
            if (isTransaction)
                transaction.Rollback();
        }

        return response.GetContentResult<T>(formatString, httpStatusCode);
    }

    public ContentResult NewResponse1CFromQuery(ISessionFactory sessionFactory, string query,
        SqlParameter? sqlParameter, string formatString, bool isTransaction)
    {
        return NewResponse1CCore<Response1CModel>(sessionFactory, (response) =>
        {
            if (!string.IsNullOrEmpty(query))
            {
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = query;
                ISQLQuery sqlQuery = DataContext.Session.CreateSQLQuery(query);
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
        }, formatString, isTransaction);
    }

    public ContentResult NewResponse1CFromAction(ISessionFactory sessionFactory, XElement request, string formatString)
    {
        return NewResponse1CCore<Response1CModel>(sessionFactory, (response) =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<BrandModel> brandsDb = DataContext.GetListNotNullable<BrandModel>(sqlCrudConfig);

            List<BrandModel> brandsInput = GetBrandList(request);
            foreach (BrandModel brandInput in brandsInput)
            {
                // string xml = brandInput.SerializeAsXmlString<BrandModel>(false);
                switch (brandInput.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1CBrand(response, brandsDb, brandInput);
                        break;
                    case ParseStatus.Error:
                        AddResponse1CException(brandInput.IdentityValueUid, response, brandInput.ParseResult.Exception, brandInput.ParseResult.InnerException);
                        break;
                }
            }
            response.Infos.Add(new($"Proced input {brandsInput.Count} items of {nameof(brandsInput)}"));
        }, formatString, false);
    }

    public ContentResult NewResponseBarcodeFromAction(ISessionFactory sessionFactory, DateTime start, DateTime end,  string formatString, bool isTransaction)
    {
        return NewResponse1CCore<Response1CModel>(sessionFactory, (response) =>
        {
            
            List<SqlFieldFilterModel> sqlFilters = new()
            {
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.MoreOrEqual, start),
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.LessOrEqual, end),
            };

            SqlCrudConfigModel sqlCrudConfig = new(sqlFilters, true, false, false, true);
            List<BarCodeModel> barcodesDb = DataContext.GetListNotNullable<BarCodeModel>(sqlCrudConfig);

           // ResponseBarCodeModels barCodes = new(barcodesDb);
            // response.SerializeAsXmlString<BarCodeModel>(false);

        }, formatString, isTransaction);
    }

    private void AddResponse1CBrand(Response1CModel response, List<BrandModel> brandsDb, BrandModel brandInput)
    {
        try
        {
            (bool isOk, Exception? exception) resultDbStore;
            BrandModel? brandDb = brandsDb.Where(x => x.IdentityValueUid.Equals(brandInput.IdentityValueUid)).FirstOrDefault();
            // Find duplicate field "GUID".
            if (brandDb is not null && brandDb.IdentityIsNotNew)
            {
                brandDb.UpdateProperties(brandInput);
                resultDbStore = DataContext.DataAccess.Update(brandDb);
                if (resultDbStore.isOk)
                    response.Successes.Add(new(brandInput.IdentityValueUid, "Update was success"));
                else
                    AddResponse1CException(brandInput.IdentityValueUid, response, resultDbStore.exception);
            }
            else
            {
                // Find the duplicate field "Code".
                brandDb = brandsDb.Where(x => x.Code.Equals(brandInput.Code)).FirstOrDefault();
                if (brandDb is not null && brandDb.IdentityIsNotNew)
                {
                    resultDbStore = DataContext.DataAccess.Delete(brandDb);
                    if (resultDbStore.isOk)
                        response.Successes.Add(
                            new(brandDb.IdentityValueUid, "Delete was success", $"Duplicate field Code: {brandInput.Code}"));
                    else
                        AddResponse1CException(brandDb.IdentityValueUid, response, resultDbStore.exception);
                }
                // Not find the duplicate field "Code".
                resultDbStore = DataContext.DataAccess.Save(brandInput, brandInput.Identity);
                if (resultDbStore.isOk)
                    response.Successes.Add(new(brandInput.IdentityValueUid, "Add was success"));
                else
                    AddResponse1CException(brandInput.IdentityValueUid, response, resultDbStore.exception);
            }
        }
        catch (Exception ex)
        {
            AddResponse1CException(brandInput.IdentityValueUid, response, ex);
        }
    }

    private void AddResponse1CException(Guid uid, Response1CModel response, string? errorMessage, string? innerErrorMessage = null)
    {
        Response1CRecordModel responseRecord = new(uid, errorMessage ?? string.Empty);
        if (!string.IsNullOrEmpty(innerErrorMessage))
            responseRecord.InnerMessage = innerErrorMessage;
        response.Errors.Add(responseRecord);
    }

    private void AddResponse1CException(Guid uid, Response1CModel response, Exception? ex) =>
        AddResponse1CException(uid, response, ex?.Message, ex?.InnerException?.Message);

    public List<BrandModel> GetBrandList(BrandModel brand) => new() { brand };

    private List<BrandModel> GetBrandList(XElement xml)
    {
        List<BrandModel> brands = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return brands;

        // Root node.
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
                switch (isMarkedStr)
                {
                    case "0":
                    case "false":
                        brand.IsMarked = false;
                        break;
                    case "1":
                    case "true":
                        brand.IsMarked = true;
                        break;
                    default:
                        brand.ParseResult.Status = ParseStatus.Error;
                        brand.ParseResult.Exception = $"IsMarked is Empty!";
                        break;
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
    
    public ContentResult NewResponseBarCodes(ISessionFactory sessionFactory, DateTime dtStart, DateTime dtEnd, string formatString)
    {
        return NewResponse1CCore<ResponseBarCodeListModel>(sessionFactory, (response) =>
        {
            List<SqlFieldFilterModel> sqlFilters = new()
            {
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.MoreOrEqual, dtStart),
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.LessOrEqual, dtEnd),
            };
            SqlCrudConfigModel sqlCrudConfig = new(sqlFilters, true, false, false, true);
            List<BarCodeModel> barcodesDb = DataContext.GetListNotNullable<BarCodeModel>(sqlCrudConfig);
            response.ResponseBarCodes = WebResponseUtils.CastBarCodes(barcodesDb);
        }, formatString, false);
    }

    #endregion
}
