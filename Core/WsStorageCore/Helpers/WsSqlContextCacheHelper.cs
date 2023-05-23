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
    public List<WsSqlBoxModel> BoxesDb { get; private set; } = new();
    public List<WsSqlBrandModel> BrandsDb { get; private set; } = new();
    public List<WsSqlBundleModel> BundlesDb { get; private set; } = new();
    public List<WsSqlClipModel> ClipsDb { get; private set; } = new();
    public List<WsSqlPlu1CFkModel> Plus1CFksDb { get; private set; } = new();
    public List<WsSqlPluBrandFkModel> PluBrandsFksDb { get; private set; } = new();
    public List<WsSqlPluBundleFkModel> PluBundlesFksDb { get; private set; } = new();
    public List<WsSqlPluCharacteristicModel> PluCharacteristicsDb { get; private set; } = new();
    public List<WsSqlPluCharacteristicsFkModel> PluCharacteristicsFksDb { get; private set; } = new();
    public List<WsSqlPluClipFkModel> PluClipsFksDb { get; private set; } = new();
    public List<WsSqlPluFkModel> PluFksDb { get; private set; } = new();
    public List<WsSqlPluGroupFkModel> PluGroupsFksDb { get; private set; } = new();
    public List<WsSqlPluGroupModel> PluGroupsDb { get; private set; } = new();
    public List<WsSqlPluModel> PlusDb { get; private set; } = new();
    public List<WsSqlProductionFacilityModel> ProductionFacilities { get; private set; } = new();
    public List<WsSqlScaleModel> Scales { get; private set; } = new();
    
    #endregion

    #region Public and private fields, properties, constructor - Глобальный кэш представлений

    public List<WsSqlViewPluLineModel> ViewPlusLines { get; private set; } = new();
    public List<WsSqlViewPluStorageMethodModel> ViewPlusStorageMethods { get; private set; } = new();
    public List<WsSqlViewPluNestingModel> ViewPlusNesting { get; set; } = new();

    #endregion

    #region Public and private fields, properties, constructor - Локальный кэш представлений

    public List<WsSqlViewPluLineModel> LocalViewPlusLines { get; private set; } = new();
    public List<WsSqlViewPluNestingModel> LocalViewPlusNesting { get; private set; } = new();

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
        if (!PlusDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Plus)) 
            PlusDb = ContextManager.ContextList.GetListNotNullablePlus(SqlCrudConfig);
        if (!ProductionFacilities.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ProductionFacilities))
            ProductionFacilities = ContextManager.ContextList.GetListNotNullableProductionFacilities(SqlCrudConfig);
        if (!Scales.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Scales))
            Scales = ContextManager.ContextList.GetListNotNullableScales(SqlCrudConfig);
        if (!PluFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluFks)) 
            PluFksDb = ContextManager.ContextList.GetListNotNullablePlusFks(SqlCrudConfig);
        if (!BoxesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Boxes)) 
            BoxesDb = ContextManager.ContextList.GetListNotNullableBoxes(SqlCrudConfig);
        if (!BundlesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Bundles)) 
            BundlesDb = ContextManager.ContextList.GetListNotNullableBundles(SqlCrudConfig);
        if (!PluBundlesFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluBundlesFks)) 
            PluBundlesFksDb = ContextManager.ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);
        if (!PluBrandsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluBrandsFks)) 
            PluBrandsFksDb = ContextManager.ContextList.GetListNotNullablePlusBrandsFks(SqlCrudConfig);
        if (!ClipsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Clips)) 
            ClipsDb = ContextManager.ContextList.GetListNotNullableClips(SqlCrudConfig);
        if (!PluClipsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluClipsFks)) 
            PluClipsFksDb = ContextManager.ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);
        if (!Plus1CFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Plus1CFks)) 
            Plus1CFksDb = ContextManager.ContextPlu1CFk.GetList();
        if (!PluCharacteristicsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluCharacteristics)) 
            PluCharacteristicsDb = ContextManager.ContextList.GetListNotNullablePlusCharacteristics(SqlCrudConfig);
        if (!PluCharacteristicsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluCharacteristicsFks)) 
            PluCharacteristicsFksDb = ContextManager.ContextList.GetListNotNullablePlusCharacteristicsFks(SqlCrudConfig);
        if (!PluGroupsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluGroups)) 
            PluGroupsDb = ContextManager.ContextList.GetListNotNullablePlusGroups(SqlCrudConfig);
        if (!PluGroupsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluGroupsFks)) 
            PluGroupsFksDb = ContextManager.ContextList.GetListNotNullablePlusGroupFks(SqlCrudConfig);
        if (!BrandsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Brands)) 
            BrandsDb = ContextManager.ContextList.GetListNotNullableBrands(SqlCrudConfig);
        
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
    public void RefreshGlobalCacheForLabelPrint()
    {
        Load(WsSqlTableName.ProductionFacilities);
        Load(WsSqlTableName.Scales);
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

    public void LoadLocalViewPlusNesting(ushort pluNumber)
    {
        LocalViewPlusNesting = ContextManager.ContextView.GetListViewPlusNesting(pluNumber);
    }

    #endregion
}