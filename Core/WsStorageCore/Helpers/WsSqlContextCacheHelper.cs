// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;

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

    #region Public and private fields, properties, constructor - Инициализация репозиториев
    
    private WsSqlViewPluLineRepository PluLineRepository { get; } = new();
    private WsSqlViewPluNestingRepository ViewPluNestingFkRepository { get; } = new();
    private WsSqlViewPluStorageMethodRepository ViewPluStorageMethodRepository { get; } = new();
    private WsSqlViewTableSizeRepository ViewTableSizeRepository { get; } = new();
    private WsSqlBoxRepository BoxRepository { get; } = new();
    private WsSqlBrandRepository BrandRepository { get; } = new();
    private WsSqlWorkShopRepository WorkShopRepository { get; } = new();
    private WsSqlPluGroupRepository PluGroupRepository { get; } = new();
    private WsSqlPluGroupFkRepository PluGroupFkRepository { get; } = new();
    private WsSqlPlu1CRepository Plu1CRepository { get; } = new();
    private WsSqlAreaRepository AreaRepository { get; } = new();
    private WsSqlBundleRepository BundleRepository  { get; } = new();
    private WsSqlLineRepository LineRepository  { get; } = new();
    private WsSqlPluClipFkRepository PluClipFkRepository { get; } = new();
    private WsSqlClipRepository ClipRepository { get; } = new();
    private WsSqlDeviceSettingsRepository DeviceSettingsRepository { get; } = new();
    private WsSqlDeviceSettingsFkRepository DeviceSettingFkRepository { get; } = new();
    private WsSqlPluRepository PluRepository { get; } = new();
    private WsSqlPluFkRepository PluFkRepository { get; } = new();
    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();
    private WsSqlPluCharacteristicRepository PluCharacteristicRepository { get; } = new();
    private WsSqlPluCharacteristicsFkRepository PluCharacteristicFkRepository { get; } = new();
    private WsSqlPluBundleFkRepository PluBundleFkRepository { get; } = new();
    private WsSqlPluBrandFkRepository PluBrandFkRepository { get; } = new();

    #endregion
    
    #region Public and private fields, properties, constructor - Глобальный кэш таблиц
    
    private WsSqlCrudConfigModel SqlCrudConfig => WsSqlCrudConfigFactory.GetCrudAll();
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlEnumTableName TableName { get; set; } = WsSqlEnumTableName.None;
    public List<WsSqlBoxModel> Boxes { get; private set; } = new();
    public List<WsSqlBrandModel> Brands { get; private set; } = new();
    public List<WsSqlBundleModel> Bundles { get; private set; } = new();
    public List<WsSqlClipModel> Clips { get; private set; } = new();
    public List<WsSqlDeviceSettingsModel> DeviceSettings { get; set; } = new();
    public List<WsSqlDeviceSettingsFkModel> DeviceSettingsFks { get; set; } = new();
    public List<WsSqlPlu1CFkModel> Plus1CFks { get; private set; } = new();
    public List<WsSqlPluBrandFkModel> PlusBrandsFks { get; private set; } = new();
    public List<WsSqlPluBundleFkModel> PlusBundlesFks { get; private set; } = new();
    public List<WsSqlPluCharacteristicModel> PlusCharacteristics { get; private set; } = new();
    public List<WsSqlPluCharacteristicsFkModel> PlusCharacteristicsFks { get; private set; } = new();
    public List<WsSqlPluClipFkModel> PlusClipsFks { get; private set; } = new();
    public List<WsSqlPluFkModel> PlusFks { get; private set; } = new();
    public List<WsSqlPluGroupFkModel> PlusGroupsFks { get; private set; } = new();
    public List<WsSqlPluNestingFkModel> PlusNestingFks { get; private set; } = new();
    public List<WsSqlPluGroupModel> PlusGroups { get; private set; } = new();
    public List<WsSqlPluModel> Plus { get; private set; } = new();
    public List<WsSqlProductionFacilityModel> Areas { get; private set; } = new();
    public List<WsSqlScaleModel> Lines { get; private set; } = new();
    public List<WsSqlWorkShopModel> WorkShops { get; private set; } = new();

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
    /// Загрузить кэш по-умному.
    /// Умная оптимизация на базе сравнения представления кол-ва строк таблиц.
    /// </summary>
    public void SmartLoad()
    {
        List<WsSqlViewTableSizeModel> tableSize = ViewTableSizeRepository.GetEnumerable(SqlCrudConfig).ToList();
        WsSqlViewTableSizeModel? table;
        
        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Boxes));
        if (Boxes.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Boxes.Count))
            Boxes = BoxRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Brands));
        if (Brands.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Brands.Count))
            Brands = BrandRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Bundles));
        if (Bundles.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Bundles.Count))
            Bundles = BundleRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Clips));
        if (Clips.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Clips.Count))
            Clips = ClipRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.DeviceSettings));
        if (DeviceSettings.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)DeviceSettings.Count))
            DeviceSettings = DeviceSettingsRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.DeviceSettingsFks));
        if (DeviceSettingsFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)DeviceSettingsFks.Count))
            DeviceSettingsFks = DeviceSettingFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Plus1CFks));
        if (Plus1CFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Plus1CFks.Count))
            Plus1CFks = Plu1CRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusBrandsFks));
        if (PlusBrandsFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusBrandsFks.Count))
            PlusBrandsFks = PluBrandFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusBundlesFks));
        if (PlusBundlesFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusBundlesFks.Count))
            PlusBundlesFks = PluBundleFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusCharacteristics));
        if (PlusCharacteristics.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusCharacteristics.Count))
            PlusCharacteristics = PluCharacteristicRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusCharacteristicsFks));
        if (PlusCharacteristicsFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusCharacteristicsFks.Count))
            PlusCharacteristicsFks = PluCharacteristicFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusClipsFks));
        if (PlusClipsFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusClipsFks.Count))
            PlusClipsFks = PluClipFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusFks));
        if (PlusFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusFks.Count))
            PlusFks = PluFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusGroupsFks));
        if (PlusGroupsFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusGroupsFks.Count))
            PlusGroupsFks = PluGroupFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusNestingFks));
        if (PlusNestingFks.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusNestingFks.Count))
            PlusNestingFks = PluNestingFkRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.PlusGroups));
        if (PlusGroups.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)PlusGroups.Count))
            PlusGroups = PluGroupRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Plus));
        if (Plus.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Plus.Count))
            Plus = PluRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.ProductionFacilities));
        if (Areas.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)Areas.Count))
            Areas = AreaRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.WorkShops));
        if (WorkShops.Count.Equals(0) || table is not null && !table.RowsCount.Equals((uint)WorkShops.Count))
            WorkShops = WorkShopRepository.GetEnumerable(SqlCrudConfig).ToList();

        table = tableSize.Find(item => item.Table.Equals(WsSqlTablesUtils.Scales));
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
    public void Load(WsSqlEnumTableName tableName)
    {
        // Загрузить кэш по-умному.
        if (tableName.Equals(WsSqlEnumTableName.None))
            SmartLoad();

        // Таблицы.
        if (!Areas.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Areas))
            Areas = AreaRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!WorkShops.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.WorkShops))
            WorkShops = WorkShopRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Plus.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Plus))
            Plus = PluRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Lines.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Lines))
            Lines = LineRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluFks))
            PlusFks = PluFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Boxes.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Boxes))
            Boxes = BoxRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Bundles.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Bundles))
            Bundles = BundleRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusBundlesFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluBundlesFks))
            PlusBundlesFks = PluBundleFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusBrandsFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluBrandsFks))
            PlusBrandsFks = PluBrandFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Clips.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Clips))
            Clips = ClipRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!DeviceSettings.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.DeviceSettings))
            DeviceSettings = DeviceSettingsRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!DeviceSettingsFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.DeviceSettingsFks))
            DeviceSettingsFks = DeviceSettingFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusClipsFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluClipsFks))
            PlusClipsFks = ContextManager.PlusClipFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Plus1CFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Plus1CFks))
            Plus1CFks = ContextManager.Plu1CRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusCharacteristics.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluCharacteristics))
            PlusCharacteristics = PluCharacteristicRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusCharacteristicsFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluCharacteristicsFks))
            PlusCharacteristicsFks = PluCharacteristicFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusGroups.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluGroups))
            PlusGroups = PluGroupRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusGroupsFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PluGroupsFks))
            PlusGroupsFks = PluGroupFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!PlusNestingFks.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.PlusNestingFks))
            PlusNestingFks = PluNestingFkRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!Brands.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.Brands))
            Brands = BrandRepository.GetEnumerable(SqlCrudConfig).ToList();

        // Представления.
        if (!ViewPlusLines.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.ViewPlusLines))
            ViewPlusLines = PluLineRepository.GetEnumerable(SqlCrudConfig).ToList();
        if (!ViewPlusStorageMethods.Any() || Equals(tableName, WsSqlEnumTableName.All) ||
            Equals(tableName, WsSqlEnumTableName.ViewPlusStorageMethods))
            ViewPlusStorageMethods = ViewPluStorageMethodRepository.GetList(SqlCrudConfig);
        if (!ViewPlusNesting.Any() || Equals(tableName, WsSqlEnumTableName.All) || Equals(tableName, WsSqlEnumTableName.ViewPlusNesting))
            ViewPlusNesting = ViewPluNestingFkRepository.GetEnumerable().ToList();

        // Оптимизация.
        if (TableName.Equals(WsSqlEnumTableName.All))
            TableName = WsSqlEnumTableName.None;
    }

    /// <summary>
    /// Загрузить кэш.
    /// </summary>
    public async Task LoadAsync(WsSqlEnumTableName tableName)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        Load(tableName);
    }

    /// <summary>
    /// Обновить глобальный кэш.
    /// </summary>
    public void LoadGlobal()
    {
        Load(WsSqlEnumTableName.Areas);
        Load(WsSqlEnumTableName.WorkShops);
        Load(WsSqlEnumTableName.Lines);
        Load(WsSqlEnumTableName.ViewPlusNesting);
        Load(WsSqlEnumTableName.ViewPlusStorageMethods);
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
        LocalViewPlusLines = PluLineRepository.GetEnumerable(scaleId).ToList();
    }

    public List<WsSqlViewPluLineModel> GetCurrentViewPlusScales(int pageNumber, ushort pageSize) =>
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
        Brands.Clear();
        Bundles.Clear();
        Clips.Clear();
        DeviceSettings.Clear();
        DeviceSettingsFks.Clear();
        Lines.Clear();
        Plus.Clear();
        Plus1CFks.Clear();
        PlusBrandsFks.Clear();
        PlusBundlesFks.Clear();
        PlusCharacteristics.Clear();
        PlusCharacteristicsFks.Clear();
        PlusClipsFks.Clear();
        PlusFks.Clear();
        PlusGroups.Clear();
        PlusGroupsFks.Clear();
        PlusNestingFks.Clear();
        WorkShops.Clear();
        // Представления.
        ViewPlusLines.Clear();
        ViewPlusNesting.Clear();
        ViewPlusStorageMethods.Clear();
    }

    /// <summary>
    /// Очистить кеш.
    /// </summary>
    public void Clear(WsSqlEnumTableName tableName)
    {
        switch (tableName)
        {
            case WsSqlEnumTableName.None:
                break;
            case WsSqlEnumTableName.All:
                Clear();
                break;
            // Таблицы.
            case WsSqlEnumTableName.Areas:
                Areas.Clear();
                break;
            case WsSqlEnumTableName.Boxes:
                Boxes.Clear();
                break;
            case WsSqlEnumTableName.Brands:
                Brands.Clear();
                break;
            case WsSqlEnumTableName.Bundles:
                Bundles.Clear();
                break;
            case WsSqlEnumTableName.Clips:
                Clips.Clear();
                break;
            case WsSqlEnumTableName.DeviceSettings:
                DeviceSettings.Clear();
                break;
            case WsSqlEnumTableName.DeviceSettingsFks:
                DeviceSettingsFks.Clear();
                break;
            case WsSqlEnumTableName.Lines:
                Lines.Clear();
                break;
            case WsSqlEnumTableName.PluBrandsFks:
                PlusBrandsFks.Clear();
                break;
            case WsSqlEnumTableName.PluBundlesFks:
                PlusBundlesFks.Clear();
                break;
            case WsSqlEnumTableName.PluCharacteristics:
                PlusCharacteristics.Clear();
                break;
            case WsSqlEnumTableName.PluCharacteristicsFks:
                PlusCharacteristicsFks.Clear();
                break;
            case WsSqlEnumTableName.PluClipsFks:
                PlusClipsFks.Clear();
                break;
            case WsSqlEnumTableName.PluFks:
                PlusFks.Clear();
                break;
            case WsSqlEnumTableName.PluGroups:
                PlusGroups.Clear();
                break;
            case WsSqlEnumTableName.PluGroupsFks:
                PlusGroupsFks.Clear();
                break;
            case WsSqlEnumTableName.PlusNestingFks:
                PlusNestingFks.Clear();
                break;
            case WsSqlEnumTableName.Plus:
                Plus.Clear();
                break;
            case WsSqlEnumTableName.Plus1CFks:
                Plus1CFks.Clear();
                break;
            case WsSqlEnumTableName.WorkShops:
                WorkShops.Clear();
                break;
            // Представления.
            case WsSqlEnumTableName.ViewPlusLines:
                ViewPlusLines.Clear();
                break;
            case WsSqlEnumTableName.ViewPlusNesting:
                ViewPlusNesting.Clear();
                break;
            case WsSqlEnumTableName.ViewPlusStorageMethods:
                ViewPlusStorageMethods.Clear();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tableName), tableName, null);
        }
    }

    #endregion
}