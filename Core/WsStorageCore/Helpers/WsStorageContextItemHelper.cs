// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsStorageContextItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsStorageContextItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsStorageContextItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsStorageAccessCoreHelper AccessCore => WsStorageAccessCoreHelper.Instance;
    private WsStorageAccessManagerHelper AccessManager => WsStorageAccessManagerHelper.Instance;
    private WsSqlAppModel App { get; set; } = new();
    private DeviceModel Device { get; set; } = new();

    #endregion

    #region Public and private methods

    public WsSqlAccessModel? GetItemAccessNullable(string? userName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), userName, false, false);
        return AccessCore.GetItemNullable<WsSqlAccessModel>(sqlCrudConfig);
    }

    public ProductSeriesModel? GetItemProductSeriesNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new List<SqlFieldFilterModel>
            {
                new() { Name = nameof(ProductSeriesModel.IsClose), Value = false },
                new() { Name = $"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", Value = scale.IdentityValueId }
            }, false, false);
        return AccessCore.GetItemNullable<ProductSeriesModel>(sqlCrudConfig);
    }

    public ProductSeriesModel GetItemProductSeriesNotNullable(ScaleModel scale) =>
        GetItemProductSeriesNullable(scale) ?? AccessCore.GetItemNewEmpty<ProductSeriesModel>();

    private PluModel? GetItemPluNullable(PluScaleModel pluScale)
    {
        if (!pluScale.IsNotNew || !pluScale.Plu.IsNotNew) return null;

        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueUid), pluScale.Plu.IdentityValueUid, false, false);
        return AccessCore.GetItemNullable<PluModel>(sqlCrudConfig);
    }

    public PluModel GetItemPluNotNullable(PluScaleModel pluScale) =>
        GetItemPluNullable(pluScale) ?? new();

    public PluTemplateFkModel? GetItemPluTemplateFkNullable(PluModel plu)
    {
        if (plu.IsNew) return null;
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluTemplateFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid,
            false, false);
        return AccessCore.GetItemNullable<PluTemplateFkModel>(sqlCrudConfig);
    }

    public PluTemplateFkModel GetItemPluTemplateFkNotNullable(PluModel plu) =>
        GetItemPluTemplateFkNullable(plu) ?? new();

    public PluBundleFkModel? GetItemPluBundleFkNullable(PluModel plu, BundleModel bundle)
    {
        if (plu.IsNew) return null;
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, false, false);
        SqlCrudConfigModel sqlCrudConfigBundle = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluBundleFkModel.Bundle)}.{nameof(WsSqlTableBase.IdentityValueUid)}", bundle.IdentityValueUid, false, false);
        sqlCrudConfig.Filters.Add(sqlCrudConfigBundle.Filters.First());
        return AccessCore.GetItemNullable<PluBundleFkModel>(sqlCrudConfig);
    }

    public PluBundleFkModel GetItemPluBundleFkNotNullable(PluModel plu, BundleModel bundle) =>
        GetItemPluBundleFkNullable(plu, bundle) ?? new();

    private TemplateModel? GetItemTemplateNullable(PluScaleModel pluScale)
    {
        if (pluScale.IsNew || pluScale.Plu.IsNew) return null;
        PluModel plu = GetItemPluNotNullable(pluScale);
        return GetItemPluTemplateFkNullable(plu)?.Template;
    }

    public TemplateModel GetItemTemplateNotNullable(PluScaleModel pluScale) =>
        GetItemTemplateNullable(pluScale) ?? new();

    private ScaleModel GetItemScaleNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFiltersIdentity(
            $"{nameof(DeviceScaleFkModel.Device)}", device.IdentityValueUid), false, false);
        return AccessCore.GetItemNotNullable<DeviceScaleFkModel>(sqlCrudConfig).Scale;
    }

    public ScaleModel GetItemScaleNotNullable(DeviceModel device) =>
        GetItemScaleNullable(device) ?? new();

    public DeviceScaleFkModel? GetItemDeviceScaleFkNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Device), device.IdentityValueUid), false, false);
        return AccessCore.GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
    }

    public DeviceScaleFkModel GetItemDeviceScaleFkNotNullable(DeviceModel device) =>
        GetItemDeviceScaleFkNullable(device) ?? new();

    public DeviceScaleFkModel? GetItemDeviceScaleFkNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return AccessCore.GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
    }

    public DeviceScaleFkModel GetItemDeviceScaleFkNotNullable(ScaleModel scale) =>
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

    public ScaleModel GetScaleNotNullable(long id)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueId), id, false, false, false, false);
        return AccessCore.GetItemNotNullable<ScaleModel>(sqlCrudConfig);
    }

    public ProductionFacilityModel GetProductionFacilityNotNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(ProductionFacilityModel.Name), name, false, false);
        return AccessCore.GetItemNotNullable<ProductionFacilityModel>(sqlCrudConfig);
    }

    public PluGroupModel? GetItemNomenclatureGroupParentNullable(PluGroupModel nomenclatureGroup)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFilters(
            $"{nameof(PluGroupFkModel.PluGroup)}.{nameof(WsSqlTableBase.IdentityValueUid)}", nomenclatureGroup.IdentityValueUid),
            false, false);
        PluGroupModel? result = AccessCore.GetItemNullable<PluGroupFkModel>(sqlCrudConfig)?.Parent;
        return result;
    }

    public PluGroupModel GetItemNomenclatureGroupParentNotNullable(PluGroupModel nomenclatureGroup) =>
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
            App = AccessCore.GetItemAppOrCreateNew(appName);
        }
    }

    private void SaveLogCore(string message, LogType logType, string filePath, int lineNumber, string memberName)
    {
        StrUtils.SetStringValueTrim(ref filePath, 32, true);
        StrUtils.SetStringValueTrim(ref memberName, 32);
        StrUtils.SetStringValueTrim(ref message, 1024);
        LogTypeModel? logTypeItem = AccessCore.GetItemLogTypeNullable(logType);

        LogModel log = new()
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
        LogMemoryModel logMemory = new()
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
        LogWebModel logWebRequest = new()
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
        AccessCore.Save(logWebRequest);

        LogWebModel logWebResponse = new()
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
        AccessCore.Save(logWebResponse);

        LogTypeModel logTypeItem = AccessCore.GetItemLogTypeNotNullable(logType);
        LogWebFkModel logWebFk = new()
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

    public DeviceTypeModel? GetItemDeviceTypeNullable(string typeName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFilters(nameof(DeviceTypeModel.Name), typeName), false, false);
        return AccessCore.GetItemNullable<DeviceTypeModel>(sqlCrudConfig);
    }

    public DeviceTypeModel GetItemDeviceTypeNotNullable(string typeName) =>
        GetItemDeviceTypeNullable(typeName) ?? new();

    public DeviceTypeFkModel? GetItemDeviceTypeFkNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceTypeFkModel.Device), device.IdentityValueUid), false, false);
        return AccessCore.GetItemNullable<DeviceTypeFkModel>(sqlCrudConfig);
    }

    public DeviceTypeFkModel GetItemDeviceTypeFkNotNullable(DeviceModel device) =>
        GetItemDeviceTypeFkNullable(device) ?? new();

    #endregion

    #region Public and private methods - Get item Device

    public DeviceModel GetItemDeviceOrCreateNew(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        DeviceModel device = AccessCore.GetItemNotNullable<DeviceModel>(sqlCrudConfig);
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
        }
        else
        {
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;
        }
        AccessCore.SaveOrUpdate(device);
        return device;
    }

    private DeviceModel? GetItemDeviceNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        return AccessCore.GetItemNullable<DeviceModel>(sqlCrudConfig);
    }

    public DeviceModel GetItemDeviceNotNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        return AccessCore.GetItemNotNullable<DeviceModel>(sqlCrudConfig);
    }

    public DeviceModel? GetItemDeviceNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return AccessCore.GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
    }

    public DeviceModel GetItemDeviceNotNullable(ScaleModel scale) => GetItemDeviceNullable(scale) ?? new();

    #endregion
}