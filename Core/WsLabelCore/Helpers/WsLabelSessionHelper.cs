using WsStorageCore.Tables.TableRefModels.ProductionSites;
using WsStorageCore.Tables.TableRefModels.WorkShops;
namespace WsLabelCore.Helpers;

/// <summary>
/// Пользовательская сессия.
/// </summary>
#nullable enable
public sealed class WsLabelSessionHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsLabelSessionHelper _instance;
#pragma warning restore CS8618
    public static WsLabelSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties
    
    private static WsDebugHelper Debug => WsDebugHelper.Instance;
    private static DateTime ProductDateMaxValue => DateTime.Now.AddDays(+31);
    private static DateTime ProductDateMinValue => DateTime.Now.AddDays(-31);
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    
    public static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public static ushort PlusPageColumnCount => 4;
    public static ushort PlusPageSize => 16;
    public static ushort PlusPageRowCount => 4;
    public static string DeviceName => MdNetUtils.GetLocalDeviceName(false);
    public WsEnumPrintModel PrintModelMain => Line.Printer.PrinterType.Name.Contains("TSC ") ? WsEnumPrintModel.Tsc : WsEnumPrintModel.Zebra;
    public WsPluginPrintTscModel? PluginPrintTscMain { get; set; }
    public WsPluginPrintZebraModel? PluginPrintZebraMain { get; set; }
    public WsSqlPluWeighingModel PluWeighing { get; set; }
    public WsWeighingSettingsModel WeighingSettings { get; private set; }
    public WsSqlPluScaleModel PluLine { get; private set; }
    public int PlusPageNumber { get; set; }
    public WsSqlProductionSiteModel Area { get; private set; }
    public WsSqlScaleModel Line { get; private set; }
    public string PublishDescription { get; private set; } = "";
    public DateTime ProductDate { get; set; }
    public WsSqlViewPluNestingModel ViewPluNesting { get; private set; }
    public WsLocalizationManager Localization { get; set; } = new();

    public WsLabelSessionHelper()
    {
        Area = ContextManager.ProductionSiteRepository.GetNewItem();
        PluLine = ContextManager.PluLineRepository.GetNewItem();
        Line = ContextManager.LineRepository.GetNewItem();
        PluWeighing = ContextManager.PluWeighingRepository.GetNewItem();
        ViewPluNesting = ContextManager.PluNestingFkRepository.GetNewView();
        WeighingSettings = new();
    }

    #endregion

    #region Public and private methods

    public int GetPlusPageCount() =>
        ContextCache.LocalViewPlusLines.Where(item => item.IsActive).ToList().Count / PlusPageSize;

    public void RotateProductDate(WsEnumDirection direction)
    {
        switch (direction)
        {
            case WsEnumDirection.Left:
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue) ProductDate = ProductDateMinValue;
                break;
            }
            case WsEnumDirection.Right:
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue) ProductDate = ProductDateMaxValue;
                break;
            }
        }
    }

    public void NewPallet()
    {
        if (PluginPrintTscMain is not null) PluginPrintTscMain.LabelPrintedCount = 1;
        if (PluginPrintZebraMain is not null) PluginPrintZebraMain.LabelPrintedCount = 1;
    }

    /// <summary>
    /// Настроить сессию для ПО `Печать этикеток`.
    /// </summary>
    public void SetSessionForLabelPrint(Action<WsFormBaseUserControl, string> showNavigation)
    {
        SetSqlPublish();
        ContextCache.LoadGlobal();
        WsSqlDeviceModel device = ContextManager.DeviceRepository.GetItemByName(DeviceName);
        device = WsFormNavigationUtils.SetNewDeviceWithQuestion(showNavigation,
        device, MdNetUtils.GetLocalIpAddress(), MdNetUtils.GetLocalMacAddress());
        WsSqlDeviceTypeFkModel deviceTypeFk = new WsSqlDeviceTypeFkRepository().GetItemByDevice(device);
        if (deviceTypeFk.IsNew)
        {
            WsSqlDeviceTypeModel deviceType = new WsSqlDeviceTypeRepository().GetItemByName("Monoblock");
            deviceTypeFk.Device = device;
            deviceTypeFk.Type = deviceType;
            ContextManager.SqlCore.Save(deviceTypeFk);
        }
        Line = ContextManager.LineRepository.GetItemByDevice(deviceTypeFk.Device);
        SetAreaByLineWorkShop();
        SetPluLine();
        ProductDate = DateTime.Now;
        WeighingSettings = new();
    }
    
    public void SetSessionForLabelPrintCustom(WsSqlScaleModel line, WsSqlProductionSiteModel area)
    {
        ContextCache.LoadGlobal();
        Line = line;
        SetAreaByLineWorkShop();
        SetPluLine();
        SetArea(area);
        ProductDate = DateTime.Now;
        WeighingSettings = new();
    }

    /// <summary>
    /// Задать настройки публикации.
    private void SetSqlPublish() =>
        PublishDescription = Debug.Config switch
        {
            WsEnumConfiguration.DevelopAleksandrov => WsLocaleCore.Sql.SqlServerDevelopAleksandrov,
            WsEnumConfiguration.DevelopMorozov => WsLocaleCore.Sql.SqlServerDevelopMorozov,
            WsEnumConfiguration.DevelopVS => WsLocaleCore.Sql.SqlServerVS,
            WsEnumConfiguration.ReleaseVS => "",
            _ => throw new ArgumentOutOfRangeException()
        };

    #endregion

    #region Public and private methods

    /// <summary>
    /// Смена площадки.
    /// </summary>
    private void SetArea(WsSqlProductionSiteModel area)
    {
        Area = area;
        ContextManager.ContextItem.SaveLogInformation($"{WsLocaleCore.LabelPrint.SetAreaWithParam(Area.IdentityValueId, Area.Name)}");
    }

    /// <summary>
    /// Смена площадки из цеха линии.
    /// </summary>
    private void SetAreaByLineWorkShop()
    {
        WsSqlWorkShopModel workShop = ContextCache.WorkShops.Find(
            item => item.IdentityValueId.Equals(Line.WorkShop.IdentityValueId));
        if (workShop.IsExists)
            Area = ContextCache.Areas.Find(
                item => item.IdentityValueId.Equals(workShop.ProductionSite.IdentityValueId));
        else
            Area = ContextManager.ProductionSiteRepository.GetNewItem();
        // Журналирование смены площадки.
        ContextManager.ContextItem.SaveLogInformation(
            $"{WsLocaleCore.LabelPrint.SetAreaWithParam(Area.IdentityValueId, Area.Name)}");
    }

    /// <summary>
    /// Смена ПЛУ линии.
    /// </summary>
    public void SetPluLine(WsSqlPluScaleModel? pluLine = null)
    {
        PluLine = pluLine ?? ContextManager.PluLineRepository.GetNewItem();
        // Журналирование смены ПЛУ на линии.
        if (PluLine.IsExists)
            ContextManager.ContextItem.SaveLogInformation($"{WsLocaleCore.LabelPrint.SetPlu(PluLine.Plu.Number, PluLine.Plu.Name)}");
        
        if (PluginPrintTscMain is not null) PluginPrintTscMain.LabelPrintedCount = 1;
        if (PluginPrintZebraMain is not null) PluginPrintZebraMain.LabelPrintedCount = 1;
        // Смена вложенности ПЛУ.
        SetViewPluNesting();
    }

    /// <summary>
    /// Смена вложенности ПЛУ.
    /// </summary>
    public void SetViewPluNesting(WsSqlViewPluNestingModel? viewPluNesting = null)
    {
        if (viewPluNesting is null && PluLine.IsExists)
        {
            viewPluNesting = ContextCache.ViewPlusNesting.Find(
                item => item.PluUid.Equals(PluLine.Plu.IdentityValueUid) && item.IsDefault);
            ViewPluNesting = viewPluNesting ?? ContextManager.PluNestingFkRepository.GetNewView();
        }
        else if (viewPluNesting is null)
        {
            ViewPluNesting = ContextManager.PluNestingFkRepository.GetNewView();
        }
        else
            ViewPluNesting = viewPluNesting;
        // Журналирование смены вложенности ПЛУ.
        if (PluLine.IsExists)
            ContextManager.ContextItem.SaveLogInformation(
                $"{WsLocaleCore.LabelPrint.SetPluNesting(ViewPluNesting.PluNumber, ViewPluNesting.PluName, ViewPluNesting.BundleCount)}");
    }
    

    /// <summary>
    /// Инкремент счётчика этикеток.
    /// </summary>
    public void AddLineCounter()
    {
        Line.LabelCounter++;
        PluLine.Line.LabelCounter = Line.LabelCounter;
        ContextManager.SqlCore.Update(Line);
    }

    #endregion
}