// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;
using System.Text;
using WsLocalization.Models;
using WsWebApi.Enums;

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
    private void AddResponse1cException(Response1cShortModel response, Guid uid, string? exceptionMessage, string? innerExceptionMessage)
    {
        Response1cErrorModel responseRecord = new(uid, 
            !string.IsNullOrEmpty(innerExceptionMessage) ? innerExceptionMessage : exceptionMessage ?? string.Empty);
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

    private void SetItemPropertyFromXmlAttribute<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : ISqlTable
    {
        SetItemPropertyFromXmlAttributeForBase(xmlNode, item, xmlPropertyName);
        switch (item)
        {
            case BrandModel brand:
                SetItemPropertyFromXmlAttributeForBrand(xmlNode, xmlPropertyName, brand);
                break;
            case NomenclatureGroupModel nomenclatureGroup:
                SetItemPropertyFromXmlAttributeForNomenclatureGroup(xmlNode, xmlPropertyName, nomenclatureGroup);
                break;
            case PluModel plu:
                SetItemPropertyFromXmlAttributeForPlu(xmlNode, xmlPropertyName, plu);
                break;
        }
    }

    private void SetItemParseResultException<T>(T item, string xmlPropertyName) where T : ISqlTable
    {
        item.ParseResult.Status = ParseStatus.Error;
        item.ParseResult.Exception = string.IsNullOrEmpty(item.ParseResult.Exception)
            ? $"{xmlPropertyName} {LocaleCore.WebService.IsEmpty}!"
            : $"{item.ParseResult.Exception} | {xmlPropertyName} {LocaleCore.WebService.IsEmpty}!";

    }

    private void SetItemPropertyFromXmlAttributeForBase<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : ISqlTable
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
            case nameof(item.IdentityValueUid):
            case "IDENTITYVALUEUID":
                item.IdentityValueUid = Guid.Empty;
                if (Guid.TryParse(GetXmlAttributeValue(xmlNode, "Guid"), out Guid uid))
                {
                    item.IdentityValueUid = uid;
                }
                else
                    SetItemParseResultException(item, xmlPropertyName);
                break;
            case nameof(item.IsMarked):
            case "ISMARKED":
                item.IsMarked = false;
                switch (GetXmlAttributeValue(xmlNode, xmlPropertyName).ToUpper())
                {
                    case "0":
                    case "FALSE":
                        item.IsMarked = false;
                        break;
                    case "1":
                    case "TRUE":
                        item.IsMarked = true;
                        break;
                    default:
                        SetItemParseResultException(item, xmlPropertyName);
                        break;
                }
                break;
            case nameof(item.Name):
            case "NAME":
                item.Name = GetXmlAttributeValue(xmlNode, xmlPropertyName);
                if (string.IsNullOrEmpty(item.Name))
                    SetItemParseResultException(item, xmlPropertyName);
                break;
            case nameof(item.Description):
            case "DESCRIPTION":
                item.Description = GetXmlAttributeValue(xmlNode, xmlPropertyName);
                if (string.IsNullOrEmpty(item.Description))
                    SetItemParseResultException(item, xmlPropertyName);
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForBrand(XmlNode xmlNode, string xmlPropertyName, BrandModel item)
    {
        switch (xmlPropertyName)
        {
            case nameof(item.Code):
                item.Code = GetXmlAttributeValue(xmlNode, xmlPropertyName);
                if (string.IsNullOrEmpty(item.Code))
                    SetItemParseResultException(item, xmlPropertyName);
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForNomenclatureGroup(XmlNode xmlNode, string xmlPropertyName,
        NomenclatureGroupModel item)
    {
        switch (xmlPropertyName)
        {
            case nameof(item.Code):
                item.Code = GetXmlAttributeValue(xmlNode, xmlPropertyName);
                if (string.IsNullOrEmpty(item.Code))
                    SetItemParseResultException(item, xmlPropertyName);
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForPlu(XmlNode xmlNode, string xmlPropertyName, PluModel item)
    {
        switch (xmlPropertyName)
        {
            case nameof(item.FullName):
                item.FullName = GetXmlAttributeValue(xmlNode, xmlPropertyName);
                if (string.IsNullOrEmpty(item.FullName))
                    SetItemParseResultException(item, xmlPropertyName);
                break;
            case nameof(item.Code):
                item.Code = GetXmlAttributeValue(xmlNode, xmlPropertyName);
                if (string.IsNullOrEmpty(item.Code))
                    SetItemParseResultException(item, xmlPropertyName);
                break;
            case nameof(item.ShelfLifeDays):
                if (ushort.TryParse(GetXmlAttributeValue(xmlNode, "ShelfLife"), out ushort shelfLifeDays))
                    item.ShelfLifeDays = shelfLifeDays;
                else
                    SetItemParseResultException(item, "ShelfLife");
                break;
            case nameof(item.IsCheckWeight):
                string sourceValue = GetXmlAttributeValue(xmlNode, "MeasurementType");
                switch (sourceValue.ToUpper())
                {
                    case "КГ":
                        item.IsCheckWeight = true;
                        break;
                    case "ШТ":
                        item.IsCheckWeight = false;
                        break;
                    default:
                        SetItemParseResultException(item, "MeasurementType");
                        break;
                }
                break;
            case nameof(item.Number):
                if (ushort.TryParse(GetXmlAttributeValue(xmlNode, "PluNumber"), out ushort number))
                    item.Number = number;
                else
                    SetItemParseResultException(item, "PluNumber");
                break;
        }
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

    private List<T> GetNodesListCore<T>(XElement xml, string nodeIdentity, Action<XmlNode, T> action) where T : ISqlTable, new()
    {
        List<T> itemsXml = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return itemsXml;

        XmlNodeList xmlNodes = xmlDocument.DocumentElement.ChildNodes;
        if (xmlNodes.Count <= 0) return itemsXml;
        foreach (XmlNode xmlNode in xmlNodes)
        {
            T itemXml = new() { ParseResult = { Status = ParseStatus.Success, Exception = string.Empty } };
            if (xmlNode.Name.Equals(nodeIdentity, StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    action(xmlNode, itemXml);
                    // Fix ParseResult.
                    itemXml.ParseResult.Message = string.IsNullOrEmpty(itemXml.ParseResult.Exception) ? "Is success" : itemXml.ParseResult.Exception;
                }
                catch (Exception ex)
                {
                    itemXml.ParseResult.Status = ParseStatus.Error;
                    itemXml.ParseResult.Exception = ex.Message;
                    if (ex.InnerException is not null)
                        itemXml.ParseResult.InnerException = ex.InnerException.Message;
                }
            }
            else
            {
                itemXml.ParseResult.Status = ParseStatus.Error;
                itemXml.ParseResult.Exception = $"The node `{nodeIdentity}` with `{xmlNode.Name}` is not ident!";
            }
            itemsXml.Add(itemXml);
        }
        return itemsXml;
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