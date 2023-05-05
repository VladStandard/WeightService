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
    public List<BarCodeModel> BarCodes { get; set; } = new();
    public List<BoxModel> Boxes { get; set; } = new();
    public List<BrandModel> Brands { get; set; } = new();
    public List<BundleModel> Bundles { get; set; } = new();
    public List<ClipModel> Clips { get; set; } = new();
    public List<ContragentModel> Contragents { get; set; } = new();
    public List<DeviceModel> Devices { get; set; } = new();
    public List<DeviceTypeModel> DeviceTypes { get; set; } = new();
    public List<DeviceTypeFkModel> DeviceTypeFks { get; set; } = new();
    public List<DeviceScaleFkModel> DeviceScaleFks { get; set; } = new();
    public List<LogModel> Logs { get; set; } = new();
    public List<LogMemoryModel> LogsMemories { get; set; } = new();
    public List<LogTypeModel> LogTypes { get; set; } = new();
    public List<LogWebModel> LogsWebs { get; set; } = new();
    public List<LogWebFkModel> LogsWebsFks { get; set; } = new();
    public List<PluGroupModel> NomenclaturesGroups { get; set; } = new();
    public List<PluGroupFkModel> NomenclaturesGroupsFk { get; set; } = new();
    public List<PluCharacteristicModel> NomenclaturesCharacteristics { get; set; } = new();
    public List<PluCharacteristicsFkModel> NomenclaturesCharacteristicsFk { get; set; } = new();
    public List<OrderModel> Orders { get; set; } = new();
    public List<OrderWeighingModel> OrderWeighings { get; set; } = new();
    public List<OrganizationModel> Organizations { get; set; } = new();
    public List<PluLabelModel> PluLabels { get; set; } = new();
    public List<WsSqlPluModel> Plus { get; set; } = new();
    public List<PluFkModel> PlusFks { get; set; } = new();
    public List<PluBrandFkModel> PluBrandFks { get; set; } = new();
    public List<PluBundleFkModel> PluBundleFks { get; set; } = new();
    public List<PluClipFkModel> PluClipFks { get; set; } = new();
    public List<PluNestingFkModel> PluNestingFks { get; set; } = new();
    public List<PluScaleModel> PluScales { get; set; } = new();
    public List<PluStorageMethodModel> PluStorageMethods { get; set; } = new();
    public List<PluStorageMethodFkModel> PluStorageMethodsFks { get; set; } = new();
    public List<PluTemplateFkModel> PluTemplateFks { get; set; } = new();
    public List<PluWeighingModel> PluWeighings { get; set; } = new();
    public List<PrinterModel> Printers { get; set; } = new();
    public List<PrinterResourceFkModel> PrinterResources { get; set; } = new();
    public List<PrinterTypeModel> PrinterTypes { get; set; } = new();
    public List<ProductionFacilityModel> ProductionFacilities { get; set; } = new();
    public List<ProductSeriesModel> ProductSeries { get; set; } = new();
    public List<ScaleModel> Scales { get; set; } = new();
    public List<ScaleScreenShotModel> ScaleScreenShots { get; set; } = new();
    public List<TaskModel> Tasks { get; set; } = new();
    public List<TaskTypeModel> TaskTypes { get; set; } = new();
    public List<TemplateModel> Templates { get; set; } = new();
    public List<TemplateResourceModel> TemplateResources { get; set; } = new();
    public List<VersionModel> Versions { get; set; } = new();
    public List<WorkShopModel> WorkShops { get; set; } = new();

    #endregion

    #region Public and private methods

    public List<T> GetListNotNullableCore<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        AccessCore.GetListNotNullable<T>(sqlCrudConfig);
    
    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() => typeof(T) switch
    {
        var cls when cls == typeof(BarCodeModel) => GetListNotNullableBarCodes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BoxModel) => GetListNotNullableBoxes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BrandModel) => GetListNotNullableBrands(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BundleModel) => GetListNotNullableBundles(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ClipModel) => GetListNotNullableClips(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ContragentModel) => GetListNotNullableContragents(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceModel) => GetListNotNullableDevices(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceScaleFkModel) => GetListNotNullableDeviceScalesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceTypeFkModel) => GetListNotNullableDeviceTypesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceTypeModel) => GetListNotNullableDeviceTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogMemoryModel) => GetListNotNullableLogsMemories(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogModel) => GetListNotNullableLogs(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogTypeModel) => GetListNotNullableLogsTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogWebFkModel) => GetListNotNullableLogsWebsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogWebModel) => GetListNotNullableLogsWebs(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(OrderModel) => GetListNotNullableOrders(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(OrderWeighingModel) => GetListNotNullableOrdersWeighings(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(OrganizationModel) => GetListNotNullableOrganizations(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluBrandFkModel) => GetListNotNullablePlusBrandsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluBundleFkModel) => GetListNotNullablePlusBundlesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluCharacteristicModel) => GetListNotNullablePlusCharacteristics(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluCharacteristicsFkModel) => GetListNotNullablePlusCharacteristicsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluClipFkModel) => GetListNotNullablePlusClipsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluFkModel) => GetListNotNullablePlusFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluGroupFkModel) => GetListNotNullablePlusGroupFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluGroupModel) => GetListNotNullablePlusGroups(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluLabelModel) => GetListNotNullablePluLabels(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WsSqlPluModel) => GetListNotNullablePlus(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluNestingFkModel) => GetListNotNullablePlusNestingFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluScaleModel) => GetListNotNullablePlusScales(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluStorageMethodFkModel) => GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluStorageMethodModel) => GetListNotNullablePlusStoragesMethods(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluTemplateFkModel) => GetListNotNullablePlusTemplatesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluWeighingModel) => GetListNotNullablePlusWeighings(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PrinterModel) => GetListNotNullablePrinters(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PrinterResourceFkModel) => GetListNotNullablePrintersResources(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PrinterTypeModel) => GetListNotNullablePrintersTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ProductionFacilityModel) => GetListNotNullableProductionFacilities(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ProductSeriesModel) => GetListNotNullableProductSeries(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ScaleModel) => GetListNotNullableScales(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ScaleScreenShotModel) => GetListNotNullableScaleScreenShots(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(TaskModel) => GetListNotNullableTasks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(TaskTypeModel) => GetListNotNullableTasksTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(TemplateModel) => GetListNotNullableTemplates(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(TemplateResourceModel) => GetListNotNullableTemplateResources(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(VersionModel) => GetListNotNullableVersions(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(WorkShopModel) => GetListNotNullableWorkShops(sqlCrudConfig).Cast<T>().ToList(),
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

    public List<BarCodeModel> GetListNotNullableBarCodes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<BarCodeModel> list = GetListNotNullableCore<BarCodeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<BoxModel> GetListNotNullableBoxes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<BoxModel> list = GetListNotNullableCore<BoxModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<BrandModel> GetListNotNullableBrands(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<BrandModel> list = GetListNotNullableCore<BrandModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<BundleModel> GetListNotNullableBundles(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<BundleModel> list = GetListNotNullableCore<BundleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<ClipModel> GetListNotNullableClips(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<ClipModel> list = GetListNotNullableCore<ClipModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<ContragentModel> GetListNotNullableContragents(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<ContragentModel> list = GetListNotNullableCore<ContragentModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<DeviceModel> GetListNotNullableDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<DeviceModel> list = GetListNotNullableCore<DeviceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<DeviceScaleFkModel> GetListNotNullableDeviceScalesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name) ));
        List<DeviceScaleFkModel> list = GetListNotNullableCore<DeviceScaleFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Device.Name).ToList()
                .OrderBy(item => item.Scale.Name).ToList();
        return list;
    }

    public List<DeviceTypeModel> GetListNotNullableDeviceTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<DeviceTypeModel> list = GetListNotNullableCore<DeviceTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<DeviceTypeFkModel> GetListNotNullableDeviceTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name)));
        List<DeviceTypeFkModel> list = GetListNotNullableCore<DeviceTypeFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Type.Name).ToList()
                .OrderBy(item => item.Device.Name).ToList();
        return list;
    }

    public List<LogTypeModel> GetListNotNullableLogsTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(LogTypeModel.Number) });
        List<LogTypeModel> list = GetListNotNullableCore<LogTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Number).ToList();
        return list;
    }

    public List<LogModel> GetListNotNullableLogs(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Desc });
        List<LogModel> list = GetListNotNullableCore<LogModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public List<LogMemoryModel> GetListNotNullableLogsMemories(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Desc });
        List<LogMemoryModel> list = GetListNotNullableCore<LogMemoryModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public List<LogWebModel> GetListNotNullableLogsWebs(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt) });
        List<LogWebModel> list = GetListNotNullableCore<LogWebModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.CreateDt).ToList();
        return list;
    }

    public List<LogWebFkModel> GetListNotNullableLogsWebsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { 
        //        Name = $"{nameof(LogWebFkModel.LogWebRequest)}.{nameof(LogWebModel.CreateDt)}", Direction = SqlOrderDirection.Desc });
        sqlCrudConfig.IsReadUncommitted = true;
        List<LogWebFkModel> list = GetListNotNullableCore<LogWebFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.LogWebRequest.CreateDt).ToList();
        return list;
    }

    public List<PluCharacteristicModel> GetListNotNullablePlusCharacteristics(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<PluCharacteristicModel> list = GetListNotNullableCore<PluCharacteristicModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<PluCharacteristicsFkModel> GetListNotNullablePlusCharacteristicsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        return GetListNotNullableCore<PluCharacteristicsFkModel>(sqlCrudConfig);
    }

    public List<PluGroupModel> GetListNotNullablePlusGroups(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<PluGroupModel> list = GetListNotNullableCore<PluGroupModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<PluGroupFkModel> GetListNotNullablePlusGroupFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<PluGroupFkModel> list = GetListNotNullableCore<PluGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.PluGroup.Name).ToList()
                .OrderBy(item => item.Parent.Name).ToList();
        return list;
    }

    public List<OrderModel> GetListNotNullableOrders(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<OrderModel> list = GetListNotNullableCore<OrderModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<OrderWeighingModel> GetListNotNullableOrdersWeighings(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<OrderWeighingModel> list = GetListNotNullableCore<OrderWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<OrganizationModel> GetListNotNullableOrganizations(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<OrganizationModel> list = GetListNotNullableCore<OrganizationModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<PluLabelModel> GetListNotNullablePluLabels(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<PluLabelModel> list = GetListNotNullableCore<PluLabelModel>(sqlCrudConfig);
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

    public List<PluFkModel> GetListNotNullablePlusFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<PluFkModel> list = GetListNotNullableCore<PluFkModel>(sqlCrudConfig);
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

    public List<PluBrandFkModel> GetListNotNullablePlusBrandsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.ClearNullProperties), SqlOrderDirection.Asc));
        List<PluBrandFkModel> list = GetListNotNullableCore<PluBrandFkModel>(sqlCrudConfig);
        if (list.Any())
        {
            PluBrandFkModel bundleFk = list.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Brand.IsNew)
                bundleFk.Brand = AccessManager.AccessItem.GetItemNewEmpty<BrandModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Brand.Name).ToList();
        return list;
    }

    public List<PluBundleFkModel> GetListNotNullablePlusBundlesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluBundleFkModel.Bundle)}.{nameof(BundleModel.Name)}", SqlOrderDirection.Asc));
        List<PluBundleFkModel> list = GetListNotNullableCore<PluBundleFkModel>(sqlCrudConfig);
        if (list.Count > 0)
        {
            PluBundleFkModel bundleFk = list.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluModel>();
            if (bundleFk.Bundle.IsNew)
                bundleFk.Bundle = AccessManager.AccessItem.GetItemNewEmpty<BundleModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Bundle.Name).ToList();
        return list;
    }

    public List<PluClipFkModel> GetListNotNullablePlusClipsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        List<PluClipFkModel> list = GetListNotNullableCore<PluClipFkModel>(sqlCrudConfig);
        if (list.Count > 0)
        {
            PluClipFkModel pluClipFk = list.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = AccessManager.AccessItem.GetItemNewEmpty<WsSqlPluModel>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = AccessManager.AccessItem.GetItemNewEmpty<ClipModel>();
        }
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Clip.Name).ToList();
        return list;
    }

    public List<PluScaleModel> GetListNotNullablePlusScales(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        List<PluScaleModel> list = GetListNotNullableCore<PluScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<PluStorageMethodModel> GetListNotNullablePlusStoragesMethods(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<PluStorageMethodModel> list = GetListNotNullableCore<PluStorageMethodModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<PluStorageMethodFkModel> GetListNotNullablePlusStoragesMethodsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<PluStorageMethodFkModel> list = GetListNotNullableCore<PluStorageMethodFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<PluTemplateFkModel> GetListNotNullablePlusTemplatesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        List<PluTemplateFkModel> list = GetListNotNullableCore<PluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Template.Title).ToList()
                .OrderBy(item => item.Plu.Name).ToList();
        return list;
    }

    public List<PluWeighingModel> GetListNotNullablePlusWeighings(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<PluWeighingModel> list = GetListNotNullableCore<PluWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<PluNestingFkModel> GetListNotNullablePlusNestingFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<PluNestingFkModel> list = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
        {
            list.Add(AccessManager.AccessItem.GetItemNewEmpty<PluNestingFkModel>());
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
                    PluBundleFkModel pluBundle = new();
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

                    BoxModel box = new();
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

    public List<PrinterModel> GetListNotNullablePrinters(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<PrinterModel> list = GetListNotNullableCore<PrinterModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<PrinterResourceFkModel> GetListNotNullablePrintersResources(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<PrinterResourceFkModel> list = GetListNotNullableCore<PrinterResourceFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Printer.Name).ToList()
                .OrderBy(item => item.TemplateResource.Name).ToList();
        return list;
    }

    public List<PrinterTypeModel> GetListNotNullablePrintersTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<PrinterTypeModel> list = GetListNotNullableCore<PrinterTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<ProductionFacilityModel> GetListNotNullableProductionFacilities(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<ProductionFacilityModel> list = GetListNotNullableCore<ProductionFacilityModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<ProductSeriesModel> GetListNotNullableProductSeries(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlOrderDirection.Desc });
        List<ProductSeriesModel> list = GetListNotNullableCore<ProductSeriesModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public List<ScaleModel> GetListNotNullableScales(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Description) });
        List<ScaleModel> scales = GetListNotNullableCore<ScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && scales.Any())
            scales = scales.OrderBy(item => item.Description).ToList();
        return scales;
    }

    public List<ScaleScreenShotModel> GetListNotNullableScaleScreenShots(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        List<ScaleScreenShotModel> list = GetListNotNullableCore<ScaleScreenShotModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

    public List<TaskModel> GetListNotNullableTasks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<TaskModel> list = GetListNotNullableCore<TaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Scale.Description).ToList();
        return list;
    }

    public List<TaskTypeModel> GetListNotNullableTasksTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<TaskTypeModel> list = GetListNotNullableCore<TaskTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    public List<TemplateModel> GetListNotNullableTemplates(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(TemplateModel.Title) });
        List<TemplateModel> list = GetListNotNullableCore<TemplateModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Title).ToList();
        return list;
    }

    public List<TemplateResourceModel> GetListNotNullableTemplateResources(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(WsSqlTableBase.Name), SqlOrderDirection.Asc));
        List<TemplateResourceModel> list = GetListNotNullableCore<TemplateResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Name)
                .ThenBy(item => item.Type).ToList();
        return list;
    }

    public List<VersionModel> GetListNotNullableVersions(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(VersionModel.Version), Direction = WsSqlOrderDirection.Desc });
        List<VersionModel> list = GetListNotNullableCore<VersionModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.Version).ToList();
        return list;
    }

    public List<WorkShopModel> GetListNotNullableWorkShops(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WorkShopModel> list = GetListNotNullableCore<WorkShopModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    #endregion

    #region Public and private methods

    public List<DeviceModel> GetListDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(AccessManager.AccessItem.GetItemNewEmpty<DeviceModel>());
        List<DeviceModel> list = GetListNotNullableCore<DeviceModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<DeviceTypeModel> GetListDevicesTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceTypeModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(AccessManager.AccessItem.GetItemNewEmpty<DeviceTypeModel>());
        List<DeviceTypeModel> list = GetListNotNullableCore<DeviceTypeModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<DeviceTypeFkModel> GetListDevicesTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceTypeFkModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(new() { Device = AccessManager.AccessItem.GetItemNewEmpty<DeviceModel>(), 
                Type = AccessManager.AccessItem.GetItemNewEmpty<DeviceTypeModel>() });
        List<DeviceTypeFkModel> list = GetListNotNullableCore<DeviceTypeFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Type.Name).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<DeviceScaleFkModel> GetListDevicesScalesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        List<DeviceScaleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(new() { Device = AccessManager.AccessItem.GetItemNewEmpty<DeviceModel>(), 
                Scale = AccessManager.AccessItem.GetItemNewEmpty<ScaleModel>() });
        List<DeviceScaleFkModel> list = GetListNotNullableCore<DeviceScaleFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Scale.Description).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    public List<DeviceTypeModel> GetListDevicesTypes(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeModel> deviceTypes = GetListDevicesTypes(sqlCrudConfig);
        return deviceTypes;
    }

    public List<DeviceModel> GetListDevices(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceModel> devices = GetListDevices(sqlCrudConfig);
        return devices;
    }

    public List<DeviceTypeFkModel> GetListDevicesTypesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypesFks = GetListDevicesTypesFks(sqlCrudConfig);
        return deviceTypesFks;
    }

    public List<DeviceTypeFkModel> GetListDevicesTypesFkFree(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<DeviceModel> devices = GetListNotNullableCore<DeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFkBusy(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<DeviceModel> devices = GetListNotNullableCore<DeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    public List<PluLabelModel> GetListPluLabels(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        sqlCrudConfig.Orders.Add(new() { Name = nameof(PluWeighingModel.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        return GetListNotNullableCore<PluLabelModel>(sqlCrudConfig);
    }

    public List<ScaleScreenShotModel> GetListScalesScreenShots(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(ScaleScreenShotModel.Scale), itemFilter?.IdentityValueId),
            isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<ScaleScreenShotModel> result = GetListNotNullableCore<ScaleScreenShotModel>(sqlCrudConfig);
        result = result.OrderByDescending(x => x.CreateDt).ToList();
        return result;
    }

    public List<PluBundleFkModel> GetListPluBundles(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        List<PluBundleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(AccessManager.AccessItem.GetItemNewEmpty<PluBundleFkModel>());
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PluBundleFkModel.Plu), itemFilter?.IdentityValueUid);

        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel { Name = nameof(PluBundleFkModel.Plu), Direction = WsSqlOrderDirection.Asc },
            isShowMarked, isShowOnlyTop);
        result.AddRange(GetListNotNullableCore<PluBundleFkModel>(sqlCrudConfig));
        result = result.OrderBy(x => x.Bundle.Name).ToList();
        result = result.OrderBy(x => x.Plu.Number).ToList();
        return result;
    }

    public List<PrinterResourceFkModel> GetListPrinterResources(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop)
    {
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PrinterResourceFkModel.Printer), itemFilter?.IdentityValueId);
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel { Name = nameof(WsSqlTableBase.Description), Direction = WsSqlOrderDirection.Asc },
            isShowMarked, isShowOnlyTop);
        return GetListNotNullableCore<PrinterResourceFkModel>(sqlCrudConfig);
    }

    public List<PrinterTypeModel> GetListPrinterTypes(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new SqlFieldOrderModel { Name = nameof(PrinterTypeModel.Name), Direction = WsSqlOrderDirection.Asc }, isShowMarked, isShowOnlyTop);
        return GetListNotNullableCore<PrinterTypeModel>(sqlCrudConfig);
    }

    #endregion
}