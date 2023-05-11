//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace WsWebApiCore.Helpers;

///// <summary>
///// Помощник кэша веб-сервисов.
///// </summary>
//public sealed class WsSqlCacheServiceHelper
//{
//    #region Design pattern "Lazy Singleton"

//#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//    private static WsSqlCacheServiceHelper _instance;
//#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//    public static WsSqlCacheServiceHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

//    #endregion

//    #region Public and private fields, properties, constructor

//    private WsSqlCrudConfigModel SqlCrudConfig => new(new List<WsSqlFieldFilterModel>(),
//        true, false, false, true, false);
//    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
//    private WsSqlTableName TableName { get; set; } = WsSqlTableName.All;
//    public List<WsSqlPluModel> PlusDb { get; private set; } = new();
//    public List<WsSqlPluFkModel> PluFksDb { get; private set; } = new();
//    public List<WsSqlBoxModel> BoxesDb { get; private set; } = new();
//    public List<WsSqlBundleModel> BundlesDb { get; private set; } = new();
//    public List<WsSqlPluBundleFkModel> PluBundlesFksDb { get; private set; } = new();
//    public List<WsSqlPluBrandFkModel> PluBrandsFksDb { get; private set; } = new();
//    public List<WsSqlClipModel> ClipsDb { get; private set; } = new();
//    public List<WsSqlPluClipFkModel> PluClipsFksDb { get; private set; } = new();
//    public List<WsSqlPluNestingFkModel> PluNestingFksDb { get; private set; } = new();
//    public List<WsSqlPlu1CFkModel> Plus1CFksDb { get; private set; } = new();
//    public List<WsSqlPluCharacteristicModel> PluCharacteristicsDb { get; private set; } = new();
//    public List<WsSqlPluCharacteristicsFkModel> PluCharacteristicsFksDb { get; private set; } = new();
//    public List<WsSqlPluGroupModel> PluGroupsDb { get; private set; } = new();
//    public List<WsSqlPluGroupFkModel> PluGroupsFksDb { get; private set; } = new();
//    public List<WsSqlBrandModel> BrandsDb { get; private set; } = new();

//    #endregion

//    #region Public and private methods

//    /// <summary>
//    /// Прогреть кэш.
//    /// </summary>
//    public void Load() => Load(TableName);
//    /// <summary>
//    /// Прогреть кэш.
//    /// </summary>
//    public void Load(WsSqlTableName tableName)
//    {
//        if (!PlusDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Plus)) 
//            PlusDb = ContextManager.ContextList.GetListNotNullablePlus(SqlCrudConfig);
//        if (!PluFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluFks)) 
//            PluFksDb = ContextManager.ContextList.GetListNotNullablePlusFks(SqlCrudConfig);
//        if (!BoxesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Boxes)) 
//            BoxesDb = ContextManager.ContextList.GetListNotNullableBoxes(SqlCrudConfig);
//        if (!BundlesDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Bundles)) 
//            BundlesDb = ContextManager.ContextList.GetListNotNullableBundles(SqlCrudConfig);
//        if (!PluBundlesFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluBundlesFks)) 
//            PluBundlesFksDb = ContextManager.ContextList.GetListNotNullablePlusBundlesFks(SqlCrudConfig);
//        if (!PluBrandsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluBrandsFks)) 
//            PluBrandsFksDb = ContextManager.ContextList.GetListNotNullablePlusBrandsFks(SqlCrudConfig);
//        if (!ClipsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Clips)) 
//            ClipsDb = ContextManager.ContextList.GetListNotNullableClips(SqlCrudConfig);
//        if (!PluClipsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluClipsFks)) 
//            PluClipsFksDb = ContextManager.ContextList.GetListNotNullablePlusClipsFks(SqlCrudConfig);
//        if (!PluNestingFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluNestingFks)) 
//            PluNestingFksDb = ContextManager.ContextList.GetListNotNullablePlusNestingFks(
//                new(WsSqlQueriesScales.Tables.PluNestingFks.GetList(false), false));
//        if (!Plus1CFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Plus1CFks)) 
//            Plus1CFksDb = ContextManager.ContextPlu1CFk.GetList();
//        if (!PluCharacteristicsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluCharacteristics)) 
//            PluCharacteristicsDb = ContextManager.ContextList.GetListNotNullablePlusCharacteristics(SqlCrudConfig);
//        if (!PluCharacteristicsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluCharacteristicsFks)) 
//            PluCharacteristicsFksDb = ContextManager.ContextList.GetListNotNullablePlusCharacteristicsFks(SqlCrudConfig);
//        if (!PluGroupsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluGroups)) 
//            PluGroupsDb = ContextManager.ContextList.GetListNotNullablePlusGroups(SqlCrudConfig);
//        if (!PluGroupsFksDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.PluGroupsFks)) 
//            PluGroupsFksDb = ContextManager.ContextList.GetListNotNullablePlusGroupFks(SqlCrudConfig);
//        if (!BrandsDb.Any() || Equals(tableName, WsSqlTableName.All) || Equals(tableName, WsSqlTableName.Brands)) 
//            BrandsDb = ContextManager.ContextList.GetListNotNullableBrands(SqlCrudConfig);
//        // Optimize.
//        if (TableName.Equals(WsSqlTableName.All))
//            TableName = WsSqlTableName.None;
//    }

//    #endregion
//}