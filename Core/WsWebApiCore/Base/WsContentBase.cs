// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using WsStorageCore.Helpers;

namespace WsWebApiCore.Base;

/// <summary>
/// Базовый класс контента.
/// </summary>
public class WsContentBase : ControllerBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// AppVersion helper.
    /// </summary>
    protected AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    /// <summary>
    /// NHibernate session.
    /// </summary>
    protected ISessionFactory SessionFactory { get; }

    internal WsStorageAccessManagerHelper AccessManager => WsStorageAccessManagerHelper.Instance;
    internal WsStorageContextManagerHelper ContextManager => WsStorageContextManagerHelper.Instance;
    internal SqlCrudConfigModel SqlCrudConfig => new(new List<SqlFieldFilterModel>(), 
        true, false, false, true, false);
    private static string RootDirectory => @"\\ds4tb\Dev\WebServicesLogs\";

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public WsContentBase(ISessionFactory sessionFactory)
    {
        SessionFactory = sessionFactory;
    }

    #endregion

    #region Public and private methods

    #region Public and private methods

    /// <summary>
    /// Log the request into the file.
    /// </summary>
    /// <param name="serviceLogType"></param>
    /// <param name="appName"></param>
    /// <param name="api"></param>
    /// <param name="stampDt"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private static async Task LogToFileCore(ServiceLogDirection serviceLogType, string appName, string api, DateTime stampDt, string text)
    {
        string dtString = StrUtils.FormatDtEng(stampDt, true).Replace(':', '.');
        // Get directory name.
        if (!Directory.Exists(RootDirectory)) return;
        // Machine dir.
        string directory = RootDirectory + @$"{Environment.MachineName}";
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        // App dir.
        directory = Path.Combine(directory, appName);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        // API dir.
        if (api.StartsWith("api/")) api = api.Remove(0, 4);
        directory = Path.Combine(directory, api);
        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        // Get file name.
        string filePath = serviceLogType switch
        {
            ServiceLogDirection.Request => @$"{directory}\{dtString}_request.txt",
            ServiceLogDirection.Response => @$"{directory}\{dtString}_response.txt",
            ServiceLogDirection.MetaData => @$"{directory}\{dtString}_metadata.txt",
            _ => @$"{directory}\{dtString}_default.txt"
        };

        // Store data into the log.
        if (!System.IO.File.Exists(filePath))
        {
            await System.IO.File.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }
        else
        {
            string textExists = await System.IO.File.ReadAllTextAsync(filePath);
            text = textExists + Environment.NewLine + text;
            System.IO.File.Delete(filePath);
            await System.IO.File.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }
    }

    /// <summary>
    /// Log request and response.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="requestStampDt"></param>
    /// <param name="requestData"></param>
    /// <param name="responseData"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogWebServiceFk(string appName, string url, DateTime requestStampDt, string requestData,
        string responseData, string format, string host, string version)
    {
        DateTime responseStampDt = DateTime.Now;
        // Parse counts.
        int countAll = WsContentUtils.GetAttributeValueAsInt(requestData, "Count");
        int countSuccess = WsContentUtils.GetAttributeValueAsInt(responseData, nameof(WsResponse1cShortModel.SuccessesCount));
        int countErrors = WsContentUtils.GetAttributeValueAsInt(responseData, nameof(WsResponse1cShortModel.ErrorsCount));

        // Log into DB.
        ContextManager.ContextItem.SaveLogWebService(requestStampDt, requestData, responseStampDt, responseData, LogType.Information,
            $"{host}/{url}", "", "", format, countAll, countSuccess, countErrors);

        // Add meta data.
        string metaDataRequest = $"DateTime stamp: {requestStampDt}" + Environment.NewLine;
        metaDataRequest += $"{nameof(url)}: {host}/{url}" + Environment.NewLine;
        metaDataRequest += $"{nameof(format)}: {format}" + Environment.NewLine;
        metaDataRequest += $"{nameof(version)}: {version}" + Environment.NewLine;
        metaDataRequest += $"Request data: {requestData.Length:### ### 000} B | {requestData.Length / 1024:### ###} KB" + Environment.NewLine;
        metaDataRequest += "Request body:" + Environment.NewLine;
        string metaDataResponse = $"DateTime stamp: {responseStampDt}" + Environment.NewLine;
        metaDataResponse += $"{nameof(url)}: " + Environment.NewLine;
        metaDataResponse += $"{nameof(format)}: {format}" + Environment.NewLine;
        metaDataResponse += $"{nameof(version)}: {version}" + Environment.NewLine;
        metaDataResponse += $"Response data: {responseData.Length:### ### 000} B | {responseData.Length / 1024:### ###} KB" + Environment.NewLine;
        metaDataResponse += "Response body:" + Environment.NewLine;

        // Log into FS.
        await LogToFileCore(ServiceLogDirection.Request, appName, url, requestStampDt, metaDataRequest + requestData).ConfigureAwait(false);
        await LogToFileCore(ServiceLogDirection.Response, appName, url, responseStampDt, metaDataResponse + responseData).ConfigureAwait(false);

        // Log memory into DB.
        //PluginMemory.MemorySize.Execute();
        //WsDataContext.DataAccess.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
    }

    /// <summary>
    /// Log request and response.
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="url"></param>
    /// <param name="requestStampDt"></param>
    /// <param name="requestXml"></param>
    /// <param name="responseData"></param>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public async Task LogWebServiceFk(string appName, string url, DateTime requestStampDt, XElement requestXml,
        string responseData, string format, string host, string version) =>
        await LogWebServiceFk(appName, url, requestStampDt, requestXml.ToString(), responseData,
            format, host, version).ConfigureAwait(false);

    #endregion

    #endregion

    #region Public and private methods

    internal ContentResult NewResponse1cCore<T>(Action<T> action, string format, bool isDebug, ISessionFactory sessionFactory,
        HttpStatusCode httpStatusCode = HttpStatusCode.OK) where T : SerializeBase, new()
    {
        T response = new();

        try
        {
            action(response);
            switch (typeof(T))
            {
                case var cls when cls == typeof(WsResponse1cShortModel):
                    if (response is WsResponse1cShortModel response1CShort)
                    {
                        response1CShort.IsDebug = isDebug;
                        if (response1CShort.IsDebug)
                            response1CShort.Info = WsWebResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                    }
                    break;
                case var cls when cls == typeof(WsResponse1cModel):
                    if (response is WsResponse1cModel response1c)
                    {
                        response1c.IsDebug = isDebug;
                        if (response1c.IsDebug)
                            response1c.Info = WsWebResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            switch (typeof(T))
            {
                case var cls when cls == typeof(WsResponse1cShortModel):
                    if (response is WsResponse1cShortModel response1CShort)
                        response1CShort.Errors.Add(new(ex));
                    break;
                case var cls when cls == typeof(WsResponse1cModel):
                    if (response is WsResponse1cModel response1c)
                        response1c.Errors.Add(new(ex));
                    break;
            }
        }

        return WsDataFormatUtils.GetContentResult<T>(response, format, httpStatusCode);
    }

    public ContentResult NewResponse1cFromQuery(string url, SqlParameter? sqlParameter, string format, bool isDebug,
        ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cModel>(response =>
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = url;
                //ISQLQuery sqlQuery = WsDataContext.Session.CreateSQLQuery(url);
                ISQLQuery sqlQuery = AccessManager.SessionFactory.OpenSession().CreateSQLQuery(url);
                if (sqlParameter is not null)
                {
                    if (response.ResponseQuery is not null)
                        response.ResponseQuery.Parameters.Add(new(sqlParameter));
                    sqlQuery.SetParameter(sqlParameter.ParameterName, sqlParameter.Value);
                }

                IList? list = sqlQuery.List();
                object?[] result = new object?[list.Count];
                if (list is [object[] records])
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
        }, format, isDebug, sessionFactory);

    public ContentResult NewResponseBarCodes(DateTime dtStart, DateTime dtEnd, string format, bool isDebug, ISessionFactory sessionFactory)
    {
        return NewResponse1cCore<WsResponseBarCodeListModel>(response =>
        {
            List<SqlFieldFilterModel> sqlFilters = new()
            {
                new() { Name = nameof(BarCodeModel.CreateDt), Comparer = WsSqlFieldComparer.MoreOrEqual, Value = dtStart },
                new() { Name = nameof(BarCodeModel.CreateDt), Comparer = WsSqlFieldComparer.LessOrEqual, Value = dtEnd },
            };
            SqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
            sqlCrudConfig.AddFilters(sqlFilters);
            List<BarCodeModel> barcodesDb = ContextManager.ContextList.GetListNotNullableBarCodes(sqlCrudConfig);
            response.ResponseBarCodes = WsWebResponseUtils.CastBarCodes(barcodesDb);
            response.StartDate = dtStart;
            response.EndDate = dtEnd;
            response.Count = response.ResponseBarCodes.Count;
        }, format, isDebug, sessionFactory);
    }

    /// <summary>
    /// New response 1C not found.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cIsNotFound(string message, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cModel>(response =>
        {
            response.Infos.Add(new(message));
        }, format, isDebug, sessionFactory, HttpStatusCode.NotFound);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Проверить наличие ПЛУ в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="isCheckGroup"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsPluDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, bool isCheckGroup, out PluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = AccessManager.AccessItem.GetItemNullable<PluModel>(sqlCrudConfig);
            if (!isCheckGroup)
            {
                if (itemDb is null || itemDb.IsNew)
                {
                    AddResponse1cException(response, uid1cException,
                        new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                    return false;
                }
                return true;
            }
            // isCheckGroup.
            if (itemDb is null || itemDb.IsNew || !itemDb.IsGroup)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Проверить наличие пакета в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsBundleDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BundleModel? itemDb)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
            true, false, false, false, false);
        itemDb = AccessManager.AccessItem.GetItemNullable<BundleModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponse1cException(response, uid1cException,
                new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие бренда в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsBrandDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BrandModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = AccessManager.AccessItem.GetItemNullable<BrandModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Проверить наличие клипсы в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsClipDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out ClipModel? itemDb)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
            true, false, false, false, false);
        itemDb = AccessManager.AccessItem.GetItemNullable<ClipModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponse1cException(response, uid1cException,
                new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Get box from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetBoxDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BoxModel? itemDb)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
            { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
            true, false, false, false, false);
        itemDb = AccessManager.AccessItem.GetItemNullable<BoxModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponse1cException(response, uid1cException,
                new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Get PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetPluDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out PluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = AccessManager.AccessItem.GetItemNullable<PluModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponse1cException(response, uid1cException,
                    new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Get PLU characteristic from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetPluCharacteristicDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out PluCharacteristicModel? itemDb)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTableBase1c.Uid1c), Value = uid1c } },
            true, false, false, false, false);
        itemDb = AccessManager.AccessItem.GetItemNullable<PluCharacteristicModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponse1cException(response, uid1cException,
                new($"{refName} {LocaleCore.WebService.With} '{uid1c}' {LocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    #endregion

    #region Public and private methods

    public ContentResult GetContentResult(Func<ContentResult> action, string format,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            WsServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return WsDataFormatUtils.GetContentResult<WsServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    /// <summary>
    /// Get AcceptVersion from string value.
    /// </summary>
    /// <returns></returns>
    protected WsAcceptVersion GetAcceptVersion(string value) =>
        value.ToUpper() switch
        {
            "V2" => WsAcceptVersion.V2,
            "V3" => WsAcceptVersion.V3,
            "*/*" or _ => WsAcceptVersion.V1
        };

    internal void AddResponse1cException(WsResponse1cShortModel response, BrandModel brand)
    {
        WsResponse1cErrorModel responseRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception);
        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
            responseRecord.Message += " | " + brand.ParseResult.InnerException;
        if (response.Errors.Select(item => item.Uid).Contains(brand.Uid1c))
        {
            if (response.Errors.Find(item => Equals(item.Uid, brand.Uid1c)) is { } error)
                error.Message += $" | {responseRecord}";
        }
        else
            response.Errors.Add(responseRecord);
    }

    internal void AddResponse1cException(WsResponse1cShortModel response, Guid uid, Exception? ex) =>
        AddResponse1cException(response, uid, ex?.Message, ex?.InnerException?.Message);

    /// <summary>
    /// Add error for response.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid"></param>
    /// <param name="exceptionMessage"></param>
    /// <param name="innerExceptionMessage"></param>
    internal void AddResponse1cException(WsResponse1cShortModel response, Guid uid, string? exceptionMessage, string? innerExceptionMessage)
    {
        WsResponse1cErrorModel responseRecord = new(uid,
            !string.IsNullOrEmpty(innerExceptionMessage) ? innerExceptionMessage : exceptionMessage ?? string.Empty);
        if (response.Errors.Select(item => item.Uid).Contains(uid))
        {
            if (response.Errors.Find(item => Equals(item.Uid, uid)) is { } error)
                error.Message += $" | {responseRecord}";
        }
        else
            response.Errors.Add(responseRecord);

        RemoveResponse1cErrorFromSuccess(response, responseRecord);
    }

    /// <summary>
    /// Remove error from success for resposne.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="responseRecord"></param>
    internal static void RemoveResponse1cErrorFromSuccess(WsResponse1cShortModel response, WsResponse1cErrorModel responseRecord)
    {
        bool isFind;
        do
        {
            isFind = false;
            foreach (WsResponse1cSuccessModel success in response.Successes)
            {
                if (Equals(success.Uid, responseRecord.Uid))
                {
                    response.Successes.Remove(success);
                    isFind = true;
                    break;
                }
            }
        } while (isFind);
    }

    #endregion

    #region Public and private methods - Update item

    /// <summary>
    /// Обновить запись 1C в БД. Не использовать с UpdateItemDb.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="importUid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdateItem1cDb<T>(WsResponse1cShortModel response, Guid importUid1c, T itemXml, T? itemDb, bool isCounter) where T : WsSqlTableBase1c
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(importUid1c));
        }
        else
            AddResponse1cException(response, importUid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    internal bool SaveItemDb<T>(WsResponse1cShortModel response, T item, bool isCounter) where T : WsSqlTableBase1c
        => SaveItemDb(response, item, isCounter, item.Uid1c);

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    /// <param name="uid1c"></param>
    internal bool SaveItemDb<T>(WsResponse1cShortModel response, T item, bool isCounter, Guid uid1c) where T : WsSqlTableBase
    {
        SqlCrudResultModel dbSave = AccessManager.AccessItem.Save(item, item.Identity);
        // Add was success.
        if (dbSave.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbSave.Exception);
        return dbSave.IsOk;
    }

    /// <summary>
    /// Обновить бренд в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdateBrandDb(WsResponse1cShortModel response, Guid uid1c, BrandModel itemXml, BrandModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluFkDb(WsResponse1cShortModel response, Guid uid1c, PluFkModel itemXml, PluFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь клипсы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluClipFkDb(WsResponse1cShortModel response, Guid uid1c, PluClipFkModel itemXml, PluClipFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить номенклатурную группу в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluGroupDb(WsResponse1cShortModel response, Guid uid1c, PluGroupModel itemXml, PluGroupModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь номенклатурной группы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluGroupFkDb(WsResponse1cShortModel response, Guid uid1c, PluGroupFkModel itemXml, PluGroupFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь бренда и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluBrandFkDb(WsResponse1cShortModel response, Guid uid1c, PluBrandFkModel itemXml, PluBrandFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь пакета и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluBundleFkDb(WsResponse1cShortModel response, Guid uid1c, PluBundleFkModel itemXml, PluBundleFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь номенклатурной характиристики и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluCharacteristicFk(WsResponse1cShortModel response, Guid uid1c, PluCharacteristicsFkModel itemXml, PluCharacteristicsFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    /// <summary>
    /// Обновить связь вложенности и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdatePluNestingFk(WsResponse1cShortModel response, Guid uid1c, PluNestingFkModel itemXml, PluNestingFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = AccessManager.AccessItem.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(uid1c));
        }
        else
            AddResponse1cException(response, uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    #endregion
}