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

    #region Public and private fields, properties, constructor

    private WsSqlCrudConfigModel SqlCrudConfig => new(new List<WsSqlFieldFilterModel>(),
        true, false, false, true, false);
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
    public List<WsSqlProductionFacilityModel> ProductionFacilitiesDb { get; private set; } = new();
    public List<WsSqlScaleModel> ScalesDb { get; private set; } = new();
    public List<WsSqlViewPluScaleModel> ViewPlusScalesDb { get; private set; } = new();
    public List<WsSqlViewPluScaleModel> CurrentViewPlusScalesDb { get; private set; } = new();
    public List<WsSqlViewPluStorageMethodModel> ViewPlusStorageMethods { get; private set; } = new();
    public List<WsSqlViewPluNestingModel> ViewPlusNesting { get; set; } = new();

    #endregion

    #region Public and private methods

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
        if (!ProductionFacilitiesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ProductionFacilities))
            ProductionFacilitiesDb = ContextManager.ContextList.GetListNotNullableProductionFacilities(SqlCrudConfig);
        if (!ScalesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Scales))
            ScalesDb = ContextManager.ContextList.GetListNotNullableScales(SqlCrudConfig);
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
        if (!ViewPlusScalesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ViewPlusScales))
            ViewPlusScalesDb = ContextManager.ContextView.GetListViewPlusScales();
        if (!ViewPlusStorageMethods.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ViewPluStorageMethods))
            ViewPlusStorageMethods = ContextManager.ContextView.GetListViewPlusStorageMethods();
        if (!ViewPlusNesting.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.ViewPluNesting))
            ViewPlusNesting = ContextManager.ContextView.GetListViewPlusNesting();
        
        // Optimize.
        if (TableName.Equals(WsSqlTableName.All))
            TableName = WsSqlTableName.None;
    }

    public List<WsSqlViewPluScaleModel> GetViewPlusScalesDb(ushort scaleId) => 
        ViewPlusScalesDb.Where(item => Equals(item.ScaleId, scaleId) && item.IsActive).ToList();

    public List<WsSqlViewPluScaleModel> GetViewPlusScalesDb(ushort scaleId, int pageNumber, ushort pageSize) =>
        ViewPlusScalesDb.Where(item => Equals(item.ScaleId, scaleId) && item.IsActive)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();

    public void LoadCurrentViewPlusScales(ushort scaleId)
    {
        CurrentViewPlusScalesDb = ContextManager.ContextView.GetListViewPlusScales(scaleId);
    }

    public List<WsSqlViewPluScaleModel> GetCurrentViewPlusScalesDb(int pageNumber, ushort pageSize) =>
        CurrentViewPlusScalesDb.Where(item => item.IsActive)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();

    //public void LoadCurrentViewPlusStorageMethodsFks(ushort scaleId)
    //{
    //    CurrentViewPlusStorageMethodsFks = ContextManager.ContextView.GetListViewPlusStorageMethods();
    //}

    #endregion
}