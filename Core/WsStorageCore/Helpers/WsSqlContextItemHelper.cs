// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей.
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

    private WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;
    private WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private WsSqlAppModel App { get; set; } = new();
    private WsSqlDeviceModel Device { get; set; } = new();

    #endregion

    #region Public and private methods

    public WsSqlAccessModel? GetItemAccessNullable(string? userName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), userName, false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlAccessModel>(sqlCrudConfig);
    }

    public WsSqlProductSeriesModel? GetItemProductSeriesNullable(WsSqlScaleModel scale)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new List<WsSqlFieldFilterModel>
            {
                new() { Name = nameof(WsSqlProductSeriesModel.IsClose), Value = false },
                new() { Name = $"{nameof(WsSqlProductSeriesModel.Scale)}.{nameof(WsSqlScaleModel.IdentityValueId)}", Value = scale.IdentityValueId }
            }, false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlProductSeriesModel>(sqlCrudConfig);
    }

    public WsSqlProductSeriesModel GetItemProductSeriesNotNullable(WsSqlScaleModel scale) =>
        GetItemProductSeriesNullable(scale) ?? AccessManager.AccessItem.GetItemNewEmpty<WsSqlProductSeriesModel>();

    private WsSqlPluModel? GetItemPluNullable(WsSqlPluScaleModel pluScale)
    {
        if (!pluScale.IsNotNew || !pluScale.Plu.IsNotNew) return null;

        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueUid), pluScale.Plu.IdentityValueUid, false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetItemPluNotNullable(WsSqlPluScaleModel pluScale) =>
        GetItemPluNullable(pluScale) ?? new();

    public WsSqlPluTemplateFkModel? GetItemPluTemplateFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluTemplateFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid,
            false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluTemplateFkModel>(sqlCrudConfig);
    }

    public WsSqlPluTemplateFkModel GetItemPluTemplateFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluTemplateFkNullable(plu) ?? new();

    public WsSqlPluBundleFkModel? GetItemPluBundleFkNullable(WsSqlPluModel plu, WsSqlBundleModel bundle)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, false, false);
        WsSqlCrudConfigModel sqlCrudConfigBundle = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Bundle)}.{nameof(WsSqlTableBase.IdentityValueUid)}", bundle.IdentityValueUid, false, false);
        sqlCrudConfig.Filters.Add(sqlCrudConfigBundle.Filters.First());
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluBundleFkModel>(sqlCrudConfig);
    }

    public WsSqlPluBundleFkModel GetItemPluBundleFkNotNullable(WsSqlPluModel plu, WsSqlBundleModel bundle) =>
        GetItemPluBundleFkNullable(plu, bundle) ?? new();

    public WsSqlPluBundleFkModel? GetItemPluBundleFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluBundleFkModel>(sqlCrudConfig);
    }

    public WsSqlPluBundleFkModel GetItemPluBundleFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluBundleFkNullable(plu) ?? new();

    public WsSqlPluClipFkModel? GetItemPluClipFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluClipFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluClipFkModel>(sqlCrudConfig);
    }

    public WsSqlPluClipFkModel GetItemPluClipFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluClipFkNullable(plu) ?? new();

    public WsSqlPluStorageMethodFkModel? GetItemPluStorageMethodFkNullable(WsSqlPluModel plu)
    {
        if (plu.IsNew) return null;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluStorageMethodFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, true, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
    }

    public WsSqlPluStorageMethodFkModel GetItemPluStorageMethodFkNotNullable(WsSqlPluModel plu) =>
        GetItemPluStorageMethodFkNullable(plu) ?? new();

    public WsSqlPluStorageMethodFkModel? GetItemPluStorageMethodFkNullable(Guid pluUid)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluStorageMethodFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", pluUid, true, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
    }

    public WsSqlPluStorageMethodFkModel GetItemPluStorageMethodFkNotNullable(Guid pluUid) =>
        GetItemPluStorageMethodFkNullable(pluUid) ?? new();

    private WsSqlTemplateModel? GetItemTemplateNullable(WsSqlPluScaleModel pluScale)
    {
        if (pluScale.IsNew || pluScale.Plu.IsNew) return null;
        WsSqlPluModel plu = GetItemPluNotNullable(pluScale);
        return GetItemPluTemplateFkNullable(plu)?.Template;
    }

    public WsSqlTemplateModel GetItemTemplateNotNullable(WsSqlPluScaleModel pluScale) =>
        GetItemTemplateNullable(pluScale) ?? new();

    private WsSqlScaleModel GetItemScaleNullable(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(WsSqlCrudConfigModel.GetFiltersIdentity(
            $"{nameof(WsSqlDeviceScaleFkModel.Device)}", device.IdentityValueUid), false, false);
        return AccessManager.AccessItem.GetItemNotNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig).Scale;
    }

    public WsSqlScaleModel GetItemScaleNotNullable(WsSqlDeviceModel device) =>
        GetItemScaleNullable(device) ?? new();

    public WsSqlDeviceScaleFkModel? GetItemDeviceScaleFkNullable(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceScaleFkModel.Device), device.IdentityValueUid), false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
    }

    public WsSqlDeviceScaleFkModel GetItemDeviceScaleFkNotNullable(WsSqlDeviceModel device) =>
        GetItemDeviceScaleFkNullable(device) ?? new();

    public WsSqlDeviceScaleFkModel? GetItemDeviceScaleFkNullable(WsSqlScaleModel scale)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
    }

    public WsSqlDeviceScaleFkModel GetItemDeviceScaleFkNotNullable(WsSqlScaleModel scale) =>
        GetItemDeviceScaleFkNullable(scale) ?? new();

    public string GetAccessRightsDescription(AccessRightsEnum? accessRights)
    {
        return accessRights switch
        {
            AccessRightsEnum.Read => LocaleCore.Strings.AccessRightsRead,
            AccessRightsEnum.Write => LocaleCore.Strings.AccessRightsWrite,
            AccessRightsEnum.Admin => LocaleCore.Strings.AccessRightsAdmin,
            _ => LocaleCore.Strings.AccessRightsNone
        };
    }

    public string GetAccessRightsDescription(byte accessRights) =>
        GetAccessRightsDescription((AccessRightsEnum)accessRights);

    public string GetAccessRightsDescription(ClaimsPrincipal? user)
    {
        if (user == null)
            return string.Empty;
        string right = user.Claims.Where(c => c.Type == ClaimTypes.Role).
            Select(c => c.Value).OrderByDescending(int.Parse).First();
        return GetAccessRightsDescription((AccessRightsEnum)int.Parse(right));
    }

    public WsSqlScaleModel GetScaleNotNullable(long id)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueId), id, false, false, false, false);
        return AccessManager.AccessItem.GetItemNotNullable<WsSqlScaleModel>(sqlCrudConfig);
    }

    public WsSqlProductionFacilityModel GetProductionFacilityNotNullable(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlProductionFacilityModel.Name), name, false, false);
        return AccessManager.AccessItem.GetItemNotNullable<WsSqlProductionFacilityModel>(sqlCrudConfig);
    }

    public WsSqlPluGroupModel? GetItemNomenclatureGroupParentNullable(WsSqlPluGroupModel nomenclatureGroup)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(WsSqlCrudConfigModel.GetFilters(
            $"{nameof(WsSqlPluGroupFkModel.PluGroup)}.{nameof(WsSqlTableBase.IdentityValueUid)}", nomenclatureGroup.IdentityValueUid),
            false, false);
        WsSqlPluGroupModel? result = AccessManager.AccessItem.GetItemNullable<WsSqlPluGroupFkModel>(sqlCrudConfig)?.Parent;
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
            Device = GetItemDeviceOrCreateNew(deviceName);
        }

        if (App.IsNew)
        {
            if (string.IsNullOrEmpty(appName))
                appName = nameof(WsDataCore);
            App = AccessManager.AccessItem.GetItemAppOrCreateNew(appName);
        }
    }

    private void SaveLogCore(string message, LogType logType, string filePath, int lineNumber, string memberName)
    {
        StrUtils.SetStringValueTrim(ref filePath, 32, true);
        StrUtils.SetStringValueTrim(ref memberName, 32);
        StrUtils.SetStringValueTrim(ref message, 1024);
        WsSqlLogTypeModel? logTypeItem = AccessCore.GetItemLogTypeNullable(logType);

        WsSqlLogModel log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Device = Device,
            App = App,
            LogType = logTypeItem,
            Version = AppVersionHelper.Instance.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message
        };
        AccessCore.SaveAsync(log).ConfigureAwait(false);
    }

    public void SaveLogErrorWithInfo(Exception ex, string filePath, int lineNumber, string memberName)
    {
        SaveLogCore(ex.Message, LogType.Error, filePath, lineNumber, memberName);
        if (ex.InnerException is not null)
            SaveLogCore(ex.InnerException.Message, LogType.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogErrorWithInfo(string message, string filePath, int lineNumber, string memberName) =>
        SaveLogCore(message, LogType.Error, filePath, lineNumber, memberName);

    public void SaveLogError(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    public void SaveLogError(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Error, filePath, lineNumber, memberName);

    public void SaveLogStop(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Stop, filePath, lineNumber, memberName);

    public void SaveLogInformation(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Information, filePath, lineNumber, memberName);

    public void SaveLogWarning(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Warning, filePath, lineNumber, memberName);

    public void SaveLogQuestion(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Question, filePath, lineNumber, memberName);

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
        WsSqlLogMemoryModel logMemory = new()
        {
            CreateDt = DateTime.Now,
            SizeAppMb = sizeAppMb,
            SizeFreeMb = sizeFreeMb,
            App = App,
            Device = Device,
        };
        AccessCore.SaveAsync(logMemory).ConfigureAwait(false);
    }

    #endregion

    #region Public and private methods - LogWeb

    public void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers, FormatType formatType, int countAll, int countSuccess, int countErrors) =>
        SaveLogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)formatType, countAll, countSuccess, countErrors);

    public void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers, string format, int countAll, int countSuccess, int countErrors) =>
        SaveLogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)WsDataFormatUtils.GetFormatType(format), countAll, countSuccess, countErrors);

    private void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers,
        byte formatType, int countAll, int countSuccess, int countErrors)
    {
        WsSqlLogWebModel logWebRequest = new()
        {
            CreateDt = DateTime.Now,
            StampDt = requestStampDt,
            IsMarked = false,
            Version = AppVersionHelper.Instance.Version,
            Direction = (byte)ServiceLogDirection.Request,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = requestDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        AccessManager.AccessItem.Save(logWebRequest);

        WsSqlLogWebModel logWebResponse = new()
        {
            CreateDt = DateTime.Now,
            StampDt = responseStampDt,
            IsMarked = false,
            Version = AppVersionHelper.Instance.Version,
            Direction = (byte)ServiceLogDirection.Response,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = responseDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        AccessManager.AccessItem.Save(logWebResponse);

        WsSqlLogTypeModel logTypeItem = AccessCore.GetItemLogTypeNotNullable(logType);
        WsSqlLogWebFkModel logWebFk = new()
        {
            LogWebRequest = logWebRequest,
            LogWebResponse = logWebResponse,
            App = App,
            LogType = logTypeItem,
            Device = Device,
        };
        AccessCore.SaveAsync(logWebFk).ConfigureAwait(false);
    }

    #endregion

    #region Public and private methods - Get item Device type

    public WsSqlDeviceTypeModel? GetItemDeviceTypeNullable(string typeName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFilters(nameof(WsSqlDeviceTypeModel.Name), typeName), false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlDeviceTypeModel>(sqlCrudConfig);
    }

    public WsSqlDeviceTypeModel GetItemDeviceTypeNotNullable(string typeName) =>
        GetItemDeviceTypeNullable(typeName) ?? new();

    public WsSqlDeviceTypeFkModel? GetItemDeviceTypeFkNullable(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceTypeFkModel.Device), device.IdentityValueUid), false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
    }

    public WsSqlDeviceTypeFkModel GetItemDeviceTypeFkNotNullable(WsSqlDeviceModel device) =>
        GetItemDeviceTypeFkNullable(device) ?? new();

    #endregion

    #region Public and private methods - Get item Device

    public WsSqlDeviceModel GetItemDeviceOrCreateNew(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        WsSqlDeviceModel device = AccessManager.AccessItem.GetItemNotNullable<WsSqlDeviceModel>(sqlCrudConfig);
        if (device.IsNew)
        {
            device = new()
            {
                Name = name,
                PrettyName = name,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now,
                LoginDt = DateTime.Now,
                LogoutDt = DateTime.Now,
                Ipv4 = MdNetUtils.GetLocalIpAddress()
            };
            AccessManager.AccessItem.Save(device);
        }
        else
        {
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;
            AccessManager.AccessItem.Update(device);
        }
        return device;
    }

    private WsSqlDeviceModel? GetItemDeviceNullable(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlDeviceModel>(sqlCrudConfig);
    }

    public WsSqlDeviceModel GetItemDeviceNotNullable(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        return AccessManager.AccessItem.GetItemNotNullable<WsSqlDeviceModel>(sqlCrudConfig);
    }

    public WsSqlDeviceModel? GetItemDeviceNullable(WsSqlScaleModel scale)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlDeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return AccessManager.AccessItem.GetItemNullable<WsSqlDeviceScaleFkModel>(sqlCrudConfig)?.Device;
    }

    public WsSqlDeviceModel GetItemDeviceNotNullable(WsSqlScaleModel scale) => GetItemDeviceNullable(scale) ?? new();

    #endregion
}