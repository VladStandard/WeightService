// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-контроллер таблицы записей.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    private WsSqlAppRepository AppRepository { get; } = new();
    
    private WsSqlDeviceRepository DeviceRepository { get; } = new();
    private WsSqlAppModel App { get; set; } = new();
    private WsSqlDeviceModel Device { get; set; } = new();

    #endregion

    #region Public and private methods
    
    private WsSqlPluModel? GetItemPluNullable(WsSqlPluScaleModel pluScale)
    {
        if (!pluScale.IsNotNew || !pluScale.Plu.IsNotNew) return null;

        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueUid), pluScale.Plu.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetItemPluNotNullable(WsSqlPluScaleModel pluScale) =>
        GetItemPluNullable(pluScale) ?? new();

    public WsSqlPluTemplateFkModel? GetItemPluTemplateFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluTemplateFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid,
            WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlPluTemplateFkModel>(sqlCrudConfig);
    }

    public WsSqlPluTemplateFkModel GetItemPluTemplateFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluTemplateFkNullable(plu) ?? new();

    public WsSqlPluBundleFkModel? GetItemPluBundleFkNullable(WsSqlPluModel plu, WsSqlBundleModel bundle)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        WsSqlCrudConfigModel sqlCrudConfigBundle = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Bundle)}.{nameof(WsSqlTableBase.IdentityValueUid)}", bundle.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        sqlCrudConfig.Filters.Add(sqlCrudConfigBundle.Filters.First());
        return SqlCore.GetItemNullable<WsSqlPluBundleFkModel>(sqlCrudConfig);
    }

    public WsSqlPluBundleFkModel? GetItemPluBundleFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlPluBundleFkModel>(sqlCrudConfig);
    }

    public WsSqlPluBundleFkModel GetItemPluBundleFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluBundleFkNullable(plu) ?? new();

    public WsSqlPluClipFkModel? GetItemPluClipFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluClipFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlPluClipFkModel>(sqlCrudConfig);
    }

    public WsSqlPluClipFkModel GetItemPluClipFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluClipFkNullable(plu) ?? new();

    private WsSqlTemplateModel? GetItemTemplateNullable(WsSqlPluScaleModel pluScale)
    {
        if (pluScale.IsNew || pluScale.Plu.IsNew) return null;
        WsSqlPluModel plu = GetItemPluNotNullable(pluScale);
        return GetItemPluTemplateFkNullable(plu)?.Template;
    }

    public WsSqlTemplateModel GetItemTemplateNotNullable(WsSqlPluScaleModel pluScale) =>
        GetItemTemplateNullable(pluScale) ?? new();
    
    public string GetAccessRightsDescription(WsEnumAccessRights? accessRights)
    {
        return accessRights switch
        {
            WsEnumAccessRights.Read => WsLocaleCore.Strings.AccessRightsRead,
            WsEnumAccessRights.Write => WsLocaleCore.Strings.AccessRightsWrite,
            WsEnumAccessRights.Admin => WsLocaleCore.Strings.AccessRightsAdmin,
            _ => WsLocaleCore.Strings.AccessRightsNone
        };
    }

    public string GetAccessRightsDescription(byte accessRights) =>
        GetAccessRightsDescription((WsEnumAccessRights)accessRights);

    public string GetAccessRightsDescription(ClaimsPrincipal? user)
    {
        if (user == null)
            return string.Empty;
        string right = user.Claims.Where(c => c.Type == ClaimTypes.Role).
            Select(c => c.Value).OrderByDescending(int.Parse).First();
        return GetAccessRightsDescription((WsEnumAccessRights)int.Parse(right));
    }

    public WsSqlPluGroupModel? GetItemNomenclatureGroupParentNullable(WsSqlPluGroupModel nomenclatureGroup)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(WsSqlCrudConfigModel.GetFilters(
            $"{nameof(WsSqlPluGroupFkModel.PluGroup)}.{nameof(WsSqlTableBase.IdentityValueUid)}", nomenclatureGroup.IdentityValueUid),
            WsSqlEnumIsMarked.ShowAll, false);
        WsSqlPluGroupModel? result = SqlCore.GetItemNullable<WsSqlPluGroupFkModel>(sqlCrudConfig)?.Parent;
        return result;
    }

    public WsSqlPluGroupModel GetItemNomenclatureGroupParentNotNullable(WsSqlPluGroupModel nomenclatureGroup) =>
        GetItemNomenclatureGroupParentNullable(nomenclatureGroup) ?? new();

    #endregion

    #region Public and private methods - Logs

    public void SetupLog(string deviceName, string appName)
    {
        if (Device.IsNew)
        {
            if (string.IsNullOrEmpty(deviceName))
                deviceName = MdNetUtils.GetLocalDeviceName(false);
            Device = DeviceRepository.GetItemByName(deviceName);
        }

        if (App.IsNew)
        {
            if (string.IsNullOrEmpty(appName))
                appName = nameof(WsDataCore);
            App = AppRepository.GetItemByNameOrCreate(appName);
        }
    }

    private void SaveLogCore(StringBuilder message, WsEnumLogType logType, string filePath, int lineNumber,
        string memberName) =>
        SaveLogCore(message.ToString(), logType, filePath, lineNumber, memberName);

    private void SaveLogCore(string message, WsEnumLogType logType, string filePath, int lineNumber, string memberName)
    {
        WsStrUtils.SetStringValueTrim(ref filePath, 32, true);
        WsStrUtils.SetStringValueTrim(ref memberName, 32);
        WsStrUtils.SetStringValueTrim(ref message, 1024);
        WsSqlLogTypeModel? logTypeItem = SqlCore.GetItemLogTypeNullable(logType);

        WsSqlLogModel log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Device = Device,
            App = App,
            LogType = logTypeItem,
            Version = WsAppVersionHelper.Instance.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message
        };
        SqlCore.Save(log, WsSqlEnumSessionType.IsolatedAsync);
    }

    public void SaveLogErrorWithInfo(Exception ex, string filePath, int lineNumber, string memberName)
    {
        string message = ex.Message;
        if (ex.InnerException is not null) message += " | " + ex.InnerException.Message;
        SaveLogCore(message, WsEnumLogType.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogErrorWithInfo(Exception ex, string description, string filePath, int lineNumber, string memberName)
    {
        string message = ex.Message;
        if (ex.InnerException is not null) message += " | " + ex.InnerException.Message;
        SaveLogCore($"{description} | {message}", WsEnumLogType.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogErrorWithInfo(string message, string filePath, int lineNumber, string memberName) =>
        SaveLogCore(message, WsEnumLogType.Error, filePath, lineNumber, memberName);

    public void SaveLogError(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    public void SaveLogErrorWithDescription(Exception ex, string description,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, description, filePath, lineNumber, memberName);

    public void SaveLogError(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Error, filePath, lineNumber, memberName);

    public void SaveLogStop(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Stop, filePath, lineNumber, memberName);

    /// <summary>
    /// Записать информационное сообщение в журнал событий.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    public void SaveLogInformation(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Information, filePath, lineNumber, memberName);

    /// <summary>
    /// Записать информационное сообщение в журнал событий.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    public void SaveLogInformation(StringBuilder message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Information, filePath, lineNumber, memberName);

    /// <summary>
    /// Записать информационное сообщение в журнал событий.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="description"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    public void SaveLogInformationWithDescription(string message, string description,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore($"{description} | {message}", WsEnumLogType.Information, filePath, lineNumber, memberName);

    public void SaveLogWarning(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Warning, filePath, lineNumber, memberName);

    public void SaveLogQuestion(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, WsEnumLogType.Question, filePath, lineNumber, memberName);

    #endregion

    #region Public and private methods - LogMemory

    public void SetupLog(string appName) => SetupLog("", appName);
    /// <summary>
    /// Save log memory info.
    /// </summary>
    /// <param name="sizeAppMb"></param>
    /// <param name="sizeFreeMb"></param>
    public void SaveLogMemory(short sizeAppMb, short sizeFreeMb)
    {
        if (sizeAppMb.Equals(0) || sizeFreeMb.Equals(0)) return;
        WsSqlLogMemoryModel logMemory = new()
        {
            CreateDt = DateTime.Now,
            SizeAppMb = sizeAppMb,
            SizeFreeMb = sizeFreeMb,
            App = App,
            Device = Device,
        };
        SqlCore.Save(logMemory, WsSqlEnumSessionType.IsolatedAsync);
    }

    #endregion

    #region Public and private methods - LogWeb

    public void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, WsEnumLogType logType,
        string url, string parameters, string headers, WsEnumFormatType formatType, int countAll, int countSuccess, int countErrors) =>
        SaveLogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)formatType, countAll, countSuccess, countErrors);

    public void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, WsEnumLogType logType,
        string url, string parameters, string headers, string format, int countAll, int countSuccess, int countErrors) =>
        SaveLogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)WsDataFormatUtils.GetFormatType(format), countAll, countSuccess, countErrors);

    private void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, WsEnumLogType logType,
        string url, string parameters, string headers,
        byte formatType, int countAll, int countSuccess, int countErrors)
    {
        WsSqlLogWebModel logWebRequest = new()
        {
            CreateDt = DateTime.Now,
            StampDt = requestStampDt,
            IsMarked = false,
            Version = WsAppVersionHelper.Instance.Version,
            Direction = (byte)WsEnumServiceLogDirection.Request,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = requestDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        SqlCore.Save(logWebRequest, WsSqlEnumSessionType.IsolatedAsync);

        WsSqlLogWebModel logWebResponse = new()
        {
            CreateDt = DateTime.Now,
            StampDt = responseStampDt,
            IsMarked = false,
            Version = WsAppVersionHelper.Instance.Version,
            Direction = (byte)WsEnumServiceLogDirection.Response,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = responseDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        SqlCore.Save(logWebResponse, WsSqlEnumSessionType.IsolatedAsync);

        WsSqlLogTypeModel logTypeItem = SqlCore.GetItemLogTypeNotNullable(logType);
        WsSqlLogWebFkModel logWebFk = new()
        {
            LogWebRequest = logWebRequest,
            LogWebResponse = logWebResponse,
            App = App,
            LogType = logTypeItem,
            Device = Device,
        };
        SqlCore.Save(logWebFk, WsSqlEnumSessionType.IsolatedAsync);
    }

    #endregion
    
}