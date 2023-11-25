namespace Ws.StorageCore.Helpers;

[Obsolete("Will be deleted soon")]
public sealed class SqlContextCacheHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlContextCacheHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlContextCacheHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor - Инициализация репозиториев
    
    private SqlViewPluNestingRepository ViewPluNestingFkRepository { get; } = new();
    private SqlViewPluLineRepository PluLineRepository { get; } = new();
    private SqlViewTableSizeRepository ViewTableSizeRepository { get; } = new();
    private SqlWorkShopRepository WorkShopRepository { get; } = new();
    private SqlProductionSiteRepository ProductionSiteRepository { get; } = new();
    private SqlLineRepository LineRepository  { get; } = new();

    #endregion
    
    #region Public and private fields, properties, constructor - Глобальный кэш таблиц
    
    private static SqlCrudConfigModel SqlCrudConfig => SqlCrudConfigFactory.GetCrudAll();
    public List<SqlProductionSiteEntity> Areas { get; private set; } = new();
    public List<SqlLineEntity> Lines { get; private set; } = new();
    public List<SqlWorkShopEntity> WorkShops { get; private set; } = new();

    #endregion
    
    public List<SqlViewPluNestingModel> ViewPlusNesting { get; set; } = new();
    
    #region Public and private fields, properties, constructor - Локальный кэш представлений

    public List<SqlViewPluLineModel> LocalViewPlusLines { get; private set; } = new();

    #endregion

    #region Public and private methods - Глобальный кэш

    /// <summary>
    /// Загрузить кэш по-умному.
    /// Умная оптимизация на базе сравнения представления кол-ва строк таблиц.
    /// </summary>
    private void SmartLoad()
    {
        List<SqlViewTableSizeModel> tableSize = ViewTableSizeRepository.GetEnumerable(SqlCrudConfig).ToList();
        SqlViewTableSizeModel? table;

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
        if (!Lines.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.Lines))
            Lines = LineRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!ViewPlusNesting.Any() || Equals(tableName, SqlEnumTableName.All) || Equals(tableName, SqlEnumTableName.ViewPlusNesting))
            ViewPlusNesting = ViewPluNestingFkRepository.GetEnumerable().ToList();
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

    #endregion
}