// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник самих данных.
/// Клиентский слой доступа к БД.
/// </summary>
internal sealed class WsSqlContextCoreHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;
    public WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;

    #endregion

    #region Public and private methods

    public T? GetItemNullable<T>(object? value) where T : WsSqlTableBase, new() => AccessCore.GetItemNullable<T>(value);

    [Obsolete(@"Use GetItemNotNullable(SqlFieldIdentityModel) or GetItemNullableByUid(Guid?) or GetItemNullableById(long?)")]
    public T GetItemNotNullable<T>(object? value) where T : WsSqlTableBase, new() => AccessCore.GetItemNotNullable<T>(value);

    public T? GetItemNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullable<T>(identity);

    public T GetItemNotNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullable<T>(identity);

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullableByUid<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullableByUid<T>(uid);

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullableById<T>(id);

    public T GetItemNotNullableById<T>(long id) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullableById<T>(id);

    /// <summary>
    /// List of tables models.
    /// </summary>
    /// <returns></returns>
    public List<WsSqlTableBase> GetTableModels() => new()
    {
        new WsSqlAccessModel(),
        new WsSqlAppModel(),
        new BarCodeModel(),
        new BoxModel(),
        new BrandModel(),
        new BundleModel(),
        new ClipModel(),
        new ContragentModel(),
        new DeviceModel(),
        new WsSqlDeviceScaleFkModel(),
        new WsSqlDeviceTypeFkModel(),
        new DeviceTypeModel(),
        new LogModel(),
        new LogTypeModel(),
        new LogWebModel(),
        new LogWebFkModel(),
        new OrderModel(),
        new OrderWeighingModel(),
        new OrganizationModel(),
        new WsSqlPluBundleFkModel(),
        new PluCharacteristicModel(),
        new WsSqlPluCharacteristicsFkModel(),
        new WsSqlPluFkModel(),
        new WsSqlPluGroupFkModel(),
        new PluGroupModel(),
        new WsSqlPluLabelModel(),
        new WsSqlPluModel(),
        new PluScaleModel(),
        new WsSqlPluTemplateFkModel(),
        new WsSqlPluWeighingModel(),
        new PrinterModel(),
        new WsSqlPrinterResourceFkModel(),
        new PrinterTypeModel(),
        new ProductionFacilityModel(),
        new ProductSeriesModel(),
        new ScaleModel(),
        new ScaleScreenShotModel(),
        new TaskModel(),
        new TaskTypeModel(),
        new TemplateModel(),
        new TemplateResourceModel(),
        new VersionModel(),
        new WorkShopModel(),
    };

    /// <summary>
    /// List of tables types.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableTypes() => new()
    {
        typeof(WsSqlAccessModel),
        typeof(WsSqlAppModel),
        typeof(BarCodeModel),
        typeof(BoxModel),
        typeof(BrandModel),
        typeof(BundleModel),
        typeof(ContragentModel),
        typeof(DeviceModel),
        typeof(WsSqlDeviceScaleFkModel),
        typeof(WsSqlDeviceTypeFkModel),
        typeof(DeviceTypeModel),
        typeof(LogModel),
        typeof(LogTypeModel),
        typeof(LogWebModel),
        typeof(LogWebFkModel),
        typeof(OrderModel),
        typeof(OrderWeighingModel),
        typeof(OrganizationModel),
        typeof(WsSqlPluBundleFkModel),
        typeof(PluCharacteristicModel),
        typeof(WsSqlPluCharacteristicsFkModel),
        typeof(WsSqlPluFkModel),
        typeof(WsSqlPluGroupFkModel),
        typeof(PluGroupModel),
        typeof(WsSqlPluLabelModel),
        typeof(WsSqlPluModel),
        typeof(PluScaleModel),
        typeof(WsSqlPluTemplateFkModel),
        typeof(WsSqlPluWeighingModel),
        typeof(PrinterModel),
        typeof(WsSqlPrinterResourceFkModel),
        typeof(PrinterTypeModel),
        typeof(ProductionFacilityModel),
        typeof(ProductSeriesModel),
        typeof(ScaleModel),
        typeof(ScaleScreenShotModel),
        typeof(TaskModel),
        typeof(TaskTypeModel),
        typeof(TemplateModel),
        typeof(TemplateResourceModel),
        typeof(VersionModel),
        typeof(WorkShopModel),
    };

    /// <summary>
    /// List of tables mappings.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableMaps() => new()
    {
        typeof(WsSqlAccessMap),
        typeof(WsSqlAppMap),
        typeof(BarCodeMap),
        typeof(BoxMap),
        typeof(BrandMap),
        typeof(BundleMap),
        typeof(ClipMap),
        typeof(ContragentMap),
        typeof(DeviceMap),
        typeof(WsSqlDeviceScaleFkMap),
        typeof(WsSqlDeviceTypeFkMap),
        typeof(DeviceTypeMap),
        typeof(LogMap),
        typeof(LogTypeMap),
        typeof(LogWebMap),
        typeof(LogWebFkMap),
        typeof(OrderMap),
        typeof(OrderWeighingMap),
        typeof(OrganizationMap),
        typeof(WsSqlPluBrandFkMap),
        typeof(WsSqlPluBundleFkMap),
        typeof(PluCharacteristicMap),
        typeof(WsSqlPluCharacteristicsFkMap),
        typeof(WsSqlPluFkMap),
        typeof(WsSqlPluGroupFkMap),
        typeof(PluGroupMap),
        typeof(WsSqlPluLabelMap),
        typeof(WsSqlPluMap),
        typeof(PluScaleMap),
        typeof(WsSqlPluTemplateFkMap),
        typeof(WsSqlPluWeighingMap),
        typeof(PrinterMap),
        typeof(WsSqlPrinterResourceFkMap),
        typeof(PrinterTypeMap),
        typeof(ProductionFacilityMap),
        typeof(ProductSeriesMap),
        typeof(ScaleMap),
        typeof(ScaleScreenShotMap),
        typeof(TaskMap),
        typeof(TaskTypeMap),
        typeof(TemplateMap),
        typeof(TemplateResourceMap),
        typeof(VersionMap),
        typeof(WorkShopMap),
    };

    /// <summary>
    /// List of tables validators.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableValidators() => new()
    {
        typeof(WsSqlAccessValidator),
        typeof(WsSqlAppValidator),
        typeof(BarCodeValidator),
        typeof(BoxValidator),
        typeof(BrandValidator),
        typeof(BundleValidator),
        typeof(ClipValidator),
        typeof(ContragentValidator),
        typeof(WsSqlDeviceScaleFkValidator),
        typeof(WsSqlDeviceTypeFkValidator),
        typeof(DeviceTypeValidator),
        typeof(DeviceValidator),
        typeof(LogValidator),
        typeof(LogTypeValidator),
        typeof(LogWebValidator),
        typeof(LogWebFkValidator),
        typeof(OrderValidator),
        typeof(OrderWeighingValidator),
        typeof(OrganizationValidator),
        typeof(WsSqlPluBundleFkValidator),
        typeof(WsSqlPluCharacteristicsFkValidator),
        typeof(PluCharacteristicValidator),
        typeof(WsSqlPluFkValidator),
        typeof(WsSqlPluGroupFkValidator),
        typeof(PluGroupValidator),
        typeof(WsSqlPluLabelValidator),
        typeof(PluScaleValidator),
        typeof(WsSqlPluTemplateFkValidator),
        typeof(WsSqlPluValidator),
        typeof(WsSqlPluWeighingValidator),
        typeof(WsSqlPrinterResourceFkValidator),
        typeof(PrinterTypeValidator),
        typeof(PrinterValidator),
        typeof(ProductionFacilityValidator),
        typeof(ProductSeriesValidator),
        typeof(ScaleScreenShotValidator),
        typeof(ScaleValidator),
        typeof(TaskTypeValidator),
        typeof(TaskValidator),
        typeof(TemplateResourceValidator),
        typeof(TemplateValidator),
        typeof(VersionValidator),
        typeof(WorkShopValidator),
    };

    public string GetTableModelName<T>() where T : WsSqlTableBase, new()
    {
        return typeof(T) switch
        {
            var cls when cls == typeof(WsSqlAccessModel) => nameof(WsSqlAccessModel),
            var cls when cls == typeof(WsSqlAppModel) => nameof(WsSqlAppModel),
            var cls when cls == typeof(BarCodeModel) => nameof(BarCodeModel),
            var cls when cls == typeof(BoxModel) => nameof(BoxModel),
            var cls when cls == typeof(BrandModel) => nameof(BrandModel),
            var cls when cls == typeof(BundleModel) => nameof(BundleModel),
            var cls when cls == typeof(ClipModel) => nameof(ClipModel),
            var cls when cls == typeof(ContragentModel) => nameof(ContragentModel),
            var cls when cls == typeof(DeviceModel) => nameof(DeviceModel),
            var cls when cls == typeof(WsSqlDeviceScaleFkModel) => nameof(WsSqlDeviceScaleFkModel),
            var cls when cls == typeof(WsSqlDeviceTypeFkModel) => nameof(WsSqlDeviceTypeFkModel),
            var cls when cls == typeof(DeviceTypeModel) => nameof(DeviceTypeModel),
            var cls when cls == typeof(LogModel) => nameof(LogModel),
            var cls when cls == typeof(LogTypeModel) => nameof(LogTypeModel),
            var cls when cls == typeof(LogWebModel) => nameof(LogWebModel),
            var cls when cls == typeof(LogWebFkModel) => nameof(LogWebFkModel),
            var cls when cls == typeof(OrderModel) => nameof(OrderModel),
            var cls when cls == typeof(OrderWeighingModel) => nameof(OrderWeighingModel),
            var cls when cls == typeof(OrganizationModel) => nameof(OrganizationModel),
            var cls when cls == typeof(WsSqlPluBundleFkModel) => nameof(WsSqlPluBundleFkModel),
            var cls when cls == typeof(PluCharacteristicModel) => nameof(PluCharacteristicModel),
            var cls when cls == typeof(WsSqlPluCharacteristicsFkModel) => nameof(WsSqlPluCharacteristicsFkModel),
            var cls when cls == typeof(WsSqlPluFkModel) => nameof(WsSqlPluFkModel),
            var cls when cls == typeof(WsSqlPluGroupFkModel) => nameof(WsSqlPluGroupFkModel),
            var cls when cls == typeof(PluGroupModel) => nameof(PluGroupModel),
            var cls when cls == typeof(WsSqlPluLabelModel) => nameof(WsSqlPluLabelModel),
            var cls when cls == typeof(WsSqlPluModel) => nameof(WsSqlPluModel),
            var cls when cls == typeof(PluScaleModel) => nameof(PluScaleModel),
            var cls when cls == typeof(WsSqlPluTemplateFkModel) => nameof(WsSqlPluTemplateFkModel),
            var cls when cls == typeof(WsSqlPluWeighingModel) => nameof(WsSqlPluWeighingModel),
            var cls when cls == typeof(PrinterModel) => nameof(PrinterModel),
            var cls when cls == typeof(WsSqlPrinterResourceFkModel) => nameof(WsSqlPrinterResourceFkModel),
            var cls when cls == typeof(PrinterTypeModel) => nameof(PrinterTypeModel),
            var cls when cls == typeof(ProductionFacilityModel) => nameof(ProductionFacilityModel),
            var cls when cls == typeof(ProductSeriesModel) => nameof(ProductSeriesModel),
            var cls when cls == typeof(ScaleModel) => nameof(ScaleModel),
            var cls when cls == typeof(ScaleScreenShotModel) => nameof(ScaleScreenShotModel),
            var cls when cls == typeof(TaskModel) => nameof(TaskModel),
            var cls when cls == typeof(TaskTypeModel) => nameof(TaskTypeModel),
            var cls when cls == typeof(TemplateModel) => nameof(TemplateModel),
            var cls when cls == typeof(TemplateResourceModel) => nameof(TemplateResourceModel),
            var cls when cls == typeof(VersionModel) => nameof(VersionModel),
            var cls when cls == typeof(WorkShopModel) => nameof(WorkShopModel),
            _ => string.Empty
        };
    }

    /// <summary>
    /// Get list of db files infos.
    /// </summary>
    /// <returns></returns>
    public List<SqlDbFileSizeInfoModel> GetDbFileSizeInfos()
    {
        List<SqlDbFileSizeInfoModel> result = new();
        object[] objects = AccessCore.GetArrayObjectsNotNullable(WsSqlQueriesSystem.Properties.GetDbFileSizes);
        foreach (object obj in objects)
        {
            if (obj is object[] { Length: 4 } item)
            {
                result.Add(new(Convert.ToByte(item[0]), Convert.ToString(item[1]),
                    Convert.ToUInt16(item[2]), Convert.ToUInt16(item[3])));
            }
        }
        return result;
    }

    /// <summary>
    /// Get all db files sizes.
    /// </summary>
    /// <returns></returns>
    public ushort GetDbFileSizeAll() =>
        (ushort)GetDbFileSizeInfos().Sum(item => item.SizeMb);

    #endregion
}