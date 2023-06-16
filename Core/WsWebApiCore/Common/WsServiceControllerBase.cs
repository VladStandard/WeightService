// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Common;

/// <summary>
/// Базовый класс веб-контроллера.
/// </summary>
public class WsServiceControllerBase : ControllerBase
{
    #region Public and private fields, properties, constructor

    protected WsAppVersionHelper AppVersion { get; } = WsAppVersionHelper.Instance;
    protected ISessionFactory SessionFactory { get; }
    internal WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    internal WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlCrudConfigModel SqlCrudConfig => new(new List<WsSqlFieldFilterModel>(),
        WsSqlEnumIsMarked.ShowAll, false, false, true, false);
    private static string RootDirectory => @"\\ds4tb\Dev\WebServicesLogs\";
    public WsServiceControllerBase(ISessionFactory sessionFactory) => SessionFactory = sessionFactory;

    #endregion

    #region Public and private methods

    /// <summary>
    /// Логирование в файл.
    /// </summary>
    /// <param name="serviceLogType"></param>
    /// <param name="appName"></param>
    /// <param name="api"></param>
    /// <param name="stampDt"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private static void LogToFileCore(WsEnumServiceLogDirection serviceLogType, string appName, string api, DateTime stampDt, string text)
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
            WsEnumServiceLogDirection.Request => @$"{directory}\{dtString}_request.txt",
            WsEnumServiceLogDirection.Response => @$"{directory}\{dtString}_response.txt",
            WsEnumServiceLogDirection.MetaData => @$"{directory}\{dtString}_metadata.txt",
            _ => @$"{directory}\{dtString}_default.txt"
        };

        // Store data into the log.
        if (!System.IO.File.Exists(filePath))
        {
            System.IO.File.WriteAllText(filePath, text, Encoding.UTF8);
        }
        else
        {
            string textExists = System.IO.File.ReadAllText(filePath);
            text = textExists + Environment.NewLine + text;
            System.IO.File.Delete(filePath);
            System.IO.File.WriteAllText(filePath, text, Encoding.UTF8);
        }
    }

    /// <summary>
    /// Логирование запроса и ответа.
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
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        DateTime responseStampDt = DateTime.Now;
        // Parse counts.
        int countAll = WsServiceContentUtils.GetAttributeValueAsInt(requestData, "Count");
        int countSuccess = WsServiceContentUtils.GetAttributeValueAsInt(responseData, nameof(WsResponse1CShortModel.SuccessesCount));
        int countErrors = WsServiceContentUtils.GetAttributeValueAsInt(responseData, nameof(WsResponse1CShortModel.ErrorsCount));

        // Log into DB.
        ContextManager.ContextItem.SaveLogWebService(requestStampDt, requestData, responseStampDt, responseData, WsEnumLogType.Information,
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

        // Логирование в файл.
        LogToFileCore(WsEnumServiceLogDirection.Request, appName, url, requestStampDt, metaDataRequest + requestData);
        LogToFileCore(WsEnumServiceLogDirection.Response, appName, url, responseStampDt, metaDataResponse + responseData);

        // Log memory into DB.
        //PluginMemory.MemorySize.Execute();
        //WsDataContext.DataAccess.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
    }

    /// <summary>
    /// Логирование запроса и ответа.
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
    public Task LogWebServiceFk(string appName, string url, DateTime requestStampDt, XElement requestXml,
        string responseData, string format, string host, string version) =>
        LogWebServiceFk(appName, url, requestStampDt, requestXml.ToString(), responseData,
            format, host, version);

    #endregion

    #region Public and private methods

    internal ContentResult NewResponse1CCore<T>(Action<T> action, string format, bool isDebug, ISessionFactory sessionFactory,
        HttpStatusCode httpStatusCode = HttpStatusCode.OK) where T : SerializeBase, new()
    {
        T response = new();

        try
        {
            action(response);
            switch (typeof(T))
            {
                case var cls when cls == typeof(WsResponse1CShortModel):
                    if (response is WsResponse1CShortModel response1CShort)
                    {
                        response1CShort.IsDebug = isDebug;
                        if (response1CShort.IsDebug)
                            response1CShort.Info = WsServiceResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                        else
                        {
                            response1CShort.SuccessesPlus?.Clear();
                            response1CShort.SuccessesPlus = null;
                        }
                    }
                    break;
                case var cls when cls == typeof(WsResponse1CModel):
                    if (response is WsResponse1CModel response1C)
                    {
                        response1C.IsDebug = isDebug;
                        if (response1C.IsDebug)
                            response1C.Info = WsServiceResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            switch (typeof(T))
            {
                case var cls when cls == typeof(WsResponse1CShortModel):
                    if (response is WsResponse1CShortModel response1CShort)
                        response1CShort.Errors.Add(new(ex));
                    break;
                case var cls when cls == typeof(WsResponse1CModel):
                    if (response is WsResponse1CModel response1C)
                        response1C.Errors.Add(new(ex));
                    break;
            }
        }

        return WsDataFormatUtils.GetContentResult<T>(response, format, httpStatusCode);
    }

    public ContentResult NewResponse1CFromQuery(string url, SqlParameter? sqlParameter, string format, bool isDebug,
        ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CModel>(response =>
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (SqlCore.SessionFactory is null)
                    throw new ArgumentException(nameof(SqlCore.SessionFactory));
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = url;
                //ISQLQuery sqlQuery = WsDataContext.Session.CreateSQLQuery(url);
                ISQLQuery sqlQuery = SqlCore.SessionFactory.OpenSession().CreateSQLQuery(url);
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
        return NewResponse1CCore<WsResponseBarCodeListModel>(response =>
        {
            List<WsSqlFieldFilterModel> sqlFilters = new()
            {
                new() { Name = nameof(WsSqlBarCodeModel.CreateDt), Comparer = WsSqlEnumFieldComparer.MoreOrEqual, Value = dtStart },
                new() { Name = nameof(WsSqlBarCodeModel.CreateDt), Comparer = WsSqlEnumFieldComparer.LessOrEqual, Value = dtEnd },
            };
            WsSqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
            sqlCrudConfig.AddFilters(sqlFilters);
            List<WsSqlBarCodeModel> barcodesDb = ContextManager.ContextList.GetListNotNullableBarCodes(sqlCrudConfig);
            response.ResponseBarCodes = WsServiceResponseUtils.CastBarCodes(barcodesDb);
            response.StartDate = dtStart;
            response.EndDate = dtEnd;
            response.Count = response.ResponseBarCodes.Count;
        }, format, isDebug, sessionFactory);
    }

    /// <summary>
    /// Новый ответ 1С - не найдено.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1CIsNotFound(string message, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CModel>(response =>
        {
            response.Infos.Add(new(message));
        }, format, isDebug, sessionFactory, HttpStatusCode.NotFound);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Проверить наличие ПЛУ в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="number"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="isCheckGroup"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsPluDb(WsResponse1CShortModel response, short number, Guid uid1CException,
        string refName, bool isCheckGroup, out WsSqlPluModel? itemDb)
    {
        itemDb = null;
        if (number > 0)
        {
            itemDb = ContextManager.ContextPlus.GetItemByNumber(number);
            if (!isCheckGroup)
            {
                if (itemDb.IsNew)
                {
                    AddResponseException(response, uid1CException,
                        new($"{refName} {WsLocaleCore.WebService.WithFieldNumber} '{number}' {WsLocaleCore.WebService.IsNotFound}!"));
                    return false;
                }
                return true;
            }
            // isCheckGroup.
            if (itemDb.IsNew || !itemDb.IsGroup)
            {
                AddResponseException(response, uid1CException,
                    new($"{refName} {WsLocaleCore.WebService.With} '{number}' {WsLocaleCore.WebService.IsNotFound}!"));
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
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsBundleDb(WsResponse1CShortModel response, Guid uid1C, Guid uid1CException,
        string refName, out WsSqlBundleModel? itemDb)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        itemDb = SqlCore.GetItemNullable<WsSqlBundleModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие бренда в БД.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsBrandDb(WsResponse1CShortModel response, Guid uid1C, Guid uid1CException,
        string refName, out WsSqlBrandModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1C, Guid.Empty))
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                    { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
                WsSqlEnumIsMarked.ShowAll, false, false, false, false);
            itemDb = SqlCore.GetItemNullable<WsSqlBrandModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponseException(response, uid1CException,
                    new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
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
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool CheckExistsClipDb(WsResponse1CShortModel response, Guid uid1C, Guid uid1CException,
        string refName, out WsSqlClipModel? itemDb)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        itemDb = SqlCore.GetItemNullable<WsSqlClipModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Get box from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetBoxDb(WsResponse1CShortModel response, Guid uid1C, Guid uid1CException,
        string refName, out WsSqlBoxModel? itemDb)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        itemDb = SqlCore.GetItemNullable<WsSqlBoxModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Get PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetPluDb(WsResponse1CShortModel response, Guid uid1C, Guid uid1CException,
        string refName, out WsSqlPluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1C, Guid.Empty))
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                    { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
                WsSqlEnumIsMarked.ShowAll, false, false, false, false);
            itemDb = SqlCore.GetItemNullable<WsSqlPluModel>(sqlCrudConfig);
            if (itemDb is null || itemDb.IsNew)
            {
                AddResponseException(response, uid1CException,
                    new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
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
    /// <param name="uid1C"></param>
    /// <param name="uid1CException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetPluCharacteristicDb(WsResponse1CShortModel response, Guid uid1C, Guid uid1CException,
        string refName, out WsSqlPluCharacteristicModel? itemDb)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        itemDb = SqlCore.GetItemNullable<WsSqlPluCharacteristicModel>(sqlCrudConfig);
        if (itemDb is null || itemDb.IsNew)
        {
            AddResponseException(response, uid1CException,
                new($"{refName} {WsLocaleCore.WebService.With} '{uid1C}' {WsLocaleCore.WebService.IsNotFound}!"));
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
    protected WsSqlEnumAcceptVersion GetAcceptVersion(string value) =>
        value.ToUpper() switch
        {
            "V2" => WsSqlEnumAcceptVersion.V2,
            "V3" => WsSqlEnumAcceptVersion.V3,
            _ => WsSqlEnumAcceptVersion.V1
        };

    internal void AddResponseException(WsResponse1CShortModel response, WsSqlBrandModel brand)
    {
        WsResponse1CErrorModel responseRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception);
        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
            responseRecord.Message += " | " + brand.ParseResult.InnerException;
        if (response.Errors.Select(item => item.Uid).Contains(brand.Uid1C))
        {
            if (response.Errors.Find(item => Equals(item.Uid, brand.Uid1C)) is { } error)
                error.Message += $" | {responseRecord}";
        }
        else
            response.Errors.Add(responseRecord);
    }

    internal void AddResponseException(WsResponse1CShortModel response, Guid uid, Exception ex) =>
        AddResponseExceptionString(response, uid, ex.Message, ex.InnerException?.Message);

    /// <summary>
    /// Add error for response.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid"></param>
    /// <param name="exceptionMessage"></param>
    /// <param name="innerExceptionMessage"></param>
    internal void AddResponseExceptionString(WsResponse1CShortModel response, Guid uid, string exceptionMessage,
        string? innerExceptionMessage = "")
    {
        WsResponse1CErrorModel responseRecord = new(uid,
            !string.IsNullOrEmpty(innerExceptionMessage) ? innerExceptionMessage : exceptionMessage);
        if (response.Errors.Select(item => item.Uid).Contains(uid))
        {
            if (response.Errors.Find(item => Equals(item.Uid, uid)) is { } error)
                error.Message += $" | {responseRecord}";
        }
        else
            response.Errors.Add(responseRecord);

        RemoveResponseErrorFromSuccess(response, responseRecord);
    }

    /// <summary>
    /// Remove error from success for response.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="responseRecord"></param>
    private static void RemoveResponseErrorFromSuccess(WsResponse1CShortModel response, WsResponse1CErrorModel responseRecord)
    {
        bool isFind;
        do
        {
            isFind = false;
            if (response.SuccessesPlus is not null)
            {
                foreach (WsResponse1CSuccessPluModel successPlu in response.SuccessesPlus)
                {
                    if (Equals(successPlu.Uid, responseRecord.Uid))
                    {
                        response.SuccessesPlus?.Remove(successPlu);
                        //isFind = true;
                        break;
                    }
                }
            }
            foreach (WsResponse1CSuccessModel success in response.Successes)
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
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    internal void UpdateItemDb<T>(WsResponse1CShortModel response, T itemXml, T? itemDb, bool isCounter, string description = "")
        where T : WsSqlTable1CBase
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter)
        {
            response.Successes.Add(new(itemXml.Uid1C));
            if (!string.IsNullOrEmpty(description) && itemXml is WsSqlPluModel pluXml)
                response.SuccessesPlus?.Add(new(itemXml.Uid1C, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
        }
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    internal void SaveItemDb<T>(WsResponse1CShortModel response, T item, bool isCounter) where T : WsSqlTable1CBase
        => SaveItemDb(response, item, isCounter, item.Uid1C);

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    /// <param name="uid1C"></param>
    internal void SaveItemDb<T>(WsResponse1CShortModel response, T item, bool isCounter, Guid uid1C) where T : WsSqlTableBase
    {
        SqlCore.Save(item, item.Identity, WsSqlEnumSessionType.Isolated);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить бренд в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdateBrandDb(WsResponse1CShortModel response, Guid uid1C, WsSqlBrandModel itemXml, WsSqlBrandModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluFkModel itemXml, WsSqlPluFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь клипсы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluClipFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluClipFkModel itemXml, WsSqlPluClipFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить номенклатурную группу в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluGroupDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluGroupModel itemXml, WsSqlPluGroupModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь номенклатурной группы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluGroupFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluGroupFkModel itemXml, WsSqlPluGroupFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь бренда и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluBrandFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluBrandFkModel itemXml, WsSqlPluBrandFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь пакета и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluBundleFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluBundleFkModel itemXml, WsSqlPluBundleFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь номенклатурной характеристики и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <param name="pluNumber"></param>
    /// <returns></returns>
    internal void UpdatePluCharacteristicFk(WsResponse1CShortModel response, Guid uid1C, WsSqlPluCharacteristicsFkModel itemXml,
        WsSqlPluCharacteristicsFkModel? itemDb, bool isCounter, short pluNumber)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter)
            response.Successes.Add(new(uid1C));
        response.SuccessesPlus?.Add(new(uid1C, $"{WsWebConstants.PluNumber}='{pluNumber}'"));
    }

    /// <summary>
    /// Обновить связь вложенности и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluNestingFk(WsResponse1CShortModel response, Guid uid1C, WsSqlPluNestingFkModel itemXml,
        WsSqlViewPluNestingModel? itemDb, bool isCounter)
    {
        //if (itemDb is null || itemDb.IsNew);
        //itemDb.UpdateProperties(itemXml);
        //ContextManager.ContextPluNesting.Update(itemDb);
        //    if (isCounter)
        //        response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Проверить некорректность группы и номера ПЛУ.
    /// </summary>
    /// <param name="pluXml">Номенклатура</param>
    public bool IsUnCorrectPluNumberForNonGroup(WsSqlPluModel pluXml) =>
        // Проверить корректность группы и номера ПЛУ.
        !IsCorrectPluNumberForNonGroup(pluXml);

    /// <summary>
    /// Проверить корректность группы и номера ПЛУ.
    /// </summary>
    /// <param name="pluXml">Номенклатура</param>
    public bool IsCorrectPluNumberForNonGroup(WsSqlPluModel pluXml)
    {
        if (pluXml is { IsGroup: true, Number: 0 }) return true;
        if (pluXml is { IsGroup: false, Number: > 0 }) return true;
        pluXml.ParseResult.Status = WsEnumParseStatus.Error;
        pluXml.ParseResult.Exception =
            $"{WsLocaleCore.WebService.FieldPluNumber} '{pluXml.Number}' " +
            $"{WsLocaleCore.WebService.ForDbRecord} {WsLocaleCore.WebService.With} {WsLocaleCore.WebService.FieldCode} '{pluXml.Code}'";
        return false;
    }

    /// <summary>
    /// Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
    /// </summary>
    public bool CheckExistsAllPlus1CFksDb()
    {
        // Загрузить кэш.
        ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        // Получить список ПЛУ.
        List<WsSqlPluModel> plusDb = ContextManager.ContextPlus.GetList();
        if (plusDb.Count > ContextCache.Plus1CFks.Count) return false;
        
        foreach (WsSqlPluModel plu in plusDb)
        {
            WsSqlPlu1CFkModel plu1CFk = 
                ContextCache.Plus1CFks.Find(item => item.Plu.Uid1C.Equals(plu.Uid1C)) ?? new();
            if (plu1CFk.IsNotExists)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
    /// </summary>
    public List<Exception> FillPlus1CFksDb()
    {
        List<Exception> exceptions = new();
        // Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
        if (CheckExistsAllPlus1CFksDb()) return exceptions;
        // Получить список ПЛУ.
        foreach (WsSqlPluModel plu in ContextManager.ContextPlus.GetList())
        {
            //// Проверить корректность группы и номера ПЛУ.
            //if (!IsCorrectPluNumberForNonGroup(plu, false))
                //exceptions.Add(new($"Exception at {nameof(FillPlus1CFksDb)}. Check PLU {plu}!"));
            WsSqlPlu1CFkModel plus1CFk = new() { Plu = plu, IsEnabled = false };
            Exception? exception = UpdatePlu1CFkDbCore(plus1CFk);
            if (exception is not null)
                exceptions.Add(exception);
        }
        return exceptions;
    }

    /// <summary>
    /// Обновить таблицу связей ПЛУ для обмена.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="recordXml"></param>
    internal List<WsSqlPlu1CFkModel> UpdatePlus1CFksDb<T>(WsResponse1CShortModel response, 
        WsXmlContentRecord<T> recordXml) where T : WsSqlTable1CBase, new()
    {
        List<WsSqlPlu1CFkModel> plus1CFksDb = new();
        switch (recordXml)
        {
            // ПЛУ.
            case WsXmlContentRecord<WsSqlPluModel> pluXml:
                plus1CFksDb = GetPlus1CFksByGuid1C(pluXml.Item.Uid1C);
                break;
            // Характеристика ПЛУ.
            case WsXmlContentRecord<WsSqlPluCharacteristicModel> pluCharacteristicXml:
                plus1CFksDb = GetPlus1CFksByGuid1C(pluCharacteristicXml.Item.NomenclatureGuid);
                break;
        }
        // Обновить таблицу связей ПЛУ для обмена.
        plus1CFksDb.ForEach(item => UpdatePlu1CFkDbCore(response, recordXml, item));
        return plus1CFksDb;
    }

    /// <summary>
    /// Получить список связей обмена ПЛУ 1С по GUID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    public List<WsSqlPlu1CFkModel> GetPlus1CFksByGuid1C(Guid uid1C)
    {
        List<WsSqlPlu1CFkModel> plus1CFks = new();
        ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        // Получить список ПЛУ по UID_1C.
        List<WsSqlPluModel> plusDb = ContextManager.ContextPlus.GetListByUid1C(uid1C);
        foreach (WsSqlPluModel pluDb in plusDb)
        {
            WsSqlPlu1CFkModel plu1CFk = ContextCache.Plus1CFks.Find(
                itemCache => Equals(itemCache.Plu.IdentityValueUid, pluDb.IdentityValueUid))
                ?? ContextManager.ContextPlu1CFk.GetNewItem();
            if (plu1CFk.IsExists)
                plus1CFks.Add(plu1CFk);
        }
        return plus1CFks;
    }

    public void SavePlu1CFk(WsSqlPlu1CFkModel plu1CFk)
    {
        SqlCore.Save(plu1CFk, WsSqlEnumSessionType.Isolated);
    }

    private void SavePlu1CFk<T>(WsResponse1CShortModel response, WsXmlContentRecord<T> recordXml, 
        WsSqlPlu1CFkModel plu1CFk) where T : WsSqlTable1CBase, new()
    {
        SqlCore.Save(plu1CFk, WsSqlEnumSessionType.Isolated);
    }

    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="recordXml"></param>
    /// <param name="plu1CFk"></param>
    private void UpdatePlu1CFkDbCore<T>(WsResponse1CShortModel response, WsXmlContentRecord<T> recordXml, 
        WsSqlPlu1CFkModel plu1CFk) where T : WsSqlTable1CBase, new()
    {
        WsSqlPlu1CFkModel plu1CFkCache =
            ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu1CFk.Plu.IdentityValueUid)) ?? new();
        // В кэше не найдено - сохранить.
        if (plu1CFkCache.IsNotExists)
        {
            plu1CFk.UpdateProperties(recordXml.Content);
            SavePlu1CFk(response, recordXml, plu1CFk);
            // Загрузить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFkCache.UpdateProperties(plu1CFk);
            plu1CFkCache.UpdateProperties(recordXml.Content);
            WsSqlPlu1CFkValidator validator = new();
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
            {
                if (recordXml is WsXmlContentRecord<WsSqlPluModel> pluXml)
                    AddResponseExceptionString(response, pluXml.Item.Uid1C,
                        string.Join(',', validation.Errors.Select(item => item.ErrorMessage).ToList()));
                else if (recordXml is WsXmlContentRecord<WsSqlPluCharacteristicModel> pluCharacteristicXml)
                    AddResponseExceptionString(response, pluCharacteristicXml.Item.NomenclatureGuid,
                        string.Join(',', validation.Errors.Select(item => item.ErrorMessage).ToList()));
            }
            else
            {
                SqlCore.Update(plu1CFkCache);
            }
        }
    }

    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <param name="plu1CFk"></param>
    private Exception? UpdatePlu1CFkDbCore(WsSqlPlu1CFkModel plu1CFk)
    {
        WsSqlPlu1CFkModel plu1CFkCache =
            ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu1CFk.Plu.IdentityValueUid)) ?? new();
        // В кэше не найдено - сохранить.
        if (plu1CFkCache.IsNotExists)
        {
            SavePlu1CFk(plu1CFk);
            // Загрузить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFkCache.UpdateProperties(plu1CFk);
            WsSqlPlu1CFkValidator validator = new();
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
                return new($"Exception at UpdatePlu1CFkDbCore. Check PLU {plu1CFkCache}!");
            SqlCore.Update(plu1CFkCache);
        }
        return null;
    }
    
    #endregion

    #region Public and private methods - Проверка ПЛУ

    /// <summary>
    /// Проверить разрешение обмена для ПЛУ.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="plus1CFks"></param>
    internal void CheckIsEnabledPlu(WsSqlTable1CBase itemXml, List<WsSqlPlu1CFkModel> plus1CFks) => 
        plus1CFks.ForEach(item => CheckIsEnabledPluForItem(itemXml, item));

    /// <summary>
    /// Проверить номер ПЛУ в списке доступа к выгрузке.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="plu1CFkDb"></param>
    private void CheckIsEnabledPluForItem(WsSqlTable1CBase itemXml, WsSqlPlu1CFkModel plu1CFkDb)
    {
        // Пропуск групп с нулевым номером.
        if (IsUnCorrectPluNumberForNonGroup(plu1CFkDb.Plu)) return;
        // ПЛУ не найдена.
        if (plu1CFkDb.IsNotExists)
        {
            itemXml.ParseResult.Status = WsEnumParseStatus.Error;
            itemXml.ParseResult.Exception =
                $"{WsLocaleCore.WebService.FieldNomenclatureIsNotFound} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
        }
        // UID_1C не совпадает.
        if (itemXml is WsSqlPluModel pluXml)
        {
            if (!Equals(pluXml.Uid1C, plu1CFkDb.Plu.Uid1C))
            {
                itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                itemXml.ParseResult.Exception =
                    $"{WsLocaleCore.WebService.FieldNomenclatureIsErrorUid1c} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
            }
        }
        else if (itemXml is WsSqlPluCharacteristicModel pluCharacteristicXml)
        {
            if (!Equals(pluCharacteristicXml.NomenclatureGuid, plu1CFkDb.Plu.Uid1C))
            {
                itemXml.ParseResult.Status = WsEnumParseStatus.Error;
                itemXml.ParseResult.Exception =
                    $"{WsLocaleCore.WebService.FieldNomenclatureIsErrorUid1c} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
            }
        }
        // Загрузка ПЛУ выключена.
        if (!plu1CFkDb.IsEnabled)
        {
            itemXml.ParseResult.Status = WsEnumParseStatus.Error;
            itemXml.ParseResult.Exception =
                $"{WsLocaleCore.WebService.FieldNomenclatureIsDenyForLoad} '{plu1CFkDb.Plu.Number}' {WsLocaleCore.WebService.WithFieldCode} '{plu1CFkDb.Plu.Code}'";
        }
    }

    #endregion
}