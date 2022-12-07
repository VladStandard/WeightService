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
using DataCore.Sql.Tables;
using WebApiCore.Models;
using WebApiCore.Models.WebResponses;
using System.Drawing.Drawing2D;

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

    public ContentResult NewResponse1CBrandsFromAction(ISessionFactory sessionFactory, XElement request, string formatString)
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

    //public ContentResult NewResponseBarcodeFromAction(ISessionFactory sessionFactory, DateTime start, DateTime end,  string formatString, bool isTransaction)
    //{
    //    return NewResponse1CCore<Response1CModel>(sessionFactory, (response) =>
    //    {
            
    //        List<SqlFieldFilterModel> sqlFilters = new()
    //        {
    //            new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.MoreOrEqual, start),
    //            new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.LessOrEqual, end),
    //        };

    //        SqlCrudConfigModel sqlCrudConfig = new(sqlFilters, true, false, false, true);
    //        List<BarCodeModel> barcodesDb = DataContext.GetListNotNullable<BarCodeModel>(sqlCrudConfig);

    //       // ResponseBarCodeModels barCodes = new(barcodesDb);
    //        // response.SerializeAsXmlString<BarCodeModel>(false);

    //    }, formatString, isTransaction);
    //}

    private void AddResponse1CBrand(Response1CModel response, List<BrandModel> listDb, BrandModel itemInput)
    {
        try
        {
            (bool isOk, Exception? exception) resultDbStore;
            BrandModel? itemDb = listDb.Where(x => x.IdentityValueUid.Equals(itemInput.IdentityValueUid)).FirstOrDefault();
            // Find duplicate field "GUID".
            if (itemDb is not null && itemDb.IdentityIsNotNew)
            {
                itemDb.UpdateProperties(itemInput);
                resultDbStore = DataContext.DataAccess.Update(itemDb);
                if (resultDbStore.isOk)
                    response.Successes.Add(new(itemInput.IdentityValueUid, "Update was success"));
                else
                    AddResponse1CException(itemInput.IdentityValueUid, response, resultDbStore.exception);
            }
            else
            {
                // Find the duplicate field "Code".
                itemDb = listDb.Where(x => x.Code.Equals(itemInput.Code)).FirstOrDefault();
                if (itemDb is not null && itemDb.IdentityIsNotNew)
                {
                    resultDbStore = DataContext.DataAccess.Delete(itemDb);
                    if (resultDbStore.isOk)
                        response.Successes.Add(
                            new(itemDb.IdentityValueUid, "Delete was success", $"Duplicate field Code: {itemInput.Code}"));
                    else
                        AddResponse1CException(itemDb.IdentityValueUid, response, resultDbStore.exception);
                }
                // Not find the duplicate field "Code".
                resultDbStore = DataContext.DataAccess.Save(itemInput, itemInput.Identity);
                if (resultDbStore.isOk)
                    response.Successes.Add(new(itemInput.IdentityValueUid, "Add was success"));
                else
                    AddResponse1CException(itemInput.IdentityValueUid, response, resultDbStore.exception);
            }
        }
        catch (Exception ex)
        {
            AddResponse1CException(itemInput.IdentityValueUid, response, ex);
        }
    }

    private void AddResponse1CItem<T>(Response1CModel response, List<T> listDb, T itemInput) where T : SqlTableBase, new()
    {
        try
        {
            (bool isOk, Exception? exception) resultDbStore;
            T? itemDb = listDb.Where(x => x.IdentityValueUid.Equals(itemInput.IdentityValueUid)).FirstOrDefault();
            // Find duplicate field "GUID".
            if (itemDb is not null && itemDb.IdentityIsNotNew)
            {
                itemDb.UpdateProperties(itemInput);
                resultDbStore = DataContext.DataAccess.Update(itemDb);
                if (resultDbStore.isOk)
                    response.Successes.Add(new(itemInput.IdentityValueUid, "Update was success"));
                else
                    AddResponse1CException(itemInput.IdentityValueUid, response, resultDbStore.exception);
            }
            else
            {
                // Find the duplicate field "Code".
                //itemDb = listDb.Where(x => x.Code.Equals(itemInput.Code)).FirstOrDefault();
                string itemInputCode = string.Empty;
                switch(typeof(T))
                {
                    case var cls when cls == typeof(BrandModel):
                        if (itemInput is BrandModel brandInput)
                        {
                            itemInputCode = brandInput.Code;
                            BrandModel? itemCast = listDb.Cast<BrandModel>()
                                .Where(x => x.Code.Equals(itemInputCode)).FirstOrDefault();
                            if (itemCast is T itemT)
                                itemDb = itemT;
                        }
                        break;
                    case var cls when cls == typeof(NomenclatureGroupModel):
                        if (itemInput is NomenclatureGroupModel nomenclatureGroupInput)
                        {
                            itemInputCode = nomenclatureGroupInput.Code;
                            NomenclatureGroupModel? itemCast = listDb.Cast<NomenclatureGroupModel>()
                                .Where(x => x.Code.Equals(itemInputCode)).FirstOrDefault();
                            if (itemCast is T itemT)
                                itemDb = itemT;
                        }
                        break;
                }
                if (itemDb is not null && itemDb.IdentityIsNotNew)
                {
                    resultDbStore = DataContext.DataAccess.Delete(itemDb);
                    if (resultDbStore.isOk)
                        response.Successes.Add(
                            new(itemDb.IdentityValueUid, "Delete was success", $"Duplicate field Code: {itemInputCode}"));
                    else
                        AddResponse1CException(itemDb.IdentityValueUid, response, resultDbStore.exception);
                }
                // Not find the duplicate field "Code".
                resultDbStore = DataContext.DataAccess.Save(itemInput, itemInput.Identity);
                if (resultDbStore.isOk)
                    response.Successes.Add(new(itemInput.IdentityValueUid, "Add was success"));
                else
                    AddResponse1CException(itemInput.IdentityValueUid, response, resultDbStore.exception);
            }
        }
        catch (Exception ex)
        {
            AddResponse1CException(itemInput.IdentityValueUid, response, ex);
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
                SetItemPropertyFromAttribute(node, brand, nameof(brand.IsMarked));

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

    private List<NomenclatureGroupModel> GetNomenclatureGroupList(XElement xml)
    {
        List<NomenclatureGroupModel> nomenclatureGroups = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return nomenclatureGroups;

        XmlNodeList list = xmlDocument.DocumentElement.GetElementsByTagName("NomenclatureGroup");
        foreach (XmlNode node in list)
        {
            NomenclatureGroupModel nomenclatureGroup = new();
            try
            {
                nomenclatureGroup.ParseResult.Status = ParseStatus.Success;
                // Guid.
                if (Guid.TryParse(GetAttributeValue(node, "Guid"), out Guid uid))
                {
                    nomenclatureGroup.IdentityValueUid = uid;
                }
                else
                {
                    nomenclatureGroup.ParseResult.Status = ParseStatus.Error;
                    nomenclatureGroup.ParseResult.Exception = $"Guid is Empty!";
                    //continue;
                }
                if (nomenclatureGroup.IdentityValueUid.Equals(Guid.Empty))
                {
                    nomenclatureGroup.ParseResult.Status = ParseStatus.Error;
                    nomenclatureGroup.ParseResult.Exception = $"Guid is Empty!";
                    //continue;
                }

                // Set properties.
                SetItemPropertyFromAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.IsMarked));
                SetItemPropertyFromAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.Name));
                SetItemPropertyFromAttribute(node, nomenclatureGroup, "Code");

                if (string.IsNullOrEmpty(nomenclatureGroup.ParseResult.Exception))
                    nomenclatureGroup.ParseResult.Message = $"Is success";
            }
            catch (Exception ex)
            {
                nomenclatureGroup.ParseResult.Status = ParseStatus.Error;
                nomenclatureGroup.ParseResult.Exception = ex.Message;
                if (ex.InnerException is not null)
                    nomenclatureGroup.ParseResult.InnerException = ex.InnerException.Message;
            }
            nomenclatureGroups.Add(nomenclatureGroup);
        }

        return nomenclatureGroups;
    }

    private void SetItemPropertyFromAttribute<T>(XmlNode node, T item, string property) where T : SqlTableBase, new()
    {
        switch (property)
        {
            case nameof(item.IsMarked):
                string isMarkedStr = GetAttributeValue(node, property);
                switch (isMarkedStr)
                {
                    case "0":
                    case "false":
                        item.IsMarked = false;
                        break;
                    case "1":
                    case "true":
                        item.IsMarked = true;
                        break;
                    default:
                        item.ParseResult.Status = ParseStatus.Error;
                        item.ParseResult.Exception = "IsMarked is Empty!";
                        break;
                }
                break;
            case nameof(item.Name):
                item.Name = GetAttributeValue(node, property);
                if (string.IsNullOrEmpty(item.Name))
                {
                    item.ParseResult.Status = ParseStatus.Error;
                    item.ParseResult.Exception = "Name is Empty!";
                }
                break;
            case "Code":
                switch (item)
                {
                    case BrandModel brand:
                        brand.Code = GetAttributeValue(node, property);
                        if (string.IsNullOrEmpty(brand.Code))
                        {
                            brand.ParseResult.Status = ParseStatus.Error;
                            brand.ParseResult.Exception = "Code is Empty!";
                        }
                        break;
                    case NomenclatureGroupModel nomenclatureGroup:
                        nomenclatureGroup.Code = GetAttributeValue(node, property);
                        if (string.IsNullOrEmpty(nomenclatureGroup.Code))
                        {
                            nomenclatureGroup.ParseResult.Status = ParseStatus.Error;
                            nomenclatureGroup.ParseResult.Exception = "Code is Empty!";
                        }
                        break;
                }
                break;
        }
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
            response.StartDate = dtStart;
            response.EndDate = dtEnd;
            response.Count = response.ResponseBarCodes.Count;
        }, formatString, false);
    }

    public ContentResult NewResponse1CNomenclaturesGroupsFromAction(ISessionFactory sessionFactory, XElement request, string formatString)
    {
        return NewResponse1CCore<Response1CModel>(sessionFactory, (response) =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<NomenclatureGroupModel> nomenclaturesGroupsDb = DataContext.GetListNotNullable<NomenclatureGroupModel>(sqlCrudConfig);

            List<NomenclatureGroupModel> nomenclaturesGroupsInput = GetNomenclatureGroupList(request);
            foreach (NomenclatureGroupModel nomenclatureGroupInput in nomenclaturesGroupsInput)
            {
                // string xml = brandInput.SerializeAsXmlString<BrandModel>(false);
                switch (nomenclatureGroupInput.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1CItem(response, nomenclaturesGroupsDb, nomenclatureGroupInput);
                        break;
                    case ParseStatus.Error:
                        AddResponse1CException(nomenclatureGroupInput.IdentityValueUid, response, nomenclatureGroupInput.ParseResult.Exception, nomenclatureGroupInput.ParseResult.InnerException);
                        break;
                }
            }
            response.Infos.Add(new($"Proced input {nomenclaturesGroupsInput.Count} items of {nameof(nomenclaturesGroupsInput)}"));
        }, formatString, false);
    }

    #endregion
}
