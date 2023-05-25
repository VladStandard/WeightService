// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// Помощник кэша.
/// </summary>
public sealed class WsSqlContextCacheHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextCacheHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextCacheHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor - Глобальный кэш таблиц

    private WsSqlCrudConfigModel SqlCrudConfig => new(new List<WsSqlFieldFilterModel>(),
        WsSqlIsMarked.ShowAll, false, false, true, false);
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlTableName TableName { get; set; } = WsSqlTableName.None;
    
    public List<WsSqlBoxModel> Boxes { get; private set; } = new();
    public List<WsSqlBrandModel> Brands { get; private set; } = new();
    public List<WsSqlBundleModel> Bundles { get; private set; } = new();
    public List<WsSqlClipModel> Clips { get; private set; } = new();
    public List<WsSqlPlu1CFkModel> Plus1CFks { get; private set; } = new();
    public List<WsSqlPluBrandFkModel> PluBrandsFks { get; private set; } = new();
    public List<WsSqlPluBundleFkModel> PluBundlesFks { get; private set; } = new();
    public List<WsSqlPluCharacteristicModel> PluCharacteristics { get; private set; } = new();
    public List<WsSqlPluCharacteristicsFkModel> PluCharacteristicsFks { get; private set; } = new();
    public List<WsSqlPluClipFkModel> PluClipsFks { get; private set; } = new();
    public List<WsSqlPluFkModel> PluFks { get; private set; } = new();
    public List<WsSqlPluGroupFkModel> PluGroupsFks { get; private set; } = new();
    public List<WsSqlPluGroupModel> PluGroups { get; private set; } = new();
    public List<WsSqlPluModel> Plus { get; private set; } = new();
    public List<WsSqlProductionFacilityModel> Areas { get; private set; } = new();
    public List<WsSqlWorkShopModel> WorkShops { get; private set; } = new();
    public List<WsSqlScaleModel> Lines { get; private set; } = new();
    
    #endregion

    #region Public and private fields, properties, constructor - Глобальный кэш представлений

    public List<WsSqlViewPluLineModel> ViewPlusLines { get; private set; } = new();
    public List<WsSqlViewPluStorageMethodModel> ViewPlusStorageMethods { get; private set; } = new();
    public List<WsSqlViewPluNestingModel> ViewPlusNesting { get; set; } = new();

    #endregion

    #region Public and private fields, properties, constructor - Локальный кэш представлений

    public List<WsSqlViewPluLineModel> LocalViewPlusLines { get; private set; } = new();

    #endregion

    #region Public and private methods - Глобальный кэш

    /// <summary>
    /// Прогреть кэш.
    /// </summary>
    public void Load() => Load(TableName);

    /// <summary>
    /// Прогреть кэш.
    /// </summary>
    public void Load(WsSqlTableName tableName)
    {
        // Tables.
        if (!Areas.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Areas))
            Areas = ContextManager.ContextList.GetListNotNullableAreas(SqlCrudConfig);
        if (!WorkShops.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.WorkShops))
            WorkShops = ContextManager.ContextList.GetListNotNullableWorkShops(SqlCrudConfig);
        if (!Plus.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Plus)) 
            Plus = ContextManager.ContextList.GetListNotNullablePlus(SqlCrudConfig);
        if (!Lines.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Lines))
            Lines = ContextManager.ContextList.GetListNotNullableScales(SqlCrudConfig);
        if (!PluFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluFks)) 
            PluFks = ContextManager.ContextList.GetListNotNullablePlusFks(SqlCrudConfig);
        if (!Boxes.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Boxes)) 
            Boxes = ContextManager.ContextList.GetListNotNullableBoxes(SqlCrudConfig);
        if (!Bundles.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Bundles)) 
            Bundles = ContextManager.ContextList.GetListNotNullableBundles(SqlCrudConfig);
        if (!PluBundlesFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluBundlesFks)) 
            PluBundlesFks = ContextManager.ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);
        if (!PluBrandsFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluBrandsFks)) 
            PluBrandsFks = ContextManager.ContextList.GetListNotNullablePlusBrandsFks(SqlCrudConfig);
        if (!Clips.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Clips)) 
            Clips = ContextManager.ContextList.GetListNotNullableClips(SqlCrudConfig);
        if (!PluClipsFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluClipsFks)) 
            PluClipsFks = ContextManager.ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);
        if (!Plus1CFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Plus1CFks)) 
            Plus1CFks = ContextManager.ContextPlu1CFk.GetList();
        if (!PluCharacteristics.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluCharacteristics)) 
            PluCharacteristics = ContextManager.ContextList.GetListNotNullablePlusCharacteristics(SqlCrudConfig);
        if (!PluCharacteristicsFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluCharacteristicsFks)) 
            PluCharacteristicsFks = ContextManager.ContextList.GetListNotNullablePlusCharacteristicsFks(SqlCrudConfig);
        if (!PluGroups.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluGroups)) 
            PluGroups = ContextManager.ContextList.GetListNotNullablePlusGroups(SqlCrudConfig);
        if (!PluGroupsFks.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluGroupsFks)) 
            PluGroupsFks = ContextManager.ContextList.GetListNotNullablePlusGroupFks(SqlCrudConfig);
        if (!Brands.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Brands)) 
            Brands = ContextManager.ContextList.GetListNotNullableBrands(SqlCrudConfig);
        
        // Views.
        if (!ViewPlusLines.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ViewPlusLines))
            ViewPlusLines = ContextManager.ContextView.GetListViewPlusScales();
        if (!ViewPlusStorageMethods.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ViewPlusStorageMethods))
            ViewPlusStorageMethods = ContextManager.ContextView.GetListViewPlusStorageMethods();
        if (!ViewPlusNesting.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ViewPlusNesting))
            ViewPlusNesting = ContextManager.ContextView.GetListViewPlusNesting();
        
        // Optimize.
        if (TableName.Equals(WsSqlTableName.All))
            TableName = WsSqlTableName.None;
    }

    /// <summary>
    /// Обновить глобальный кэш.
    /// </summary>
    public void LoadGlobal()
    {
        Load(WsSqlTableName.Areas);
        Load(WsSqlTableName.WorkShops);
        Load(WsSqlTableName.Lines);
        Load(WsSqlTableName.ViewPlusNesting);
        Load(WsSqlTableName.ViewPlusStorageMethods);
    }

    public List<WsSqlViewPluLineModel> GetViewPlusScalesDb(ushort scaleId) => 
        ViewPlusLines.Where(item => Equals(item.ScaleId, scaleId) && item.IsActive).ToList();

    public List<WsSqlViewPluLineModel> GetViewPlusScalesDb(ushort scaleId, int pageNumber, ushort pageSize) =>
        ViewPlusLines.Where(item => Equals(item.ScaleId, scaleId) && item.IsActive)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();


    #endregion

    #region Public and private methods - Локальный кэш

    public void LoadLocalViewPlusLines(ushort scaleId)
    {
        LocalViewPlusLines = ContextManager.ContextView.GetListViewPlusScales(scaleId);
    }

    public List<WsSqlViewPluLineModel> GetCurrentViewPlusScales(int pageNumber, ushort pageSize) =>
        LocalViewPlusLines.Where(item => item.IsActive)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();

    #endregion
}