// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using DataCore.Serialization.Models;

namespace WsWebApi.Base;

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
    internal DataContextModel WsDataContext { get; } = new();
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
        int countAll = GetAttributeValueAsInt(requestData, "Count");
        int countSuccess = GetAttributeValueAsInt(responseData, nameof(WsResponse1cShortModel.SuccessesCount));
        int countErrors = GetAttributeValueAsInt(responseData, nameof(WsResponse1cShortModel.ErrorsCount));

        // Log into DB.
        WsDataContext.DataAccess.LogWebService(requestStampDt, requestData, responseStampDt, responseData, LogType.Information,
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

        return DataFormatUtils.GetContentResult<T>(response, format, httpStatusCode);
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
                ISQLQuery sqlQuery = WsDataContext.DataAccess.SessionFactory.OpenSession().CreateSQLQuery(url);
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
                new() { Name = nameof(BarCodeModel.CreateDt), Comparer = SqlFieldComparer.MoreOrEqual, Value = dtStart },
                new() { Name = nameof(BarCodeModel.CreateDt), Comparer = SqlFieldComparer.LessOrEqual, Value = dtEnd },
            };
            SqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
            sqlCrudConfig.AddFilters(sqlFilters);
            List<BarCodeModel> barcodesDb = WsDataContext.GetListNotNullableBarCodes(sqlCrudConfig);
            response.ResponseBarCodes = WsWebResponseUtils.CastBarCodes(barcodesDb);
            response.StartDate = dtStart;
            response.EndDate = dtEnd;
            response.Count = response.ResponseBarCodes.Count;
        }, format, isDebug, sessionFactory);
    }

    /// <summary>
    /// New response 1C.
    /// </summary>
    /// <param name="version"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cIsNotFound(string version, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cModel>(response =>
        {
            response.Infos.Add(new($"Version {version} {LocaleCore.WebService.IsNotFound}!"));
        }, format, isDebug, sessionFactory, HttpStatusCode.NotFound);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get PLU from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="isCheckGroup"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetPluDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, bool isCheckGroup, out PluModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<PluModel>(sqlCrudConfig);
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
    /// Get bundle from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetBundleDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BundleModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<BundleModel>(sqlCrudConfig);
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
    /// Get brand from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetBrandDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out BrandModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<BrandModel>(sqlCrudConfig);
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
    /// Get clip from DB.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1c"></param>
    /// <param name="uid1cException"></param>
    /// <param name="refName"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal bool GetClipDb(WsResponse1cShortModel response, Guid uid1c, Guid uid1cException,
        string refName, out ClipModel? itemDb)
    {
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<ClipModel>(sqlCrudConfig);
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
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<BoxModel>(sqlCrudConfig);
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
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<PluModel>(sqlCrudConfig);
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
        itemDb = null;
        if (!Equals(uid1c, Guid.Empty))
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                    { new() { Name = nameof(SqlTableBase1c.Uid1c), Value = uid1c } },
                true, false, false, false, false);
            itemDb = WsDataContext.DataAccess.GetItemNullable<PluCharacteristicModel>(sqlCrudConfig);
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
            return DataFormatUtils.GetContentResult<WsServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
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
    protected AcceptVersion GetAcceptVersion(string value) =>
        value.ToUpper() switch
        {
            "V2" => AcceptVersion.V2,
            "V3" => AcceptVersion.V3,
            "*/*" or _ => AcceptVersion.V1
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

    internal void SetItemPropertyFromXmlAttribute<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : ISqlTable
    {
        SetItemPropertyFromXmlAttributeForBase(xmlNode, item, xmlPropertyName);
        switch (item)
        {
            case BrandModel brand:
                SetItemPropertyFromXmlAttributeForBrand(xmlNode, brand, xmlPropertyName);
                break;
            case PluModel plu:
                SetItemPropertyFromXmlAttributeForPlu(xmlNode, plu, xmlPropertyName);
                break;
            case PluGroupModel pluGroup:
                SetItemPropertyFromXmlAttributeForPluGroup(xmlNode, pluGroup, xmlPropertyName);
                break;
            case PluCharacteristicModel pluCharacteristic:
                SetItemPropertyFromXmlAttributeForPluCharacteristic(xmlNode, pluCharacteristic, xmlPropertyName);
                break;
        }
    }

    internal void SetItemParseResultException<T>(T item, string xmlPropertyName) where T : ISqlTable
    {
        item.ParseResult.Status = ParseStatus.Error;
        item.ParseResult.Exception = string.IsNullOrEmpty(item.ParseResult.Exception)
            ? $"{xmlPropertyName} {LocaleCore.WebService.IsEmpty}!"
            : $"{item.ParseResult.Exception} | {xmlPropertyName} {LocaleCore.WebService.IsEmpty}!";

    }

    internal void SetItemPropertyFromXmlAttributeForBase<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : ISqlTable
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.IdentityValueUid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ISMARKED":
                item.IsMarked = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "NAME":
                item.Name = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "DESCRIPTION":
                item.Description = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    internal void SetItemPropertyFromXmlAttributeForBrand(XmlNode xmlNode, BrandModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    internal void SetItemPropertyFromXmlAttributeForPlu(XmlNode xmlNode, PluModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ISGROUP":
                item.IsGroup = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "FULLNAME":
                item.FullName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "MEASUREMENTTYPE":
                item.MeasurementType = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                item.IsCheckWeight = GetXmlAttributeBool(xmlNode, item, xmlPropertyName, "ШТ", "КГ");
                break;
            case "PLUNUMBER":
                item.Number = GetXmlAttributeShort(xmlNode, item, xmlPropertyName);
                break;
            case "SHELFLIFE":
                item.ShelfLifeDays = GetXmlAttributeByte(xmlNode, item, xmlPropertyName);
                break;
            case "PARENTGROUPGUID":
                item.ParentGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "GROUPGUID":
                item.GroupGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "CATEGORYGUID":
                item.CategoryGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "BRANDGUID":
                item.BrandGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "BOXTYPEGUID":
                item.BoxTypeGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "BOXTYPENAME":
                item.BoxTypeName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "BOXTYPEWEIGHT":
                item.BoxTypeWeight = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
            case "CLIPTYPEGUID":
                item.ClipTypeGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "CLIPTYPENAME":
                item.ClipTypeName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "CLIPTYPEWEIGHT":
                item.ClipTypeWeight = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
            case "PACKAGETYPEGUID":
                item.PackageTypeGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "PACKAGETYPENAME":
                item.PackageTypeName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "PACKAGETYPEWEIGHT":
                item.PackageTypeWeight = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
            case "ATTACHMENTSCOUNT":
                item.AttachmentsCount = GetXmlAttributeShort(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    internal void SetItemPropertyFromXmlAttributeForPluGroup(XmlNode xmlNode, PluGroupModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ISGROUP":
                item.IsGroup = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "GROUPGUID":
                item.ParentGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    internal void SetItemPropertyFromXmlAttributeForPluCharacteristic(XmlNode xmlNode, PluCharacteristicModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
                item.Uid1c = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "NOMENCLATUREGUID":
                item.NomenclatureGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
            case "ATTACHMENTSCOUNT":
                item.AttachmentsCount = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    internal string GetXmlAttributeString<T>(XmlNode? xmlNode, T item, string attributeName) where T : ISqlTable
    {
        if (xmlNode?.Attributes is null) return string.Empty;
        foreach (XmlAttribute? attribute in xmlNode.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(attributeName.ToUpper()))
                    return attribute.Value;
            }
        }
        SetItemParseResultException(item, attributeName);
        return string.Empty;
    }

    internal bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName,
        List<string> valuesFalse, List<string> valuesTrue) where T : ISqlTable
    {
        string value = GetXmlAttributeString(xmlNode, item, xmlPropertyName).ToUpper();
        if (Enumerable.Contains(valuesFalse, value)) return false;
        if (Enumerable.Contains(valuesTrue, value)) return true;
        return default;
    }

    internal bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        GetXmlAttributeBool(xmlNode, item, xmlPropertyName, new List<string> { "0", "FALSE" }, new() { "1", "TRUE" });

    internal bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName,
        string valueFalse, string valueTrue) where T : ISqlTable =>
        GetXmlAttributeBool(xmlNode, item, xmlPropertyName, new List<string> { valueFalse }, new() { valueTrue });

    internal Guid GetXmlAttributeGuid<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        Guid.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out Guid uid) ? uid : Guid.Empty;

    internal byte GetXmlAttributeByte<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        byte.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out byte result) ? result : default;

    internal ushort GetXmlAttributeUshort<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        ushort.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out ushort result) ? result : default;

    internal short GetXmlAttributeShort<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        short.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out short result) ? result : default;

    internal decimal GetXmlAttributeDecimal<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        decimal.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out decimal result) ? result : default;

    internal List<T> GetNodesListCore<T>(XElement xml, string nodeIdentity, Action<XmlNode, T> action) where T : ISqlTable, new()
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
                itemXml.ParseResult.Exception =
                    $"{LocaleCore.WebService.Node} `{nodeIdentity}` {LocaleCore.WebService.With} `{xmlNode.Name}` {LocaleCore.WebService.IsNotIdent}!";
            }
            itemsXml.Add(itemXml);
        }
        return itemsXml;
    }

    internal int GetAttributeValueAsInt(string xml, string nodeIdentity)
    {
        if (!string.IsNullOrEmpty(xml) && GetAttributeValue(xml, nodeIdentity) is { } value)
        {
            if (int.TryParse(value, out int iValue))
                return iValue;
        }
        return default;
    }

    internal string GetAttributeValue(string xml, string nodeIdentity)
    {
        try
        {
            XmlDocument xmlDocument = new();
            xmlDocument.LoadXml(xml);
            if (xmlDocument.DocumentElement is not null)
                foreach (XmlAttribute xmlAttribute in xmlDocument.DocumentElement.Attributes)
                {
                    if (xmlAttribute.Name.Equals(nodeIdentity, StringComparison.InvariantCultureIgnoreCase))
                    {
                        try
                        {
                            return xmlAttribute.Value;
                        }
                        catch (Exception)
                        {
                            //
                        }
                    }
                }
        }
        catch (Exception)
        {
            //
        }
        return string.Empty;
    }

    #endregion

    #region Public and private methods - Update item

    /// <summary>
    /// Update the record in the database.
    /// Don't merge with UpdateItem1cDb.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="importUid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdateItemDb<T>(WsResponse1cShortModel response, Guid importUid1c, T itemXml, T? itemDb, bool isCounter) where T : ISqlTable
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = WsDataContext.DataAccess.UpdateForce(itemDb);
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
    /// Update the record 1C in the database.
    /// Don't merge with UpdateItemDb.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="importUid1c"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal bool UpdateItem1cDb<T>(WsResponse1cShortModel response, Guid importUid1c, T itemXml, T? itemDb, bool isCounter) where T : SqlTableBase1c
    {
        if (itemDb is null || itemDb.IsNew) return false;
        itemDb.UpdateProperties(itemXml);
        SqlCrudResultModel dbUpdate = WsDataContext.DataAccess.UpdateForce(itemDb);
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
    internal bool SaveItemDb<T>(WsResponse1cShortModel response, T item, bool isCounter) where T : SqlTableBase1c
        => SaveItemDb(response, item, isCounter, item.Uid1c);

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    /// <param name="uid1c"></param>
    internal bool SaveItemDb<T>(WsResponse1cShortModel response, T item, bool isCounter, Guid uid1c) where T : SqlTableBase
    {
        SqlCrudResultModel dbSave = WsDataContext.DataAccess.Save(item, item.Identity);
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

    internal bool UpdateBrandDb(WsResponse1cShortModel response, BrandModel brandXml, BrandModel? brandDb, bool isCounter)
    {
        if (brandDb is null || brandDb.IsNew) return false;
        brandDb.UpdateProperties(brandXml);
        SqlCrudResultModel dbUpdate = WsDataContext.DataAccess.UpdateForce(brandDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(brandXml.Uid1c));
        }
        else
            AddResponse1cException(response, brandXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    internal bool UpdatePluGroupDb(WsResponse1cShortModel response, PluGroupModel pluGroupXml, PluGroupModel? pluGroupDb, bool isCounter)
    {
        if (pluGroupDb is null || pluGroupDb.IsNew) return false;
        pluGroupDb.UpdateProperties(pluGroupXml);
        SqlCrudResultModel dbUpdate = WsDataContext.DataAccess.UpdateForce(pluGroupDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(pluGroupXml.Uid1c));
        }
        else
            AddResponse1cException(response, pluGroupXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    #endregion
}