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
public sealed class LabelSessionHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static LabelSessionHelper _instance;
#pragma warning restore CS8618
    public static LabelSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties
    
    private SqlContextItemHelper ContextItem => SqlContextItemHelper.Instance;
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    private static DebugHelper Debug => DebugHelper.Instance;
    private static DateTime ProductDateMaxValue => DateTime.Now.AddDays(+31);
    private static DateTime ProductDateMinValue => DateTime.Now.AddDays(-31);
    
    public static SqlContextCacheHelper ContextCache => SqlContextCacheHelper.Instance;
    public static ushort PlusPageColumnCount => 4;
    public static ushort PlusPageSize => 16;
    public static ushort PlusPageRowCount => 4;
    public static string DeviceName => Environment.MachineName;
    public PluginPrintTscModel? PluginPrintTscMain { get; set; }
    public PluginPrintZebraModel? PluginPrintZebraMain { get; set; }
    public SqlPluWeighingEntity PluWeighing { get; set; }
    public WeighingSettingsModel WeighingSettings { get; private set; }
    public SqlPluScaleEntity PluLine { get; private set; }
    public int PlusPageNumber { get; set; }
    public SqlProductionSiteEntity Area { get; private set; }
    public SqlScaleEntity Line { get; private set; }
    public string PublishDescription { get; private set; } = "";
    public DateTime ProductDate { get; set; }
    public SqlViewPluNestingModel ViewPluNesting { get; private set; }
    public LocalizationManager Localization { get; set; } = new();

    public LabelSessionHelper()
    {
        Area = new SqlProductionSiteRepository().GetNewItem();
        PluLine = new SqlPluLineRepository().GetNewItem();
        Line =  new SqlLineRepository().GetNewItem();
        PluWeighing =  new SqlPluWeighingRepository().GetNewItem();
        ViewPluNesting =  new SqlPluNestingFkRepository().GetNewView();
        WeighingSettings = new();
    }

    #endregion

    #region Public and private methods

    public int GetPlusPageCount() =>
        ContextCache.LocalViewPlusLines.Where(item => item.IsActive).ToList().Count / PlusPageSize;

    public void RotateProductDate(EnumDirection direction)
    {
        switch (direction)
        {
            case EnumDirection.Left:
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue) ProductDate = ProductDateMinValue;
                break;
            }
            case EnumDirection.Right:
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
    public void SetSessionForLabelPrint(Action<FormBaseUserControl, string> showNavigation)
    {
        SetSqlPublish();
        ContextCache.LoadGlobal();
        SqlHostEntity host = new SqlHostRepository().GetItemByName(DeviceName);
        host = FormNavigationUtils.SetNewDeviceWithQuestion(showNavigation, host, MdNetUtils.GetLocalIpAddress());
        Line = new SqlLineRepository().GetItemByHost(host);
        SetAreaByLineWorkShop();
        SetPluLine();
        ProductDate = DateTime.Now;
        WeighingSettings = new();
    }
    
    public void SetSessionForLabelPrintCustom(SqlScaleEntity line, SqlProductionSiteEntity area)
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
            EnumConfiguration.DevelopVs => LocaleCore.Sql.SqlServerVS,
            EnumConfiguration.ReleaseVs => "",
            _ => throw new ArgumentOutOfRangeException()
        };

    #endregion

    #region Public and private methods

    /// <summary>
    /// Смена площадки.
    /// </summary>
    private void SetArea(SqlProductionSiteEntity area)
    {
        Area = area;
        ContextItem.SaveLogInformation($"{LocaleCore.LabelPrint.SetAreaWithParam(Area.IdentityValueId, Area.Name)}");
    }

    /// <summary>
    /// Смена площадки из цеха линии.
    /// </summary>
    private void SetAreaByLineWorkShop()
    {
        SqlWorkShopEntity workShop = ContextCache.WorkShops.Find(
            item => item.IdentityValueId.Equals(Line.WorkShop.IdentityValueId));
        if (workShop.IsExists)
            Area = ContextCache.Areas.Find(
            item => item.IdentityValueId.Equals(workShop.ProductionSite.IdentityValueId));
        else
            Area = new SqlProductionSiteRepository().GetNewItem();
    }

    /// <summary>
    /// Смена ПЛУ линии.
    /// </summary>
    public void SetPluLine(SqlPluScaleEntity? pluLine = null)
    {
        PluLine = pluLine ?? new SqlPluLineRepository().GetNewItem();
        
        if (PluginPrintTscMain is not null) PluginPrintTscMain.LabelPrintedCount = 1;
        if (PluginPrintZebraMain is not null) PluginPrintZebraMain.LabelPrintedCount = 1;
        // Смена вложенности ПЛУ.
        SetViewPluNesting();
    }

    /// <summary>
    /// Смена вложенности ПЛУ.
    /// </summary>
    public void SetViewPluNesting(SqlViewPluNestingModel? viewPluNesting = null)
    {
        if (viewPluNesting is null && PluLine.IsExists)
        {
            viewPluNesting = ContextCache.ViewPlusNesting.Find(
                item => item.PluUid.Equals(PluLine.Plu.IdentityValueUid) && item.IsDefault);
            ViewPluNesting = viewPluNesting ??  new SqlPluNestingFkRepository().GetNewView();
        }
        else if (viewPluNesting is null)
        {
            ViewPluNesting =  new SqlPluNestingFkRepository().GetNewView();
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
        SqlCore.Update(Line);
    }

    #endregion
}