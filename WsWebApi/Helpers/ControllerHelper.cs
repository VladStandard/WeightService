// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WsWebApi.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static WsWebApi.Models.WebUtils;

namespace WsWebApi.Helpers;

/// <summary>
/// Web API Controller helper.
/// </summary>
public partial class ControllerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static ControllerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static ControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private DataContextModel DataContext { get; } = new();
    private static string RootDirectory => @"\\ds4tb\Dev\WebServicesLogs\";

    #endregion

    #region Public and private methods

    public ContentResult GetContentResult(Task<ContentResult> task, string format,
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
            return DataFormatUtils.GetContentResult<ServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    public delegate ContentResult GetContentResultDelegate();

    public ContentResult GetContentResult(GetContentResultDelegate getContentResult, string format,
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
            return DataFormatUtils.GetContentResult<ServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private ContentResult NewResponse1cCore<T>(ISessionFactory sessionFactory, Action<T> action,
        string format, bool isTransaction, HttpStatusCode httpStatusCode = HttpStatusCode.OK) where T : SerializeBase, new()
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
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
            if (response is Response1cModel response1c)
                response1c.Errors.Add(new(ex));
            if (isTransaction)
                transaction.Rollback();
        }

        return DataFormatUtils.GetContentResult<T>(response, format, httpStatusCode);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cFromQuery(ISessionFactory sessionFactory, string query,
        SqlParameter? sqlParameter, string format, bool isTransaction)
    {
        return NewResponse1cCore<Response1cModel>(sessionFactory, response =>
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
        }, format, isTransaction);
    }

    [Obsolete(@"Deprecated method")]
    public ContentResult NewResponseBarcodeFromAction(ISessionFactory sessionFactory, DateTime start, DateTime end, string format, bool isTransaction)
    {
        return NewResponse1cCore<Response1cModel>(sessionFactory, response =>
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

        }, format, isTransaction);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cItem<T>(Response1cShortModel response, IReadOnlyCollection<T> listDb, T itemXml) where T : ISqlTable
    {
        try
        {
            T? itemDb = listDb.FirstOrDefault(x => x.IsIdentityId
                ? x.IdentityValueId.Equals(itemXml.IdentityValueId) : x.IdentityValueUid.Equals(itemXml.IdentityValueUid));

            // Find by Identity -> Update.
            if (itemDb is not null && itemDb.IsNotNew)
            {
                itemDb.UpdateProperties(itemXml);
                (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.Update(itemDb);
                if (dbUpdate.IsOk)
                    response.Successes.Add(new(itemXml.IdentityValueUid));
                else
                    AddResponse1cException(response, itemXml.IdentityValueUid, dbUpdate.Exception);
                return;
            }

            // Find by Code -> Update.
            //itemDb = listDb.Where(x => x.Code.Equals(itemXml.Code)).FirstOrDefault();
            string itemInputCode;
            switch (typeof(T))
            {
                case var cls when cls == typeof(BrandModel):
                    if (itemXml is BrandModel brandInput)
                    {
                        itemInputCode = brandInput.Code;
                        BrandModel? itemCast = listDb.Cast<BrandModel>().FirstOrDefault(x => x.Code.Equals(itemInputCode));
                        if (itemCast is T itemT) itemDb = itemT;
                    }
                    break;
                case var cls when cls == typeof(NomenclatureGroupModel):
                    if (itemXml is NomenclatureGroupModel nomenclatureGroupInput)
                    {
                        itemInputCode = nomenclatureGroupInput.Code;
                        NomenclatureGroupModel? itemCast = listDb.Cast<NomenclatureGroupModel>().FirstOrDefault(x => x.Code.Equals(itemInputCode));
                        if (itemCast is T itemT) itemDb = itemT;
                    }
                    break;
            }
            if (itemDb is not null && itemDb.IsNotNew)
            {
                (bool IsOk, Exception? Exception) dbDelete = DataContext.DataAccess.Delete(itemDb);
                // Delete was success. Duplicate field Code: {itemInputCode}.
                if (!dbDelete.IsOk)
                {
                    AddResponse1cException(response, itemDb.IdentityValueUid, dbDelete.Exception);
                    return;
                }
            }

            // Not find the duplicate field "Code".
            (bool IsOk, Exception? Exception) dbSave = DataContext.DataAccess.Save(itemXml, itemXml.Identity);
            // Add was success.
            if (dbSave.IsOk)
                response.Successes.Add(new(itemXml.IdentityValueUid));
            else
                AddResponse1cException(response, itemXml.IdentityValueUid, dbSave.Exception);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cException(Response1cShortModel response, Guid uid, string? errorMessage, string? innerErrorMessage)
    {
        Response1cErrorModel responseRecord = new(uid, innerErrorMessage ?? errorMessage ?? string.Empty);
        //responseRecord.Message += " | " + innerErrorMessage;
        response.Errors.Add(responseRecord);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cException(Response1cShortModel response, BrandModel brand)
    {
        Response1cErrorModel responseRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception ?? string.Empty);
        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
            responseRecord.Message += " | " + brand.ParseResult.InnerException;
        response.Errors.Add(responseRecord);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cException(Response1cShortModel response, Guid uid, Exception? ex) =>
        AddResponse1cException(response, uid, ex?.Message, ex?.InnerException?.Message);

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
                // Set properties.
                SetItemPropertyFromXmlAttributeGuid(node, nomenclatureGroup, "Guid");
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.IsMarked));
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.Name));
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.Code));

                if (string.IsNullOrEmpty(nomenclatureGroup.ParseResult.Exception))
                    nomenclatureGroup.ParseResult.Message = "Is success";
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

    private (object? Value, ParseResultModel ParseResult) GetItemPropertyFromXmlAttribute(XmlNode node, string propertyName)
    {
        object? value = null;
        ParseResultModel parseResult = new() { Status = ParseStatus.Success };
        switch (propertyName)
        {
            case "Guid":
                if (Guid.TryParse(GetXmlAttributeValue(node, "Guid"), out Guid uid))
                {
                    value = uid;
                }
                else
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                if (value is Guid guid && guid.Equals(Guid.Empty))
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                break;
            case nameof(SqlTableBase.IsMarked):
                string isMarkedStr = GetXmlAttributeValue(node, propertyName);
                switch (isMarkedStr)
                {
                    case "0":
                    case "false":
                        value = false;
                        break;
                    case "1":
                    case "true":
                        value = true;
                        break;
                    default:
                        parseResult.Status = ParseStatus.Error;
                        parseResult.Exception = $"{propertyName} is Empty!";
                        break;
                }
                break;
            case nameof(SqlTableBase.Name):
                value = GetXmlAttributeValue(node, propertyName);
                if (value is string name && string.IsNullOrEmpty(name))
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                break;
            case "Code":
                value = GetXmlAttributeValue(node, propertyName);
                if (value is string code && string.IsNullOrEmpty(code))
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                break;
        }
        return new(value, parseResult);
    }

    private void SetItemPropertyFromXmlAttributeGuid<T>(XmlNode node, T item, string propertyName) where T : ISqlTable
    {
        (object? Value, ParseResultModel ParseResult) property = GetItemPropertyFromXmlAttribute(node, propertyName);
        if (property.Value is Guid uid)
            item.IdentityValueUid = uid;
        item.ParseResult = property.ParseResult;
    }

    private void SetItemPropertyFromXmlAttribute<T>(XmlNode node, T item, string propertyName) where T : ISqlTable
    {
        (object? Value, ParseResultModel ParseResult) property = GetItemPropertyFromXmlAttribute(node, propertyName);
        item.ParseResult = property.ParseResult;
        
        SetItemPropertyFromXmlAttributeForBase(item, propertyName, property);

        switch (item)
        {
            case BrandModel brand:
                SetItemPropertyFromXmlAttributeForBrand(propertyName, brand, property);
                break;
            case NomenclatureGroupModel nomenclatureGroup:
                SetItemPropertyFromXmlAttributeForNomenclatureGroup(propertyName, nomenclatureGroup, property);
                break;
            case PluModel plu:
                SetItemPropertyFromXmlAttributeForPlu(propertyName, plu, property);
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForPlu(string propertyName,
        PluModel plu, (object? Value, ParseResultModel ParseResult) property)
    {
        switch (propertyName)
        {
            case nameof(plu.FullName):
                if (property.Value is string fullName)
                    plu.FullName = fullName;
                break;
            case nameof(plu.Code):
                if (property.Value is string code)
                    plu.Code = code;
                break;
            case nameof(plu.ShelfLifeDays):
                if (property.Value is short shelfLifeDays)
                    plu.ShelfLifeDays = shelfLifeDays;
                break;
            case nameof(plu.IsCheckWeight):
                if (property.Value is bool isCheckWeight)
                    plu.IsCheckWeight = isCheckWeight;
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForNomenclatureGroup(string propertyName,
        NomenclatureGroupModel nomenclatureGroup, (object? Value, ParseResultModel ParseResult) property)
    {
        switch (propertyName)
        {
            case nameof(nomenclatureGroup.Code):
                if (property.Value is string code)
                    nomenclatureGroup.Code = code;
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForBrand(string propertyName, BrandModel brand,
        (object? Value, ParseResultModel ParseResult) property)
    {
        switch (propertyName)
        {
            case nameof(brand.Code):
                if (property.Value is string code)
                    brand.Code = code;
                break;
        }
    }

    private static void SetItemPropertyFromXmlAttributeForBase<T>(T item, string propertyName,
        (object? Value, ParseResultModel ParseResult) property) where T : ISqlTable
    {
        switch (propertyName)
        {
            case nameof(item.IsMarked):
                if (property.Value is bool isMarked)
                    item.IsMarked = isMarked;
                break;
            case nameof(item.Name):
                if (property.Value is string name)
                    item.Name = name;
                break;
            case nameof(item.Description):
                if (property.Value is string description)
                    item.Description = description;
                break;
        }
    }

    [Obsolete(@"Deprecated method")]
    private void SetItemPropertyFromXmlAttributeDeprecated<T>(XmlNode node, T item, string propertyName) where T : ISqlTable
    {
        switch (propertyName)
        {
            case "Guid":
                if (Guid.TryParse(GetXmlAttributeValue(node, "Guid"), out Guid uid))
                {
                    item.IdentityValueUid = uid;
                }
                else
                {
                    item.ParseResult.Status = ParseStatus.Error;
                    item.ParseResult.Exception = "Guid is Empty!";
                }
                if (item.IdentityValueUid.Equals(Guid.Empty))
                {
                    item.ParseResult.Status = ParseStatus.Error;
                    item.ParseResult.Exception = "Guid is Empty!";
                }
                break;
            case nameof(item.IsMarked):
                string isMarkedStr = GetXmlAttributeValue(node, propertyName);
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
                item.Name = GetXmlAttributeValue(node, propertyName);
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
                        brand.Code = GetXmlAttributeValue(node, propertyName);
                        if (string.IsNullOrEmpty(brand.Code))
                        {
                            brand.ParseResult.Status = ParseStatus.Error;
                            brand.ParseResult.Exception = "Code is Empty!";
                        }
                        break;
                    case NomenclatureGroupModel nomenclatureGroup:
                        nomenclatureGroup.Code = GetXmlAttributeValue(node, propertyName);
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

    private string GetXmlAttributeValue(XmlElement? xmlElement, string nameAttribute)
    {
        string result = string.Empty;
        if (xmlElement is null) return result;

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

    private string GetXmlAttributeValue(XmlNode? xmlNode, string nameAttribute)
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

    public ContentResult NewResponseBarCodes(ISessionFactory sessionFactory, DateTime dtStart, DateTime dtEnd, string format)
    {
        return NewResponse1cCore<ResponseBarCodeListModel>(sessionFactory, response =>
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
        }, format, false);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cNomenclaturesGroups(ISessionFactory sessionFactory, XElement request, string format)
    {
        return NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<NomenclatureGroupModel> nomenclaturesGroupsDb = DataContext.GetListNotNullable<NomenclatureGroupModel>(sqlCrudConfig);

            List<NomenclatureGroupModel> nomenclaturesGroupsXml = GetNomenclatureGroupList(request);
            foreach (NomenclatureGroupModel nomenclatureGroupXml in nomenclaturesGroupsXml)
            {
                // string xml = brandInput.SerializeAsXmlString<BrandModel>(false);
                switch (nomenclatureGroupXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cItem(response, nomenclaturesGroupsDb, nomenclatureGroupXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, nomenclatureGroupXml.IdentityValueUid,
                            nomenclatureGroupXml.ParseResult.Exception, nomenclatureGroupXml.ParseResult.InnerException);
                        break;
                }
            }
        }, format, false);
    }

    /// <summary>
    /// New response 1C.
    /// </summary>
    /// <param name="sessionFactory"></param>
    /// <param name="version"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cIsNotFound(ISessionFactory sessionFactory, string version, string format) =>
        NewResponse1cCore<Response1cModel>(sessionFactory, response =>
        {
            response.Infos.Add(new($"Version {version} is not found!"));
        }, format, false, HttpStatusCode.NotFound);

    /// <summary>
    /// Update a record in the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isUpdateIdentity"></param>
    /// <returns></returns>
    private bool UpdateItemDb<T>(Response1cShortModel response, T itemXml, T? itemDb, bool isUpdateIdentity) where T : ISqlTable
    {
        if (itemDb is null || !itemDb.IsNotNew) return false;
        if (isUpdateIdentity)
            itemDb.Identity = itemXml.Identity;
        itemDb.UpdateProperties(itemXml);
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
            response.Successes.Add(new(itemXml.IdentityValueUid));
        else
            AddResponse1cException(response, itemXml.IdentityValueUid, dbUpdate.Exception);
        return true;
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="itemXml"></param>
    private void SaveItemDb<T>(Response1cShortModel response, T itemXml) where T : ISqlTable
    {
        (bool IsOk, Exception? Exception) dbSave = DataContext.DataAccess.Save(itemXml, itemXml.Identity);
        // Add was success.
        if (dbSave.IsOk)
            response.Successes.Add(new(itemXml.IdentityValueUid));
        else
            AddResponse1cException(response, itemXml.IdentityValueUid, dbSave.Exception);
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="serviceLogType"></param>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private async Task LogCore(ServiceLogType serviceLogType, string appName, DateTime dtStamp, string text)
    {
        string dtString = StringUtils.FormatDtEng(dtStamp, true).Replace(':', '.');
        // Get directory name.
        if (!Directory.Exists(RootDirectory)) return;
        string directory = RootDirectory + @$"{Environment.MachineName}";
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        // Get file name.
        string filePath = @$"{directory}\{appName}_{dtString}";
        filePath += serviceLogType switch
        {
            ServiceLogType.Request => ".request",
            ServiceLogType.Response => ".response",
            ServiceLogType.MetaData => ".metadata",
            _ => ".txt"
        };

        // Store data into the log.
        if (!File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }
        else
        {
            string textExists = await File.ReadAllTextAsync(filePath);
            text = textExists + Environment.NewLine + text;
            File.Delete(filePath);
            await File.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, DateTime dtStamp, string xml, string format, string version)
    {
        await LogCore(ServiceLogType.Request, appName, dtStamp, xml).ConfigureAwait(false);
        
        string text = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        text += $"{nameof(format)}: {format}" + Environment.NewLine;
        text += $"{nameof(version)}: {version}" + Environment.NewLine;
        text += $"Request data: {nameof(xml)}.{nameof(string.Length)}: {xml.Length} B | {xml.Length / 1024} KB | {xml.Length / 1024 / 1024} MB" + Environment.NewLine;
        await LogCore(ServiceLogType.MetaData, appName, dtStamp, text).ConfigureAwait(false);
    }

    /// <summary>
    /// Log the request.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogRequest(string appName, DateTime dtStamp, XElement xml, string format, string version) => 
        await LogRequest(appName, dtStamp, xml.ToString(), format, version).ConfigureAwait(false);

    /// <summary>
    /// Log the response.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="dtStamp"></param>
    /// <param name="result"></param>
    /// <param name="format"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task LogResponse(string appName, DateTime dtStamp, ContentResult result, string format, string version)
    {
        await LogCore(ServiceLogType.Response, appName, dtStamp, result.Content).ConfigureAwait(false);

        string text = $"DateTime stamp: {DateTime.Now}" + Environment.NewLine;
        text += $"{nameof(format)}: {format}" + Environment.NewLine;
        text += $"{nameof(version)}: {version}" + Environment.NewLine;
        text += $"Response data: {nameof(result.Content)}.{nameof(string.Length)}: {result.Content.Length} B | {result.Content.Length / 1024} KB | {result.Content.Length / 1024 / 1024} MB" + Environment.NewLine;
        await LogCore(ServiceLogType.MetaData, appName, dtStamp, text).ConfigureAwait(false);
    }

    #endregion
}