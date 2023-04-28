// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Helpers;

public sealed class WsWebApiCacheHelper //: WsContentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsWebApiCacheHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsWebApiCacheHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private SqlCrudConfigModel SqlCrudConfig => new(new List<SqlFieldFilterModel>(),
        true, false, false, true, false);
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public List<PluModel> PlusDb { get; set; } = new();
    public List<PluFkModel> PluFksDb { get; set; } = new();
    public List<BoxModel> BoxesDb { get; set; } = new();
    public List<BundleModel> BundlesDb { get; set; } = new();
    public List<PluBundleFkModel> PluBundlesFksDb { get; set; } = new();
    public List<PluBrandFkModel> PluBrandsFksDb { get; set; } = new();
    public List<ClipModel> ClipsDb { get; set; } = new();
    public List<PluClipFkModel> PluClipsFksDb { get; set; } = new();
    public List<PluNestingFkModel> PluNestingFksDb { get; set; } = new();
    public List<WsSqlPlu1cFkModel> Plus1cFksDb { get; set; } = new();
    public List<PluCharacteristicModel> PluCharacteristicsDb { get; set; } = new();
    public List<PluCharacteristicsFkModel> PluCharacteristicsFksDb { get; set; } = new();
    public List<PluGroupModel> PluGroupsDb { get; set; } = new();
    public List<PluGroupFkModel> PluGroupsFksDb { get; set; } = new();
    public List<BrandModel> BrandsDb { get; set; } = new();

    #endregion

    #region Public and private methods

    /// <summary>
    /// Прогреть кеш.
    /// </summary>
    public void Load()
    {
        if (!PlusDb.Any()) 
            PlusDb = ContextManager.ContextList.GetListNotNullablePlus(SqlCrudConfig);
        if (!PluFksDb.Any()) 
            PluFksDb = ContextManager.ContextList.GetListNotNullablePlusFks(SqlCrudConfig);
        if (!BoxesDb.Any()) 
            BoxesDb = ContextManager.ContextList.GetListNotNullableBoxes(SqlCrudConfig);
        if (!BundlesDb.Any()) 
            BundlesDb = ContextManager.ContextList.GetListNotNullableBundles(SqlCrudConfig);
        if (!PluBundlesFksDb.Any()) 
            PluBundlesFksDb = ContextManager.ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);
        if (!PluBrandsFksDb.Any()) 
            PluBrandsFksDb = ContextManager.ContextList.GetListNotNullablePlusBrandsFks(SqlCrudConfig);
        if (!ClipsDb.Any()) 
            ClipsDb = ContextManager.ContextList.GetListNotNullableClips(SqlCrudConfig);
        if (!PluClipsFksDb.Any()) 
            PluClipsFksDb = ContextManager.ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);
        if (!PluNestingFksDb.Any()) 
            PluNestingFksDb = ContextManager.ContextList.GetListNotNullablePlusNestingFks(
            new(WsSqlQueriesScales.Tables.PluNestingFks.GetList(false), false));
        if (!Plus1cFksDb.Any()) 
            Plus1cFksDb = ContextManager.ContextPlu1cFk.GetList();
        if (!PluCharacteristicsDb.Any())
            PluCharacteristicsDb = ContextManager.ContextList.GetListNotNullablePlusCharacteristics(SqlCrudConfig);
        if (!PluCharacteristicsFksDb.Any())
            PluCharacteristicsFksDb = ContextManager.ContextList.GetListNotNullablePlusCharacteristicsFks(SqlCrudConfig);
        if (!PluGroupsDb.Any())
            PluGroupsDb = ContextManager.ContextList.GetListNotNullablePlusGroups(SqlCrudConfig);
        if (!PluGroupsFksDb.Any())
            PluGroupsFksDb = ContextManager.ContextList.GetListNotNullablePlusGroupFks(SqlCrudConfig);
        if (!BrandsDb.Any())
            BrandsDb = ContextManager.ContextList.GetListNotNullableBrands(SqlCrudConfig);
    }

    #endregion
}