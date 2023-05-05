// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных списков.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextListHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextListHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextListHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;
    private WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;

    public List<WsSqlAccessModel> Accesses { get; set; } = new();
    public List<WsSqlAppModel> Apps { get; set; } = new();
    public List<WsSqlBarCodeModel> BarCodes { get; set; } = new();
    public List<WsSqlBoxModel> Boxes { get; set; } = new();
    public List<WsSqlBrandModel> Brands { get; set; } = new();
    public List<WsSqlBundleModel> Bundles { get; set; } = new();
    public List<WsSqlClipModel> Clips { get; set; } = new();
    public List<WsSqlContragentModel> Contragents { get; set; } = new();
    public List<WsSqlDeviceModel> Devices { get; set; } = new();
    public List<WsSqlDeviceTypeModel> DeviceTypes { get; set; } = new();
    public List<WsSqlDeviceTypeFkModel> DeviceTypeFks { get; set; } = new();
    public List<WsSqlDeviceScaleFkModel> DeviceScaleFks { get; set; } = new();
    public List<WsSqlLogModel> Logs { get; set; } = new();
    public List<WsSqlLogMemoryModel> LogsMemories { get; set; } = new();
    public List<WsSqlLogTypeModel> LogTypes { get; set; } = new();
    public List<WsSqlLogWebModel> LogsWebs { get; set; } = new();
    public List<WsSqlLogWebFkModel> LogsWebsFks { get; set; } = new();
    public List<WsSqlPluGroupModel> NomenclaturesGroups { get; set; } = new();
    public List<WsSqlPluGroupFkModel> NomenclaturesGroupsFk { get; set; } = new();
    public List<WsSqlPluCharacteristicModel> NomenclaturesCharacteristics { get; set; } = new();
    public List<WsSqlPluCharacteristicsFkModel> NomenclaturesCharacteristicsFk { get; set; } = new();
    public List<WsSqlOrderModel> Orders { get; set; } = new();
    public List<WsSqlOrderWeighingModel> OrderWeighings { get; set; } = new();
    public List<WsSqlOrganizationModel> Organizations { get; set; } = new();
    public List<WsSqlPluLabelModel> PluLabels { get; set; } = new();
    public List<WsSqlPluModel> Plus { get; set; } = new();
    public List<WsSqlPluFkModel> PlusFks { get; set; } = new();
    public List<WsSqlPluBrandFkModel> PluBrandFks { get; set; } = new();
    public List<WsSqlPluBundleFkModel> PluBundleFks { get; set; } = new();
    public List<WsSqlPluClipFkModel> PluClipFks { get; set; } = new();
    public List<WsSqlPluNestingFkModel> PluNestingFks { get; set; } = new();
    public List<WsSqlPluScaleModel> PluScales { get; set; } = new();
    public List<WsSqlPluStorageMethodModel> PluStorageMethods { get; set; } = new();
    public List<WsSqlPluStorageMethodFkModel> PluStorageMethodsFks { get; set; } = new();
    public List<WsSqlPluTemplateFkModel> PluTemplateFks { get; set; } = new();
    public List<WsSqlPluWeighingModel> PluWeighings { get; set; } = new();
    public List<WsSqlPrinterModel> Printers { get; set; } = new();
    public List<WsSqlPrinterResourceFkModel> PrinterResources { get; set; } = new();
    public List<WsSqlPrinterTypeModel> PrinterTypes { get; set; } = new();
    public List<WsSqlProductionFacilityModel> ProductionFacilities { get; set; } = new();
    public List<WsSqlProductSeriesModel> ProductSeries { get; set; } = new();
    public List<WsSqlScaleModel> Scales { get; set; } = new();
    public List<WsSqlScaleScreenShotModel> ScaleScreenShots { get; set; } = new();
    public List<WsSqlTaskModel> Tasks { get; set; } = new();
    public List<WsSqlTaskTypeModel> TaskTypes { get; set; } = new();
    public List<WsSqlTemplateModel> Templates { get; set; } = new();
    public List<WsSqlTemplateResourceModel> TemplateResources { get; set; } = new();
    public List<WsSqlVersionModel> Versions { get; set; } = new();
    public List<WsSqlWorkShopModel> WorkShops { get; set; } = new();

    #endregion

    #region Public and private methods

    public List<T> GetListNotNullableCore<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        AccessCore.GetListNotNullable<T>(sqlCrudConfig);
    
    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() => typeof(T) switch
    {
        var cls when cls == typeof(WsSqlBarCodeModel) => GetListNotNullableBarCodes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlBoxModel) => GetListNotNullableBoxes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlBrandModel) => GetListNotNullableBrands(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlBundleModel) => GetListNotNullableBundles(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlClipModel) => GetListNotNullableClips(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlContragentModel) => GetListNotNullableContragents(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlDeviceModel) => GetListNotNullableDevices(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlDeviceScaleFkModel) => GetListNotNullableDeviceScalesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlDeviceTypeFkModel) => GetListNotNullableDeviceTypesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlDeviceTypeModel) => GetListNotNullableDeviceTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlLogMemoryModel) => GetListNotNullableLogsMemories(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlLogModel) => GetListNotNullableLogs(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlLogTypeModel) => GetListNotNullableLogsTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlLogWebFkModel) => GetListNotNullableLogsWebsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlLogWebModel) => GetListNotNullableLogsWebs(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlOrderModel) => GetListNotNullableOrders(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlOrderWeighingModel) => GetListNotNullableOrdersWeighings(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlOrganizationModel) => GetListNotNullableOrganizations(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluBrandFkModel) => GetListNotNullablePlusBrandsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluBundleFkModel) => GetListNotNullablePlusBundlesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluCharacteristicModel) => GetListNotNullablePlusCharacteristics(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluCharacteristicsFkModel) => GetListNotNullablePlusCharacteristicsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluClipFkModel) => GetListNotNullablePlusClipsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluFkModel) => GetListNotNullablePlusFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluGroupFkModel) => GetListNotNullablePlusGroupFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluGroupModel) => GetListNotNullablePlusGroups(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluLabelModel) => GetListNotNullablePluLabels(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluModel) => GetListNotNullablePlus(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluNestingFkModel) => GetListNotNullablePlusNestingFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluScaleModel) => GetListNotNullablePlusScales(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluStorageMethodFkModel) => GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluStorageMethodModel) => GetListNotNullablePlusStoragesMethods(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluTemplateFkModel) => GetListNotNullablePlusTemplatesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluWeighingModel) => GetListNotNullablePlusWeighings(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPrinterModel) => GetListNotNullablePrinters(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPrinterResourceFkModel) => GetListNotNullablePrintersResources(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPrinterTypeModel) => GetListNotNullablePrintersTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlProductionFacilityModel) => GetListNotNullableProductionFacilities(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlProductSeriesModel) => GetListNotNullableProductSeries(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlScaleModel) => GetListNotNullableScales(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlScaleScreenShotModel) => GetListNotNullableScaleScreenShots(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlTaskModel) => GetListNotNullableTasks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlTaskTypeModel) => GetListNotNullableTasksTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlTemplateModel) => GetListNotNullableTemplates(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlTemplateResourceModel) => GetListNotNullableTemplateResources(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlVersionModel) => GetListNotNullableVersions(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlWorkShopModel) => GetListNotNullableWorkShops(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlAccessModel) => GetListNotNullableAccesses(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlAppModel) => GetListNotNullableApps(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPlu1CFkModel) => GetListNotNullablePlus1cFks(sqlCrudConfig).Cast<T>().ToList(),
        _ => new()
    };

    public List<WsSqlAccessModel> GetListNotNullableAccesses(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlAccessModel> list = GetListNotNullableCore<WsSqlAccessModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.RightsEnum).ThenByDescending(item => item.LoginDt).ToList();
        return list;
    }

    public List<WsSqlAppModel> GetListNotNullableApps(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlAppModel> list = GetListNotNullableCore<WsSqlAppModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlBarCodeModel> GetListNotNullableBarCodes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlBarCodeModel> list = GetListNotNullableCore<WsSqlBarCodeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<WsSqlBoxModel> GetListNotNullableBoxes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlBoxModel> list = GetListNotNullableCore<WsSqlBoxModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlBrandModel> GetListNotNullableBrands(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlBrandModel> list = GetListNotNullableCore<WsSqlBrandModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlBundleModel> GetListNotNullableBundles(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlBundleModel> list = GetListNotNullableCore<WsSqlBundleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlClipModel> GetListNotNullableClips(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlClipModel> list = GetListNotNullableCore<WsSqlClipModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlContragentModel> GetListNotNullableContragents(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlContragentModel> list = GetListNotNullableCore<WsSqlContragentModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlDeviceModel> GetListNotNullableDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlDeviceModel> list = GetListNotNullableCore<WsSqlDeviceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlDeviceScaleFkModel> GetListNotNullableDeviceScalesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name) ));
        List<WsSqlDeviceScaleFkModel> list = GetListNotNullableCore<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Device.Name).ToList()
                .OrderBy(item => item.Scale.Name).ToList();
        return list;
    }

    public List<WsSqlDeviceTypeModel> GetListNotNullableDeviceTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlDeviceTypeModel> list = GetListNotNullableCore<WsSqlDeviceTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlDeviceTypeFkModel> GetListNotNullableDeviceTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name)));
        List<WsSqlDeviceTypeFkModel> list = GetListNotNullableCore<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Type.Name).ToList()
                .OrderBy(item => item.Device.Name).ToList();
        return list;
    }

    public List<WsSqlLogTypeModel> GetListNotNullableLogsTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlLogTypeModel.Number) });
        List<WsSqlLogTypeModel> list = GetListNotNullableCore<WsSqlLogTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Number).ToList();
        return list;
    }

    public List<WsSqlLogModel> GetListNotNullableLogs(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlLogModel> list = GetListNotNullableCore<WsSqlLogModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public List<WsSqlLogMemoryModel> GetListNotNullableLogsMemories(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlLogMemoryModel> list = GetListNotNullableCore<WsSqlLogMemoryModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public List<WsSqlLogWebModel> GetListNotNullableLogsWebs(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt) });
        List<WsSqlLogWebModel> list = GetListNotNullableCore<WsSqlLogWebModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.CreateDt).ToList();
        return list;
    }

    public List<WsSqlLogWebFkModel> GetListNotNullableLogsWebsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { 
        //        Name = $"{nameof(LogWebFkModel.LogWebRequest)}.{nameof(LogWebModel.CreateDt)}", Direction = SqlOrderDirection.Desc });
        sqlCrudConfig.IsReadUncommitted = true;
        List<WsSqlLogWebFkModel> list = GetListNotNullableCore<WsSqlLogWebFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.LogWebRequest.CreateDt).ToList();
        return list;
    }

    public List<WsSqlPluCharacteristicModel> GetListNotNullablePlusCharacteristics(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluCharacteristicModel> list = GetListNotNullableCore<WsSqlPluCharacteristicModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlPluCharacteristicsFkModel> GetListNotNullablePlusCharacteristicsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        return GetListNotNullableCore<WsSqlPluCharacteristicsFkModel>(sqlCrudConfig);
    }

    public List<WsSqlPluGroupModel> GetListNotNullablePlusGroups(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluGroupModel> list = GetListNotNullableCore<WsSqlPluGroupModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlPluGroupFkModel> GetListNotNullablePlusGroupFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<WsSqlPluGroupFkModel> list = GetListNotNullableCore<WsSqlPluGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.PluGroup.Name).ToList()
                .OrderBy(item => item.Parent.Name).ToList();
        return list;
    }

    public List<WsSqlOrderModel> GetListNotNullableOrders(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlOrderModel> list = GetListNotNullableCore<WsSqlOrderModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<WsSqlOrderWeighingModel> GetListNotNullableOrdersWeighings(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlOrderWeighingModel> list = GetListNotNullableCore<WsSqlOrderWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<WsSqlOrganizationModel> GetListNotNullableOrganizations(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlOrganizationModel> list = GetListNotNullableCore<WsSqlOrganizationModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlPluLabelModel> GetListNotNullablePluLabels(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlPluLabelModel> list = GetListNotNullableCore<WsSqlPluLabelModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<WsSqlPluModel> GetListNotNullablePlus(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlPluModel.Number) });
        List<WsSqlPluModel> list = GetListNotNullableCore<WsSqlPluModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Number).ToList();
        return list;
    }

    public List<WsSqlPluFkModel> GetListNotNullablePlusFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPluFkModel> list = GetListNotNullableCore<WsSqlPluFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<WsSqlPlu1CFkModel> GetListNotNullablePlus1cFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPlu1CFkModel> list = GetListNotNullableCore<WsSqlPlu1CFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<WsSqlPluBrandFkModel> GetListNotNullablePlusBrandsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.ClearNullProperties), SqlOrderDirection.Asc));
        List<WsSqlPluBrandFkModel> list = GetListNotNullableCore<WsSqlPluBrandFkModel>(sqlCrudConfig);
        if (list.Any())
        {
            WsSqlPluBrandFkModel bundleFk = list.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Brand.IsNew)
                bundleFk.Brand = AccessManager.AccessItem.GetItemNewEmpty<WsSqlBrandModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Brand.Name).ToList();
        return list;
    }

    public List<WsSqlPluBundleFkModel> GetListNotNullablePlusBundlesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluBundleFkModel.Bundle)}.{nameof(BundleModel.Name)}", SqlOrderDirection.Asc));
        List<WsSqlPluBundleFkModel> list = GetListNotNullableCore<WsSqlPluBundleFkModel>(sqlCrudConfig);
        if (list.Count > 0)
        {
            WsSqlPluBundleFkModel bundleFk = list.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Bundle.IsNew)
                bundleFk.Bundle = AccessManager.AccessItem.GetItemNewEmpty<WsSqlBundleModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Bundle.Name).ToList();
        return list;
    }

    public List<WsSqlPluClipFkModel> GetListNotNullablePlusClipsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        List<WsSqlPluClipFkModel> list = GetListNotNullableCore<WsSqlPluClipFkModel>(sqlCrudConfig);
        if (list.Count > 0)
        {
            WsSqlPluClipFkModel pluClipFk = list.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluModel>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = AccessManager.AccessItem.GetItemNewEmpty<WsSqlClipModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Clip.Name).ToList();
        return list;
    }

    public List<WsSqlPluScaleModel> GetListNotNullablePlusScales(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        List<WsSqlPluScaleModel> list = GetListNotNullableCore<WsSqlPluScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<WsSqlPluStorageMethodModel> GetListNotNullablePlusStoragesMethods(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluStorageMethodModel> list = GetListNotNullableCore<WsSqlPluStorageMethodModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlPluStorageMethodFkModel> GetListNotNullablePlusStoragesMethodsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPluStorageMethodFkModel> list = GetListNotNullableCore<WsSqlPluStorageMethodFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<WsSqlPluTemplateFkModel> GetListNotNullablePlusTemplatesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        List<WsSqlPluTemplateFkModel> list = GetListNotNullableCore<WsSqlPluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Template.Title).ToList()
                .OrderBy(item => item.Plu.Name).ToList();
        return list;
    }

    public List<WsSqlPluWeighingModel> GetListNotNullablePlusWeighings(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlPluWeighingModel> list = GetListNotNullableCore<WsSqlPluWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<WsSqlPluNestingFkModel> GetListNotNullablePlusNestingFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPluNestingFkModel> list = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
        {
            list.Add(AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluNestingFkModel>());
        }
        if (string.IsNullOrEmpty(sqlCrudConfig.NativeQuery))
        {
            sqlCrudConfig.NativeQuery = WsSqlQueriesScales.Tables.PluNestingFks.GetList(false);
            //new("P_UID", plu.IdentityValueUid), true);
        }
        object[] objects = AccessCore.GetArrayObjectsNotNullable(sqlCrudConfig);
        foreach (object obj in objects)
        {
            if (obj is object[] { Length: 45 } item)
            {
                if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                {
                    WsSqlPluBundleFkModel pluBundle = new();
                    // -- [DB_SCALES].[PLUS_BUNDLES_FK] | 11 - 16
                    if (Guid.TryParse(Convert.ToString(item[11]), out Guid pluBundleUid))
                    {
                        pluBundle.IdentityValueUid = pluBundleUid;
                        pluBundle.CreateDt = Convert.ToDateTime(item[12]);
                        pluBundle.ChangeDt = Convert.ToDateTime(item[13]);
                        pluBundle.IsMarked = Convert.ToBoolean(item[14]);
                    }

                    // -- [DB_SCALES].[PLUS] | 17 - 30
                    if (Guid.TryParse(Convert.ToString(item[17]), out Guid pluUid))
                    {
                        pluBundle.Plu.IdentityValueUid = pluUid;
                        pluBundle.Plu.CreateDt = Convert.ToDateTime(item[18]);
                        pluBundle.Plu.ChangeDt = Convert.ToDateTime(item[19]);
                        pluBundle.Plu.IsMarked = Convert.ToBoolean(item[20]);
                        pluBundle.Plu.Number = Convert.ToInt16(item[21]);
                        pluBundle.Plu.Name = Convert.ToString(item[22]);
                        pluBundle.Plu.FullName = Convert.ToString(item[23]);
                        pluBundle.Plu.Description = Convert.ToString(item[24]);
                        pluBundle.Plu.ShelfLifeDays = Convert.ToByte(item[25]);
                        pluBundle.Plu.Gtin = Convert.ToString(item[26]);
                        pluBundle.Plu.Ean13 = Convert.ToString(item[27]);
                        pluBundle.Plu.Itf14 = Convert.ToString(item[28]);
                        pluBundle.Plu.IsCheckWeight = Convert.ToBoolean(item[29]);
                    }

                    // -- [DB_SCALES].[BUNDLES] | 30 - 35
                    if (Guid.TryParse(Convert.ToString(item[30]), out Guid bundleUid))
                    {
                        pluBundle.Bundle.IdentityValueUid = bundleUid;
                        pluBundle.Bundle.CreateDt = Convert.ToDateTime(item[31]);
                        pluBundle.Bundle.ChangeDt = Convert.ToDateTime(item[32]);
                        pluBundle.Bundle.IsMarked = Convert.ToBoolean(item[33]);
                        pluBundle.Bundle.Name = Convert.ToString(item[34]);
                        pluBundle.Bundle.Weight = Convert.ToDecimal(item[35]);
                    }

                    WsSqlBoxModel box = new();
                    // -- [DB_SCALES].[BOXES] | 36 - 41
                    if (Guid.TryParse(Convert.ToString(item[36]), out Guid boxUid))
                    {
                        box.IdentityValueUid = boxUid;
                        box.CreateDt = Convert.ToDateTime(item[37]);
                        box.ChangeDt = Convert.ToDateTime(item[38]);
                        box.IsMarked = Convert.ToBoolean(item[39]);
                        box.Name = Convert.ToString(item[40]);
                        box.Weight = Convert.ToDecimal(item[41]);
                    }

                    // -- UID_1C | 42 - 44
                    if (Guid.TryParse(Convert.ToString(item[42]), out Guid pluUid1c))
                        pluBundle.Plu.Uid1C = pluUid1c;
                    if (Guid.TryParse(Convert.ToString(item[43]), out Guid boxUid1c))
                        box.Uid1C = boxUid1c;
                    if (Guid.TryParse(Convert.ToString(item[44]), out Guid bundleUid1c))
                        pluBundle.Bundle.Uid1C = bundleUid1c;
                    // All.
                    list.Add(new()
                    {
                        IdentityValueUid = uid,
                        CreateDt = Convert.ToDateTime(item[1]),
                        ChangeDt = Convert.ToDateTime(item[2]),
                        IsMarked = Convert.ToBoolean(item[3]),
                        IsDefault = Convert.ToBoolean(item[4]),
                        BundleCount = Convert.ToInt16(item[5]),
                        WeightMax = Convert.ToDecimal(item[6]),
                        WeightMin = Convert.ToDecimal(item[7]),
                        WeightNom = Convert.ToDecimal(item[8]),
                        PluBundle = pluBundle,
                        Box = box
                    });
                }
            }
            else
                throw new($"Exception length in {nameof(GetListNotNullablePlusNestingFks)} for native query!");
        }
        return list;
    }

    public List<WsSqlPrinterModel> GetListNotNullablePrinters(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPrinterModel> list = GetListNotNullableCore<WsSqlPrinterModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlPrinterResourceFkModel> GetListNotNullablePrintersResources(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<WsSqlPrinterResourceFkModel> list = GetListNotNullableCore<WsSqlPrinterResourceFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Printer.Name).ToList()
                .OrderBy(item => item.TemplateResource.Name).ToList();
        return list;
    }

    public List<WsSqlPrinterTypeModel> GetListNotNullablePrintersTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlPrinterTypeModel> list = GetListNotNullableCore<WsSqlPrinterTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlProductionFacilityModel> GetListNotNullableProductionFacilities(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlProductionFacilityModel> list = GetListNotNullableCore<WsSqlProductionFacilityModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlProductSeriesModel> GetListNotNullableProductSeries(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlProductSeriesModel> list = GetListNotNullableCore<WsSqlProductSeriesModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public List<WsSqlScaleModel> GetListNotNullableScales(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Description) });
        List<WsSqlScaleModel> scales = GetListNotNullableCore<WsSqlScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && scales.Any())
            scales = scales.OrderBy(item => item.Description).ToList();
        return scales;
    }

    public List<WsSqlScaleScreenShotModel> GetListNotNullableScaleScreenShots(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlScaleScreenShotModel> list = GetListNotNullableCore<WsSqlScaleScreenShotModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<WsSqlTaskModel> GetListNotNullableTasks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlTaskModel> list = GetListNotNullableCore<WsSqlTaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Scale.Description).ToList();
        return list;
    }

    public List<WsSqlTaskTypeModel> GetListNotNullableTasksTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlTaskTypeModel> list = GetListNotNullableCore<WsSqlTaskTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<WsSqlTemplateModel> GetListNotNullableTemplates(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTemplateModel.Title) });
        List<WsSqlTemplateModel> list = GetListNotNullableCore<WsSqlTemplateModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Title).ToList();
        return list;
    }

    public List<WsSqlTemplateResourceModel> GetListNotNullableTemplateResources(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<WsSqlTemplateResourceModel> list = GetListNotNullableCore<WsSqlTemplateResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Name)
                .ThenBy(item => item.Type).ToList();
        return list;
    }

    public List<WsSqlVersionModel> GetListNotNullableVersions(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlVersionModel.Version), Direction = WsSqlOrderDirection.Desc });
        List<WsSqlVersionModel> list = GetListNotNullableCore<WsSqlVersionModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.Version).ToList();
        return list;
    }

    public List<WsSqlWorkShopModel> GetListNotNullableWorkShops(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlWorkShopModel> list = GetListNotNullableCore<WsSqlWorkShopModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    #endregion

    #region Public and private methods

    public List<WsSqlDeviceModel> GetListDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceModel>());
        List<WsSqlDeviceModel> list = GetListNotNullableCore<WsSqlDeviceModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<WsSqlDeviceTypeModel> GetListDevicesTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceTypeModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceTypeModel>());
        List<WsSqlDeviceTypeModel> list = GetListNotNullableCore<WsSqlDeviceTypeModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<WsSqlDeviceTypeFkModel> GetListDevicesTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceTypeFkModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(new() { Device = AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceModel>(), 
                Type = AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceTypeModel>() });
        List<WsSqlDeviceTypeFkModel> list = GetListNotNullableCore<WsSqlDeviceTypeFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Type.Name).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<WsSqlDeviceScaleFkModel> GetListDevicesScalesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        List<WsSqlDeviceScaleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(new() { Device = AccessManager.AccessItem.GetItemNewEmpty<WsSqlDeviceModel>(), 
                Scale = AccessManager.AccessItem.GetItemNewEmpty<WsSqlScaleModel>() });
        List<WsSqlDeviceScaleFkModel> list = GetListNotNullableCore<WsSqlDeviceScaleFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Scale.Description).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<WsSqlDeviceTypeModel> GetListDevicesTypes(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<WsSqlDeviceTypeModel> deviceTypes = GetListDevicesTypes(sqlCrudConfig);
        return deviceTypes;
    }

    public List<WsSqlDeviceModel> GetListDevices(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<WsSqlDeviceModel> devices = GetListDevices(sqlCrudConfig);
        return devices;
    }

    public List<WsSqlDeviceTypeFkModel> GetListDevicesTypesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<WsSqlDeviceTypeFkModel> deviceTypesFks = GetListDevicesTypesFks(sqlCrudConfig);
        return deviceTypesFks;
    }

    public List<WsSqlDeviceTypeFkModel> GetListDevicesTypesFkFree(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<WsSqlDeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<WsSqlDeviceModel> devices = GetListNotNullableCore<WsSqlDeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<WsSqlDeviceTypeFkModel> GetListDevicesTypesFkBusy(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<WsSqlDeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<WsSqlDeviceModel> devices = GetListNotNullableCore<WsSqlDeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    public List<WsSqlPluLabelModel> GetListPluLabels(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        sqlCrudConfig.Orders.Add(new() { Name = nameof(WsSqlPluWeighingModel.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        return GetListNotNullableCore<WsSqlPluLabelModel>(sqlCrudConfig);
    }

    public List<WsSqlScaleScreenShotModel> GetListScalesScreenShots(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlScaleScreenShotModel.Scale), itemFilter?.IdentityValueId),
            isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<WsSqlScaleScreenShotModel> result = GetListNotNullableCore<WsSqlScaleScreenShotModel>(sqlCrudConfig);
        result = result.OrderByDescending(x => x.CreateDt).ToList();
        return result;
    }

    public List<WsSqlPluBundleFkModel> GetListPluBundles(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        List<WsSqlPluBundleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluBundleFkModel>());
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlPluBundleFkModel.Plu), itemFilter?.IdentityValueUid);

        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel { Name = nameof(WsSqlPluBundleFkModel.Plu), Direction = WsSqlOrderDirection.Asc },
            isShowMarked, isShowOnlyTop);
        result.AddRange(GetListNotNullableCore<WsSqlPluBundleFkModel>(sqlCrudConfig));
        result = result.OrderBy(x => x.Bundle.Name).ToList();
        result = result.OrderBy(x => x.Plu.Number).ToList();
        return result;
    }

    public List<WsSqlPrinterResourceFkModel> GetListPrinterResources(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop)
    {
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlPrinterResourceFkModel.Printer), itemFilter?.IdentityValueId);
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel { Name = nameof(WsSqlTableBase.Description), Direction = WsSqlOrderDirection.Asc },
            isShowMarked, isShowOnlyTop);
        return GetListNotNullableCore<WsSqlPrinterResourceFkModel>(sqlCrudConfig);
    }

    public List<WsSqlPrinterTypeModel> GetListPrinterTypes(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new SqlFieldOrderModel { Name = nameof(WsSqlPrinterTypeModel.Name), Direction = WsSqlOrderDirection.Asc }, isShowMarked, isShowOnlyTop);
        return GetListNotNullableCore<WsSqlPrinterTypeModel>(sqlCrudConfig);
    }

    #endregion
}