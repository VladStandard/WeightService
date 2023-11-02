using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Entities.SchemaRef.ProductionSites;
using WsStorageCore.Entities.SchemaRef.WorkShops;
using WsStorageCore.Entities.SchemaScale.PlusNestingFks;
using WsStorageCore.Entities.SchemaScale.PlusScales;
using WsStorageCore.Entities.SchemaScale.PlusWeightings;
using WsStorageCore.Entities.SchemaScale.Scales;
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
    public WsPluginPrintTscModel? PluginPrintTscMain { get; set; }
    public WsPluginPrintZebraModel? PluginPrintZebraMain { get; set; }
    public WsSqlPluWeighingEntity PluWeighing { get; set; }
    public WsWeighingSettingsModel WeighingSettings { get; private set; }
    public WsSqlPluScaleEntity PluLine { get; private set; }
    public int PlusPageNumber { get; set; }
    public WsSqlProductionSiteEntity Area { get; private set; }
    public WsSqlScaleEntity Line { get; private set; }
    public string PublishDescription { get; private set; } = "";
    public DateTime ProductDate { get; set; }
    public WsSqlViewPluNestingModel ViewPluNesting { get; private set; }
    public WsLocalizationManager Localization { get; set; } = new();

    public WsLabelSessionHelper()
    {
        Area = new WsSqlProductionSiteRepository().GetNewItem();
        PluLine = new WsSqlPluLineRepository().GetNewItem();
        Line =  new WsSqlLineRepository().GetNewItem();
        PluWeighing =  new WsSqlPluWeighingRepository().GetNewItem();
        ViewPluNesting =  new WsSqlPluNestingFkRepository().GetNewView();
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
        WsSqlHostEntity host = new WsSqlHostRepository().GetItemByName(DeviceName);
        host = WsFormNavigationUtils.SetNewDeviceWithQuestion(showNavigation, host, MdNetUtils.GetLocalIpAddress());
        Line = new WsSqlLineRepository().GetItemByHost(host);
        SetAreaByLineWorkShop();
        SetPluLine();
        ProductDate = DateTime.Now;
        WeighingSettings = new();
    }
    
    public void SetSessionForLabelPrintCustom(WsSqlScaleEntity line, WsSqlProductionSiteEntity area)
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
    private void SetArea(WsSqlProductionSiteEntity area)
    {
        Area = area;
        ContextManager.ContextItem.SaveLogInformation($"{WsLocaleCore.LabelPrint.SetAreaWithParam(Area.IdentityValueId, Area.Name)}");
    }

    /// <summary>
    /// Смена площадки из цеха линии.
    /// </summary>
    private void SetAreaByLineWorkShop()
    {
        WsSqlWorkShopEntity workShop = ContextCache.WorkShops.Find(
            item => item.IdentityValueId.Equals(Line.WorkShop.IdentityValueId));
        if (workShop.IsExists)
            Area = ContextCache.Areas.Find(
            item => item.IdentityValueId.Equals(workShop.ProductionSite.IdentityValueId));
        else
            Area = new WsSqlProductionSiteRepository().GetNewItem();
    }

    /// <summary>
    /// Смена ПЛУ линии.
    /// </summary>
    public void SetPluLine(WsSqlPluScaleEntity? pluLine = null)
    {
        PluLine = pluLine ?? new WsSqlPluLineRepository().GetNewItem();
        
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
            ViewPluNesting = viewPluNesting ??  new WsSqlPluNestingFkRepository().GetNewView();
        }
        else if (viewPluNesting is null)
        {
            ViewPluNesting =  new WsSqlPluNestingFkRepository().GetNewView();
        }
        else
            ViewPluNesting = viewPluNesting;
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