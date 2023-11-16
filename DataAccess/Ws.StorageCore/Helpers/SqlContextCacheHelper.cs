namespace Ws.StorageCore.Helpers;

/// <summary>
/// Помощник кэша.
/// </summary>
public sealed class SqlContextCacheHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlContextCacheHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlContextCacheHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor - Инициализация репозиториев
    
    private SqlViewPluLineRepository PluLineRepository { get; } = new();
    private SqlViewPluNestingRepository ViewPluNestingFkRepository { get; } = new();
    private SqlViewPluStorageMethodRepository ViewPluStorageMethodRepository { get; } = new();
    private SqlViewTableSizeRepository ViewTableSizeRepository { get; } = new();
    private SqlBoxRepository BoxRepository { get; } = new();
    private SqlWorkShopRepository WorkShopRepository { get; } = new();
    private SqlProductionSiteRepository ProductionSiteRepository { get; } = new();
    private SqlBundleRepository BundleRepository  { get; } = new();
    private SqlLineRepository LineRepository  { get; } = new();
    private SqlPluClipFkRepository PluClipFkRepository { get; } = new();
    private SqlClipRepository ClipRepository { get; } = new();
    private SqlPluRepository PluRepository { get; } = new();
    private SqlPluFkRepository PluFkRepository { get; } = new();
    private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    #endregion
    
    #region Public and private fields, properties, constructor - Глобальный кэш таблиц
    
    private SqlCrudConfigModel SqlCrudConfig => SqlCrudConfigFactory.GetCrudAll();
    private SqlEnumTableName TableName { get; set; } = SqlEnumTableName.None;
    public List<SqlBoxEntity> Boxes { get; private set; } = new();
    public List<SqlBundleEntity> Bundles { get; private set; } = new();
    public List<SqlClipEntity> Clips { get; private set; } = new();
    public List<SqlPluClipFkEntity> PlusClipsFks { get; private set; } = new();
    public List<SqlPluFkEntity> PlusFks { get; private set; } = new();
    public List<SqlPluNestingFkEntity> PlusNestingFks { get; private set; } = new();
    public List<SqlPluEntity> Plus { get; private set; } = new();
    public List<SqlProductionSiteEntity> Areas { get; private set; } = new();
    public List<SqlScaleEntity> Lines { get; private set; } = new();
    public List<SqlWorkShopEntity> WorkShops { get; private set; } = new();

    #endregion

    #region Public and private fields, properties, constructor - Глобальный кэш представлений

    public List<SqlViewPluLineModel> ViewPlusLines { get; private set; } = new();
    public List<SqlViewPluStorageMethodModel> ViewPlusStorageMethods { get; private set; } = new();
    public List<SqlViewPluNestingModel> ViewPlusNesting { get; set; } = new();

    #endregion

    #region Public and private fields, properties, constructor - Локальный кэш представлений

    public List<SqlViewPluLineModel> LocalViewPlusLines { get; private set; } = new();

    #endregion

    #region Public and private methods - Глобальный кэш

    /// <summary>
    /// Загрузить кэш по-умному.
    /// Умная оптимизация на базе сравнения представления кол-ва строк таблиц.
    /// </summary>
    public void SmartLoad()
    {
        List<SqlViewTableSizeModel> tableSize = ViewTableSizeRepository.GetEnumerable(SqlCrudConfig).ToList();
        SqlViewTableSizeModel? table;
        
        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.Boxes));
        if (Boxes.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Boxes.Count))
            Boxes = BoxRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.Bundles));
        if (Bundles.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Bundles.Count))
            Bundles = BundleRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.Clips));
        if (Clips.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Clips.Count))
            Clips = ClipRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.PlusClipsFks));
        if (PlusClipsFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusClipsFks.Count))
            PlusClipsFks = PluClipFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.PlusFks));
        if (PlusFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusFks.Count))
            PlusFks = PluFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.PlusNestingFks));
        if (PlusNestingFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusNestingFks.Count))
            PlusNestingFks = PluNestingFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.Plus));
        if (Plus.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Plus.Count))
            Plus = PluRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.ProductionSites));
        if (Areas.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Areas.Count))
            Areas = ProductionSiteRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.WorkShops));
        if (WorkShops.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)WorkShops.Count))
            WorkShops = WorkShopRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(SqlTablesUtils.Scales));
        if (Lines.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Lines.Count))
            Lines = LineRepository.GetEnumerable(SqlCrudConfig).ToList();
    }

    /// <summary>
    /// Загрузить кэш.
    /// </summary>
    public void Load() => Load(TableName);

    /// <summary>
    /// Загрузить кэш.
    /// </summary>
    public void Load(SqlEnumTableName tableName)
    {
        // Загрузить кэш по-умному.
        if (tableName.Equals(SqlEnumTableName.None))
            SmartLoad();

        // Таблицы.
        if (!Areas.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Areas))
            Areas = ProductionSiteRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!WorkShops.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.WorkShops))
            WorkShops = WorkShopRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Plus.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Plus))
            Plus = PluRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Lines.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Lines))
            Lines = LineRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusFks.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.PluFks))
            PlusFks = PluFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Boxes.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Boxes))
            Boxes = BoxRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Bundles.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Bundles))
            Bundles = BundleRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Clips.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Clips))
            Clips = ClipRepository.GetEnumerable(SqlCrudConfig).ToList();
        //if (!DeviceSettings.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.DeviceSettings))
        //    DeviceSettings = DeviceSettingsRepository.GetEnumerable(SqlCrudConfig).ToList();
        //if (!DeviceSettingsFks.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.DeviceSettingsFks))
        //    DeviceSettingsFks = DeviceSettingFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusClipsFks.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.PluClipsFks))
            PlusClipsFks = new SqlPluClipFkRepository().GetEnumerable(SqlCrudConfig).ToList();
        
        if (!PlusNestingFks.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.PlusNestingFks))
            PlusNestingFks = PluNestingFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        // Представления.
        if (!ViewPlusLines.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.ViewPlusLines))
            ViewPlusLines = PluLineRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!ViewPlusStorageMethods.Any() || Equals(tableName, SqlEnumTableName.All) ||
            Equals(tableName, SqlEnumTableName.ViewPlusStorageMethods))
            ViewPlusStorageMethods = ViewPluStorageMethodRepository.GetList(SqlCrudConfig);
        if (!ViewPlusNesting.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.ViewPlusNesting))
            ViewPlusNesting = ViewPluNestingFkRepository.GetEnumerable().ToList();

        // Оптимизация.
        if (TableName.Equals(SqlEnumTableName.All))
            TableName = SqlEnumTableName.None;
    }

    /// <summary>
    /// Загрузить кэш.
    /// </summary>
    public async Task LoadAsync(SqlEnumTableName tableName)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        Load(tableName);
    }

    /// <summary>
    /// Обновить глобальный кэш.
    /// </summary>
    public void LoadGlobal()
    {
        Load(SqlEnumTableName.Areas);
        Load(SqlEnumTableName.WorkShops);
        Load(SqlEnumTableName.Lines);
        Load(SqlEnumTableName.ViewPlusNesting);
        Load(SqlEnumTableName.ViewPlusStorageMethods);
    }
    
    #endregion

    #region Public and private methods - Локальный кэш

    public void LoadLocalViewPlusLines(ushort scaleId)
    {
        LocalViewPlusLines = PluLineRepository.GetEnumerable(scaleId).ToList();
    }

    public List<SqlViewPluLineModel> GetCurrentViewPlusScales(int pageNumber, ushort pageSize) =>
        LocalViewPlusLines.Where(item => item.IsActive)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToList();

    /// <summary>
    /// Очистить кеш.
    /// </summary>
    public void Clear()
    {
        // Таблицы.
        Areas.Clear();
        Boxes.Clear();
        Bundles.Clear();
        Clips.Clear();
        Lines.Clear();
        Plus.Clear();
        PlusClipsFks.Clear();
        PlusFks.Clear();
        PlusNestingFks.Clear();
        WorkShops.Clear();
        ViewPlusLines.Clear();
        ViewPlusNesting.Clear();
        ViewPlusStorageMethods.Clear();
    }

    #endregion
}