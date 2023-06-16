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

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    #endregion

    #region Public and private methods

    public T? GetItemNullable<T>(object? value) where T : WsSqlTableBase, new() => SqlCore.GetItemNullable<T>(value);

    [Obsolete(@"Use GetItemNotNullable(SqlFieldIdentityModel) or GetItemNullableByUid(Guid?) or GetItemNullableById(long?)")]
    public T GetItemNotNullable<T>(object? value) where T : WsSqlTableBase, new() => SqlCore.GetItemNotNullable<T>(value);

    public T? GetItemNullable<T>(WsSqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNullable<T>(identity);

    public T GetItemNotNullable<T>(WsSqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNotNullable<T>(identity);

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNullableByUid<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNotNullableByUid<T>(uid);

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNullableById<T>(id);

    public T GetItemNotNullableById<T>(long id) where T : WsSqlTableBase, new() =>
        SqlCore.GetItemNotNullableById<T>(id);

    /// <summary>
    /// List of tables models.
    /// </summary>
    /// <returns></returns>
    public List<WsSqlTableBase> GetTableModels() => new()
    {
        new WsSqlAccessModel(),
        new WsSqlAppModel(),
        new WsSqlBarCodeModel(),
        new WsSqlBoxModel(),
        new WsSqlBrandModel(),
        new WsSqlBundleModel(),
        new WsSqlClipModel(),
        new WsSqlContragentModel(),
        new WsSqlDeviceModel(),
        new WsSqlDeviceScaleFkModel(),
        new WsSqlDeviceTypeFkModel(),
        new WsSqlDeviceTypeModel(),
        new WsSqlLogModel(),
        new WsSqlLogTypeModel(),
        new WsSqlLogWebModel(),
        new WsSqlLogWebFkModel(),
        new WsSqlOrderModel(),
        new WsSqlOrderWeighingModel(),
        new WsSqlOrganizationModel(),
        new WsSqlPluBundleFkModel(),
        new WsSqlPluCharacteristicModel(),
        new WsSqlPluCharacteristicsFkModel(),
        new WsSqlPluFkModel(),
        new WsSqlPluGroupFkModel(),
        new WsSqlPluGroupModel(),
        new WsSqlPluLabelModel(),
        new WsSqlPluModel(),
        new WsSqlPluScaleModel(),
        new WsSqlPluTemplateFkModel(),
        new WsSqlPluWeighingModel(),
        new WsSqlPrinterModel(),
        new WsSqlPrinterResourceFkModel(),
        new WsSqlPrinterTypeModel(),
        new WsSqlProductionFacilityModel(),
        new WsSqlProductSeriesModel(),
        new WsSqlScaleModel(),
        new WsSqlScaleScreenShotModel(),
        new WsSqlTaskModel(),
        new WsSqlTaskTypeModel(),
        new WsSqlTemplateModel(),
        new WsSqlTemplateResourceModel(),
        new WsSqlVersionModel(),
        new WsSqlWorkShopModel(),
    };

    /// <summary>
    /// List of tables types.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableTypes() => new()
    {
        typeof(WsSqlAccessModel),
        typeof(WsSqlAppModel),
        typeof(WsSqlBarCodeModel),
        typeof(WsSqlBoxModel),
        typeof(WsSqlBrandModel),
        typeof(WsSqlBundleModel),
        typeof(WsSqlContragentModel),
        typeof(WsSqlDeviceModel),
        typeof(WsSqlDeviceScaleFkModel),
        typeof(WsSqlDeviceTypeFkModel),
        typeof(WsSqlDeviceTypeModel),
        typeof(WsSqlLogModel),
        typeof(WsSqlLogTypeModel),
        typeof(WsSqlLogWebModel),
        typeof(WsSqlLogWebFkModel),
        typeof(WsSqlOrderModel),
        typeof(WsSqlOrderWeighingModel),
        typeof(WsSqlOrganizationModel),
        typeof(WsSqlPluBundleFkModel),
        typeof(WsSqlPluCharacteristicModel),
        typeof(WsSqlPluCharacteristicsFkModel),
        typeof(WsSqlPluFkModel),
        typeof(WsSqlPluGroupFkModel),
        typeof(WsSqlPluGroupModel),
        typeof(WsSqlPluLabelModel),
        typeof(WsSqlPluModel),
        typeof(WsSqlPluScaleModel),
        typeof(WsSqlPluTemplateFkModel),
        typeof(WsSqlPluWeighingModel),
        typeof(WsSqlPrinterModel),
        typeof(WsSqlPrinterResourceFkModel),
        typeof(WsSqlPrinterTypeModel),
        typeof(WsSqlProductionFacilityModel),
        typeof(WsSqlProductSeriesModel),
        typeof(WsSqlScaleModel),
        typeof(WsSqlScaleScreenShotModel),
        typeof(WsSqlTaskModel),
        typeof(WsSqlTaskTypeModel),
        typeof(WsSqlTemplateModel),
        typeof(WsSqlTemplateResourceModel),
        typeof(WsSqlVersionModel),
        typeof(WsSqlWorkShopModel),
    };

    /// <summary>
    /// List of tables mappings.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableMaps() => new()
    {
        typeof(WsSqlAccessMap),
        typeof(WsSqlAppMap),
        typeof(WsSqlBarCodeMap),
        typeof(WsSqlBoxMap),
        typeof(WsSqlBrandMap),
        typeof(WsSqlBundleMap),
        typeof(WsSqlClipMap),
        typeof(WsSqlContragentMap),
        typeof(WsSqlDeviceMap),
        typeof(WsSqlDeviceScaleFkMap),
        typeof(WsSqlDeviceTypeFkMap),
        typeof(WsSqlDeviceTypeMap),
        typeof(WsSqlLogMap),
        typeof(WsSqlLogTypeMap),
        typeof(WsSqlLogWebMap),
        typeof(WsSqlLogWebFkMap),
        typeof(WsSqlOrderMap),
        typeof(WsSqlOrderWeighingMap),
        typeof(WsSqlOrganizationMap),
        typeof(WsSqlPluBrandFkMap),
        typeof(WsSqlPluBundleFkMap),
        typeof(WsSqlPluCharacteristicMap),
        typeof(WsSqlPluCharacteristicsFkMap),
        typeof(WsSqlPluFkMap),
        typeof(WsSqlPluGroupFkMap),
        typeof(WsSqlPluGroupMap),
        typeof(WsSqlPluLabelMap),
        typeof(WsSqlPluMap),
        typeof(WsSqlPluScaleMap),
        typeof(WsSqlPluTemplateFkMap),
        typeof(WsSqlPluWeighingMap),
        typeof(WsSqlPrinterMap),
        typeof(WsSqlPrinterResourceFkMap),
        typeof(WsSqlPrinterTypeMap),
        typeof(WsSqlProductionFacilityMap),
        typeof(WsSqlProductSeriesMap),
        typeof(WsSqlScaleMap),
        typeof(WsSqlScaleScreenShotMap),
        typeof(WsSqlTaskMap),
        typeof(WsSqlTaskTypeMap),
        typeof(WsSqlTemplateMap),
        typeof(WsSqlTemplateResourceMap),
        typeof(WsSqlVersionMap),
        typeof(WsSqlWorkshopMap),
    };

    /// <summary>
    /// List of tables validators.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableValidators() => new()
    {
        typeof(WsSqlAccessValidator),
        typeof(WsSqlAppValidator),
        typeof(WsSqlBarCodeValidator),
        typeof(WsSqlBoxValidator),
        typeof(WsSqlBrandValidator),
        typeof(WsSqlBundleValidator),
        typeof(WsSqlClipValidator),
        typeof(WsSqlContragentValidator),
        typeof(WsSqlDeviceScaleFkValidator),
        typeof(WsSqlDeviceTypeFkValidator),
        typeof(WsSqlDeviceTypeValidator),
        typeof(WsSqlDeviceValidator),
        typeof(WsSqlLogValidator),
        typeof(WsSqlLogTypeValidator),
        typeof(WsSqlLogWebValidator),
        typeof(WsSqlLogWebFkValidator),
        typeof(WsSqlOrderValidator),
        typeof(WsSqlOrderWeighingValidator),
        typeof(WsSqlOrganizationValidator),
        typeof(WsSqlPluBundleFkValidator),
        typeof(WsSqlPluCharacteristicsFkValidator),
        typeof(WsSqlPluCharacteristicValidator),
        typeof(WsSqlPluFkValidator),
        typeof(WsSqlPluGroupFkValidator),
        typeof(WsSqlPluGroupValidator),
        typeof(WsSqlPluLabelValidator),
        typeof(WsSqlPluScaleValidator),
        typeof(WsSqlPluTemplateFkValidator),
        typeof(WsSqlPluValidator),
        typeof(WsSqlPluWeighingValidator),
        typeof(WsSqlPrinterResourceFkValidator),
        typeof(WsSqlPrinterTypeValidator),
        typeof(WsSqlPrinterValidator),
        typeof(WsSqlProductionFacilityValidator),
        typeof(WsSqlProductSeriesValidator),
        typeof(WsSqlScaleScreenShotValidator),
        typeof(WsSqlScaleValidator),
        typeof(WsSqlTaskTypeValidator),
        typeof(WsSqlTaskValidator),
        typeof(WsSqlTemplateResourceValidator),
        typeof(WsSqlTemplateValidator),
        typeof(WsSqlVersionValidator),
        typeof(WsSqlWorkShopValidator),
    };

    public string GetTableModelName<T>() where T : WsSqlTableBase, new()
    {
        return typeof(T) switch
        {
            var cls when cls == typeof(WsSqlAccessModel) => nameof(WsSqlAccessModel),
            var cls when cls == typeof(WsSqlAppModel) => nameof(WsSqlAppModel),
            var cls when cls == typeof(WsSqlBarCodeModel) => nameof(WsSqlBarCodeModel),
            var cls when cls == typeof(WsSqlBoxModel) => nameof(WsSqlBoxModel),
            var cls when cls == typeof(WsSqlBrandModel) => nameof(WsSqlBrandModel),
            var cls when cls == typeof(WsSqlBundleModel) => nameof(WsSqlBundleModel),
            var cls when cls == typeof(WsSqlClipModel) => nameof(WsSqlClipModel),
            var cls when cls == typeof(WsSqlContragentModel) => nameof(WsSqlContragentModel),
            var cls when cls == typeof(WsSqlDeviceModel) => nameof(WsSqlDeviceModel),
            var cls when cls == typeof(WsSqlDeviceScaleFkModel) => nameof(WsSqlDeviceScaleFkModel),
            var cls when cls == typeof(WsSqlDeviceTypeFkModel) => nameof(WsSqlDeviceTypeFkModel),
            var cls when cls == typeof(WsSqlDeviceTypeModel) => nameof(WsSqlDeviceTypeModel),
            var cls when cls == typeof(WsSqlLogModel) => nameof(WsSqlLogModel),
            var cls when cls == typeof(WsSqlLogTypeModel) => nameof(WsSqlLogTypeModel),
            var cls when cls == typeof(WsSqlLogWebModel) => nameof(WsSqlLogWebModel),
            var cls when cls == typeof(WsSqlLogWebFkModel) => nameof(WsSqlLogWebFkModel),
            var cls when cls == typeof(WsSqlOrderModel) => nameof(WsSqlOrderModel),
            var cls when cls == typeof(WsSqlOrderWeighingModel) => nameof(WsSqlOrderWeighingModel),
            var cls when cls == typeof(WsSqlOrganizationModel) => nameof(WsSqlOrganizationModel),
            var cls when cls == typeof(WsSqlPluBundleFkModel) => nameof(WsSqlPluBundleFkModel),
            var cls when cls == typeof(WsSqlPluCharacteristicModel) => nameof(WsSqlPluCharacteristicModel),
            var cls when cls == typeof(WsSqlPluCharacteristicsFkModel) => nameof(WsSqlPluCharacteristicsFkModel),
            var cls when cls == typeof(WsSqlPluFkModel) => nameof(WsSqlPluFkModel),
            var cls when cls == typeof(WsSqlPluGroupFkModel) => nameof(WsSqlPluGroupFkModel),
            var cls when cls == typeof(WsSqlPluGroupModel) => nameof(WsSqlPluGroupModel),
            var cls when cls == typeof(WsSqlPluLabelModel) => nameof(WsSqlPluLabelModel),
            var cls when cls == typeof(WsSqlPluModel) => nameof(WsSqlPluModel),
            var cls when cls == typeof(WsSqlPluScaleModel) => nameof(WsSqlPluScaleModel),
            var cls when cls == typeof(WsSqlPluTemplateFkModel) => nameof(WsSqlPluTemplateFkModel),
            var cls when cls == typeof(WsSqlPluWeighingModel) => nameof(WsSqlPluWeighingModel),
            var cls when cls == typeof(WsSqlPrinterModel) => nameof(WsSqlPrinterModel),
            var cls when cls == typeof(WsSqlPrinterResourceFkModel) => nameof(WsSqlPrinterResourceFkModel),
            var cls when cls == typeof(WsSqlPrinterTypeModel) => nameof(WsSqlPrinterTypeModel),
            var cls when cls == typeof(WsSqlProductionFacilityModel) => nameof(WsSqlProductionFacilityModel),
            var cls when cls == typeof(WsSqlProductSeriesModel) => nameof(WsSqlProductSeriesModel),
            var cls when cls == typeof(WsSqlScaleModel) => nameof(WsSqlScaleModel),
            var cls when cls == typeof(WsSqlScaleScreenShotModel) => nameof(WsSqlScaleScreenShotModel),
            var cls when cls == typeof(WsSqlTaskModel) => nameof(WsSqlTaskModel),
            var cls when cls == typeof(WsSqlTaskTypeModel) => nameof(WsSqlTaskTypeModel),
            var cls when cls == typeof(WsSqlTemplateModel) => nameof(WsSqlTemplateModel),
            var cls when cls == typeof(WsSqlTemplateResourceModel) => nameof(WsSqlTemplateResourceModel),
            var cls when cls == typeof(WsSqlVersionModel) => nameof(WsSqlVersionModel),
            var cls when cls == typeof(WsSqlWorkShopModel) => nameof(WsSqlWorkShopModel),
            _ => string.Empty
        };
    }

    /// <summary>
    /// Get list of db files infos.
    /// </summary>
    /// <returns></returns>
    public List<WsSqlDbFileSizeInfoModel> GetDbFileSizeInfos()
    {
        List<WsSqlDbFileSizeInfoModel> result = new();
        object[] objects = SqlCore.GetArrayObjectsNotNullable(WsSqlQueriesSystem.Properties.GetDbFileSizes);
        foreach (object obj in objects)
        {
            if (obj is object[] { Length: 4 } item)
            {
                result.Add(new(
                    Convert.ToByte(item[0]), 
                    Convert.ToString(item[1]),
                    Convert.ToUInt16(item[2]), 
                    Convert.ToUInt16(item[3]))
                );
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