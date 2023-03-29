// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.TableDiagModels.ScalesScreenshots;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.LogsWebsFks;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using PluBundleFkValidator = DataCore.Sql.TableScaleFkModels.PlusBundlesFks.PluBundleFkValidator;

namespace DataCore.Sql.Core.Models;

public partial class DataContextModel
{
    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess => DataAccessHelper.Instance;
    private List<AccessModel> Accesses { get; set; }
    private List<AppModel> Apps { get; set; }
    private List<BarCodeModel> BarCodes { get; set; }
    public List<BoxModel> Boxes { get; set; }
    private List<BrandModel> Brands { get; set; }
    public List<BundleModel> Bundles { get; set; }
    private List<ClipModel> Clips { get; set; }
    private List<ContragentModel> Contragents { get; set; }
    public List<DeviceModel> Devices { get; set; }
    public List<DeviceTypeModel> DeviceTypes { get; set; }
    private List<DeviceTypeFkModel> DeviceTypeFks { get; set; }
    private List<DeviceScaleFkModel> DeviceScaleFks { get; set; }
    private List<LogModel> Logs { get; set; }
    private List<LogTypeModel> LogTypes { get; set; }
    private List<LogWebModel> LogsWebs { get; set; }
    private List<LogWebFkModel> LogsWebsFks { get; set; }
    private List<PluGroupModel> NomenclaturesGroups { get; set; }
    private List<PluGroupFkModel> NomenclaturesGroupsFk { get; set; }
    private List<PluCharacteristicModel> NomenclaturesCharacteristics { get; set; }
    private List<PluCharacteristicsFkModel> NomenclaturesCharacteristicsFk { get; set; }
    private List<OrderModel> Orders { get; set; }
    private List<OrderWeighingModel> OrderWeighings { get; set; }
    private List<OrganizationModel> Organizations { get; set; }
    private List<PluLabelModel> PluLabels { get; set; }
    public List<PluModel> Plus { get; set; }
    private List<PluFkModel> PlusFks { get; set; }
    private List<PluBrandFkModel> PluBrandFks { get; set; }
    public List<PluBundleFkModel> PluBundleFks { get; set; }
    private List<PluClipFkModel> PluClipFks { get; set; }
    private List<PluScaleModel> PluScales { get; set; }
    private List<PluStorageMethodModel> PluStorageMethods { get; set; }
    private List<PluStorageMethodFkModel> PluStorageMethodsFks { get; set; }
    private List<PluTemplateFkModel> PluTemplateFks { get; set; }
    private List<PluWeighingModel> PluWeighings { get; set; }
    private List<PluNestingFkModel> PluNestingFks { get; set; }
    public List<PrinterModel> Printers { get; set; }
    private List<PrinterResourceFkModel> PrinterResources { get; set; }
    public List<PrinterTypeModel> PrinterTypes { get; set; }
    public List<ProductionFacilityModel> ProductionFacilities { get; set; }
    private List<ProductSeriesModel> ProductSeries { get; set; }
    public List<ScaleModel> Scales { get; set; }
    private List<ScaleScreenShotModel> ScaleScreenShots { get; set; }
    private List<TaskModel> Tasks { get; set; }
    private List<TaskTypeModel> TaskTypes { get; set; }
    public List<TemplateModel> Templates { get; set; }
    public List<TemplateResourceModel> TemplateResources { get; set; }
    private List<VersionModel> Versions { get; set; }
    public List<WorkShopModel> WorkShops { get; set; }

    public NHibernate.ISession Session => DataAccess.SessionFactory.GetCurrentSession();

    public DataContextModel()
    {
        Accesses = new();
        Apps = new();
        BarCodes = new();
        Boxes = new();
        Brands = new();
        Bundles = new();
        Clips = new();
        Contragents = new();
        Devices = new();
        DeviceTypes = new();
        DeviceTypeFks = new();
        DeviceScaleFks = new();
        Logs = new();
        LogTypes = new();
        LogsWebs = new();
        LogsWebsFks = new();
        NomenclaturesGroups = new();
        NomenclaturesGroupsFk = new();
        NomenclaturesCharacteristics = new();
        NomenclaturesCharacteristicsFk = new();
        Orders = new();
        OrderWeighings = new();
        Organizations = new();
        PluLabels = new();
        Plus = new();
        PlusFks = new();
        PluBrandFks = new();
        PluBundleFks = new();
        PluClipFks = new();
        PluScales = new();
        PluStorageMethods = new();
        PluStorageMethodsFks = new();
        PluTemplateFks = new();
        PluWeighings = new();
        PluNestingFks = new();
        Printers = new();
        PrinterResources = new();
        PrinterTypes = new();
        ProductionFacilities = new();
        ProductSeries = new();
        Scales = new();
        ScaleScreenShots = new();
        Tasks = new();
        TaskTypes = new();
        Templates = new();
        TemplateResources = new();
        Versions = new();
        WorkShops = new();
    }

    #endregion

    #region Public and private methods - GetListNotNullable

    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : SqlTableBase, new() => typeof(T) switch
    {
        var cls when cls == typeof(AccessModel) => GetListNotNullableAccesses(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(AppModel) => GetListNotNullableApps(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BarCodeModel) => GetListNotNullableBarCodes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BoxModel) => GetListNotNullableBoxes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BrandModel) => GetListNotNullableBrands(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(BundleModel) => GetListNotNullableBundles(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ClipModel) => GetListNotNullableClips(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(ContragentModel) => GetListNotNullableContragents(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceModel) => GetListNotNullableDevices(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceScaleFkModel) => GetListNotNullableDeviceScalesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceTypeModel) => GetListNotNullableDeviceTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(DeviceTypeFkModel) => GetListNotNullableDeviceTypesFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogTypeModel) => GetListNotNullableLogsTypes(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogModel) => GetListNotNullableLogs(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogWebModel) => GetListNotNullableLogsWebs(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(LogWebFkModel) => GetListNotNullableLogsWebsFks(sqlCrudConfig).Cast<T>().ToList(),
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
        var cls when cls == typeof(PluModel) => GetListNotNullablePlus(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluNestingFkModel) => GetListNotNullablePlusNestingFks(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluScaleModel) => GetListNotNullablePlusScales(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluStorageMethodModel) => GetListNotNullablePlusStoragesMethods(sqlCrudConfig).Cast<T>().ToList(),
        var cls when cls == typeof(PluStorageMethodFkModel) => GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig).Cast<T>().ToList(),
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
        _ => new()
    };

    public List<AccessModel> GetListNotNullableAccesses(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        Accesses = DataAccess.GetListNotNullable<AccessModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Accesses.Any())
            Accesses = Accesses.OrderByDescending(item => item.RightsEnum).ThenByDescending(item => item.LoginDt).ToList();
        return Accesses;
    }

    public List<AppModel> GetListNotNullableApps(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Apps = DataAccess.GetListNotNullable<AppModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Apps.Any())
            Apps = Apps.OrderBy(item => item.Name).ToList();
        return Apps;
    }

    public List<BarCodeModel> GetListNotNullableBarCodes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        BarCodes = DataAccess.GetListNotNullable<BarCodeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && BarCodes.Any())
            BarCodes = BarCodes.OrderByDescending(item => item.ChangeDt).ToList();
        return BarCodes;
    }

    public List<BoxModel> GetListNotNullableBoxes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Boxes = DataAccess.GetListNotNullable<BoxModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Boxes.Any())
            Boxes = Boxes.OrderBy(item => item.Name).ToList();
        return Boxes;
    }

    public List<BrandModel> GetListNotNullableBrands(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Brands = DataAccess.GetListNotNullable<BrandModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Brands.Any())
            Brands = Brands.OrderBy(item => item.Name).ToList();
        return Brands;
    }

    public List<BundleModel> GetListNotNullableBundles(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Bundles = DataAccess.GetListNotNullable<BundleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Bundles.Any())
            Bundles = Bundles.OrderBy(item => item.Name).ToList();
        return Bundles;
    }

    public List<ClipModel> GetListNotNullableClips(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Clips = DataAccess.GetListNotNullable<ClipModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Clips.Any())
            Clips = Clips.OrderBy(item => item.Name).ToList();
        return Clips;
    }

    public List<ContragentModel> GetListNotNullableContragents(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Contragents = DataAccess.GetListNotNullable<ContragentModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Contragents.Any())
            Contragents = Contragents.OrderBy(item => item.Name).ToList();
        return Contragents;
    }

    public List<DeviceModel> GetListNotNullableDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Devices = DataAccess.GetListNotNullable<DeviceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Devices.Any())
            Devices = Devices.OrderBy(item => item.Name).ToList();
        return Devices;
    }

    public List<DeviceScaleFkModel> GetListNotNullableDeviceScalesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(SqlTableBase.Name) ));
        DeviceScaleFks = DataAccess.GetListNotNullable<DeviceScaleFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && DeviceScaleFks.Any())
            DeviceScaleFks = DeviceScaleFks
                .OrderBy(item => item.Device.Name).ToList()
                .OrderBy(item => item.Scale.Name).ToList();
        return DeviceScaleFks;
    }

    public List<DeviceTypeModel> GetListNotNullableDeviceTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        DeviceTypes = DataAccess.GetListNotNullable<DeviceTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && DeviceTypes.Any())
            DeviceTypes = DeviceTypes.OrderBy(item => item.Name).ToList();
        return DeviceTypes;
    }

    public List<DeviceTypeFkModel> GetListNotNullableDeviceTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(SqlTableBase.Name)));
        DeviceTypeFks = DataAccess.GetListNotNullable<DeviceTypeFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && DeviceTypeFks.Any())
            DeviceTypeFks = DeviceTypeFks
                .OrderBy(item => item.Type.Name).ToList()
                .OrderBy(item => item.Device.Name).ToList();
        return DeviceTypeFks;
    }

    public List<LogTypeModel> GetListNotNullableLogsTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(LogTypeModel.Number) });
        LogTypes = DataAccess.GetListNotNullable<LogTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && LogTypes.Any())
            LogTypes = LogTypes.OrderBy(item => item.Number).ToList();
        return LogTypes;
    }

    public List<LogModel> GetListNotNullableLogs(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.CreateDt), Direction = SqlOrderDirection.Desc });
        Logs = DataAccess.GetListNotNullable<LogModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Logs.Any())
            Logs = Logs.OrderByDescending(item => item.CreateDt).ToList();
        return Logs;
    }

    public List<LogWebModel> GetListNotNullableLogsWebs(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.CreateDt) });
        LogsWebs = DataAccess.GetListNotNullable<LogWebModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && LogsWebs.Any())
            LogsWebs = LogsWebs.OrderBy(item => item.CreateDt).ToList();
        return LogsWebs;
    }

    public List<LogWebFkModel> GetListNotNullableLogsWebsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { 
        //        Name = $"{nameof(LogWebFkModel.LogWebRequest)}.{nameof(LogWebModel.CreateDt)}", Direction = SqlOrderDirection.Desc });
        LogsWebsFks = DataAccess.GetListNotNullable<LogWebFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && LogsWebsFks.Any())
            LogsWebsFks = LogsWebsFks.OrderByDescending(item => item.LogWebRequest.CreateDt).ToList();
        return LogsWebsFks;
    }

    public List<PluCharacteristicModel> GetListNotNullablePlusCharacteristics(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        NomenclaturesCharacteristics = DataAccess.GetListNotNullable<PluCharacteristicModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesCharacteristics.Any())
            NomenclaturesCharacteristics = NomenclaturesCharacteristics.OrderBy(item => item.Name).ToList();
        return NomenclaturesCharacteristics;
    }

    public List<PluCharacteristicsFkModel> GetListNotNullablePlusCharacteristicsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        return DataAccess.GetListNotNullable<PluCharacteristicsFkModel>(sqlCrudConfig);
    }

    public List<PluGroupModel> GetListNotNullablePlusGroups(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        NomenclaturesGroups = DataAccess.GetListNotNullable<PluGroupModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesGroups.Any())
            NomenclaturesGroups = NomenclaturesGroups.OrderBy(item => item.Name).ToList();
        return NomenclaturesGroups;
    }

    public List<PluGroupFkModel> GetListNotNullablePlusGroupFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(SqlTableBase.Name), SqlOrderDirection.Asc));
        NomenclaturesGroupsFk = DataAccess.GetListNotNullable<PluGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesGroupsFk.Any())
            NomenclaturesGroupsFk = NomenclaturesGroupsFk
                .OrderBy(item => item.PluGroup.Name).ToList()
                .OrderBy(item => item.Parent.Name).ToList();
        return NomenclaturesGroupsFk;
    }

    public List<OrderModel> GetListNotNullableOrders(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        Orders = DataAccess.GetListNotNullable<OrderModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Orders.Any())
            Orders = Orders.OrderByDescending(item => item.ChangeDt).ToList();
        return Orders;
    }

    public List<OrderWeighingModel> GetListNotNullableOrdersWeighings(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        OrderWeighings = DataAccess.GetListNotNullable<OrderWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && OrderWeighings.Any())
            OrderWeighings = OrderWeighings.OrderByDescending(item => item.ChangeDt).ToList();
        return OrderWeighings;
    }

    public List<OrganizationModel> GetListNotNullableOrganizations(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Organizations = DataAccess.GetListNotNullable<OrganizationModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Organizations.Any())
            Organizations = Organizations.OrderBy(item => item.Name).ToList();
        return Organizations;
    }

    public List<PluLabelModel> GetListNotNullablePluLabels(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        PluLabels = DataAccess.GetListNotNullable<PluLabelModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluLabels.Any())
            PluLabels = PluLabels.OrderByDescending(item => item.ChangeDt).ToList();
        return PluLabels;
    }

    public List<PluModel> GetListNotNullablePlus(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(PluModel.Number) });
        Plus = DataAccess.GetListNotNullable<PluModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Plus.Any())
            Plus = Plus.OrderBy(item => item.Number).ToList();
        return Plus;
    }

    public List<PluFkModel> GetListNotNullablePlusFks(SqlCrudConfigModel sqlCrudConfig)
    {
        PlusFks = DataAccess.GetListNotNullable<PluFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Plus.Any())
            PlusFks = PlusFks.OrderBy(item => item.Plu.Number).ToList();
        return PlusFks;
    }

    public List<PluBrandFkModel> GetListNotNullablePlusBrandsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(SqlTableBase.ClearNullProperties), SqlOrderDirection.Asc));
        PluBrandFks = DataAccess.GetListNotNullable<PluBrandFkModel>(sqlCrudConfig);
        if (PluBrandFks.Any())
        {
            PluBrandFkModel bundleFk = PluBrandFks.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = DataAccess.GetItemNewEmpty<PluModel>();
            if (bundleFk.Brand.IsNew)
                bundleFk.Brand = DataAccess.GetItemNewEmpty<BrandModel>();
        }
        if (sqlCrudConfig.IsResultOrder && PluBrandFks.Any())
            PluBrandFks = PluBrandFks.OrderBy(item => item.Brand.Name).ToList();
        return PluBrandFks;
    }

    public List<PluBundleFkModel> GetListNotNullablePlusBundlesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluBundleFkModel.Bundle)}.{nameof(BundleModel.Name)}", SqlOrderDirection.Asc));
        PluBundleFks = DataAccess.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig);
        if (PluBundleFks.Count > 0)
        {
            PluBundleFkModel bundleFk = PluBundleFks.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = DataAccess.GetItemNewEmpty<PluModel>();
            if (bundleFk.Bundle.IsNew)
                bundleFk.Bundle = DataAccess.GetItemNewEmpty<BundleModel>();
        }
        if (sqlCrudConfig.IsResultOrder && PluBundleFks.Any())
            PluBundleFks = PluBundleFks.OrderBy(item => item.Bundle.Name).ToList();
        return PluBundleFks;
    }

    public List<PluClipFkModel> GetListNotNullablePlusClipsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluClipFkModel.Clip)}.{nameof(ClipModel.Name)}", SqlOrderDirection.Asc));
        PluClipFks = DataAccess.GetListNotNullable<PluClipFkModel>(sqlCrudConfig);
        if (PluClipFks.Count > 0)
        {
            PluClipFkModel pluClipFk = PluClipFks.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = DataAccess.GetItemNewEmpty<PluModel>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = DataAccess.GetItemNewEmpty<ClipModel>();
        }
        if (sqlCrudConfig.IsResultOrder && PluClipFks.Any())
            PluClipFks = PluClipFks.OrderBy(item => item.Clip.Name).ToList();
        return PluClipFks;
    }

    public List<PluScaleModel> GetListNotNullablePlusScales(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        PluScales = DataAccess.GetListNotNullable<PluScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluScales.Any())
            PluScales = PluScales
                .OrderBy(item => item.Plu.Number).ToList();
        return PluScales;
    }

    public List<PluStorageMethodModel> GetListNotNullablePlusStoragesMethods(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        PluStorageMethods = DataAccess.GetListNotNullable<PluStorageMethodModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluStorageMethods.Any())
            PluStorageMethods = PluStorageMethods
                .OrderBy(item => item.Name).ToList();
        return PluStorageMethods;
    }

    public List<PluStorageMethodFkModel> GetListNotNullablePlusStoragesMethodsFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        PluStorageMethodsFks = DataAccess.GetListNotNullable<PluStorageMethodFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluStorageMethodsFks.Any())
            PluStorageMethodsFks = PluStorageMethodsFks
                .OrderBy(item => item.Plu.Number).ToList();
        return PluStorageMethodsFks;
    }

    public List<PluTemplateFkModel> GetListNotNullablePlusTemplatesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new($"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Number)}", SqlOrderDirection.Asc));
        PluTemplateFks = DataAccess.GetListNotNullable<PluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluTemplateFks.Any())
            PluTemplateFks = PluTemplateFks
                .OrderBy(item => item.Template.Title).ToList()
                .OrderBy(item => item.Plu.Name).ToList();
        return PluTemplateFks;
    }

    public List<PluWeighingModel> GetListNotNullablePlusWeighings(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        PluWeighings = DataAccess.GetListNotNullable<PluWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluWeighings.Any())
            PluWeighings = PluWeighings.OrderByDescending(item => item.ChangeDt).ToList();
        return PluWeighings;
    }

    public List<PluNestingFkModel> GetListNotNullablePlusNestingFks(SqlCrudConfigModel sqlCrudConfig)
    {
        PluNestingFks = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
        {
            PluNestingFks.Add(DataAccess.GetItemNewEmpty<PluNestingFkModel>());
        }
        object[] objects = DataAccess.GetArrayObjectsNotNullable(sqlCrudConfig);
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
                        pluBundle.Plu.Uid1c = pluUid1c;
                    if (Guid.TryParse(Convert.ToString(item[43]), out Guid boxUid1c))
                        box.Uid1c = boxUid1c;
                    if (Guid.TryParse(Convert.ToString(item[44]), out Guid bundleUid1c))
                        pluBundle.Bundle.Uid1c = bundleUid1c;
                    // All.
                    PluNestingFks.Add(new()
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
        return PluNestingFks;
    }

    public List<PrinterModel> GetListNotNullablePrinters(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Printers = DataAccess.GetListNotNullable<PrinterModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Printers.Any())
            Printers = Printers.OrderBy(item => item.Name).ToList();
        return Printers;
    }

    public List<PrinterResourceFkModel> GetListNotNullablePrintersResources(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(SqlTableBase.Name), SqlOrderDirection.Asc));
        PrinterResources = DataAccess.GetListNotNullable<PrinterResourceFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PrinterResources.Any())
            PrinterResources = PrinterResources
                .OrderBy(item => item.Printer.Name).ToList()
                .OrderBy(item => item.TemplateResource.Name).ToList();
        return PrinterResources;
    }

    public List<PrinterTypeModel> GetListNotNullablePrintersTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        PrinterTypes = DataAccess.GetListNotNullable<PrinterTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PrinterTypes.Any())
            PrinterTypes = PrinterTypes.OrderBy(item => item.Name).ToList();
        return PrinterTypes;
    }

    public List<ProductionFacilityModel> GetListNotNullableProductionFacilities(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        ProductionFacilities = DataAccess.GetListNotNullable<ProductionFacilityModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && ProductionFacilities.Any())
            ProductionFacilities = ProductionFacilities.OrderBy(item => item.Name).ToList();
        return ProductionFacilities;
    }

    public List<ProductSeriesModel> GetListNotNullableProductSeries(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.CreateDt), Direction = SqlOrderDirection.Desc });
        ProductSeries = DataAccess.GetListNotNullable<ProductSeriesModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && ProductSeries.Any())
            ProductSeries = ProductSeries.OrderByDescending(item => item.CreateDt).ToList();
        return ProductSeries;
    }

    public List<ScaleModel> GetListNotNullableScales(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Description) });
        Scales = DataAccess.GetListNotNullable<ScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Scales.Any())
            Scales = Scales.OrderBy(item => item.Description).ToList();
        return Scales;
    }

    public List<ScaleScreenShotModel> GetListNotNullableScaleScreenShots(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.ChangeDt), Direction = SqlOrderDirection.Desc });
        ScaleScreenShots = DataAccess.GetListNotNullable<ScaleScreenShotModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && ScaleScreenShots.Any())
            ScaleScreenShots = ScaleScreenShots.OrderByDescending(item => item.ChangeDt).ToList();
        return ScaleScreenShots;
    }

    public List<TaskModel> GetListNotNullableTasks(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        Tasks = DataAccess.GetListNotNullable<TaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Tasks.Any())
            Tasks = Tasks.OrderBy(item => item.Scale.Description).ToList();
        return Tasks;
    }

    public List<TaskTypeModel> GetListNotNullableTasksTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        TaskTypes = DataAccess.GetListNotNullable<TaskTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && TaskTypes.Any())
            TaskTypes = TaskTypes.OrderBy(item => item.Name).ToList();
        return TaskTypes;
    }

    public List<TemplateModel> GetListNotNullableTemplates(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(TemplateModel.Title) });
        Templates = DataAccess.GetListNotNullable<TemplateModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Templates.Any())
            Templates = Templates.OrderBy(item => item.Title).ToList();
        return Templates;
    }

    public List<TemplateResourceModel> GetListNotNullableTemplateResources(SqlCrudConfigModel sqlCrudConfig)
    {
        //if (sqlCrudConfig.IsResultOrder)
        //    sqlCrudConfig.AddOrders(new(nameof(SqlTableBase.Name), SqlOrderDirection.Asc));
        TemplateResources = DataAccess.GetListNotNullable<TemplateResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && TemplateResources.Any())
            TemplateResources = TemplateResources
                .OrderBy(item => item.Name)
                .OrderBy(item => item.Type).ToList();
        return TemplateResources;
    }

    public List<VersionModel> GetListNotNullableVersions(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(VersionModel.Version), Direction = SqlOrderDirection.Desc });
        Versions = DataAccess.GetListNotNullable<VersionModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Versions.Any())
            Versions = Versions.OrderByDescending(item => item.Version).ToList();
        return Versions;
    }

    public List<WorkShopModel> GetListNotNullableWorkShops(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(SqlTableBase.Name) });
        WorkShops = DataAccess.GetListNotNullable<WorkShopModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && WorkShops.Any())
            WorkShops = WorkShops.OrderBy(item => item.Name).ToList();
        return WorkShops;
    }

    #endregion

    #region Public and private methods

    public T? GetItemNullable<T>(object? value) where T : SqlTableBase, new() => DataAccess.GetItemNullable<T>(value);

    [Obsolete(@"Use GetItemNotNullable(SqlFieldIdentityModel) or GetItemNullableByUid(Guid?) or GetItemNullableById(long?)")]
    public T GetItemNotNullable<T>(object? value) where T : SqlTableBase, new() => DataAccess.GetItemNotNullable<T>(value);

    public T? GetItemNullable<T>(SqlFieldIdentityModel identity) where T : SqlTableBase, new() => 
        DataAccess.GetItemNullable<T>(identity);

    public T GetItemNotNullable<T>(SqlFieldIdentityModel identity) where T : SqlTableBase, new() => 
        DataAccess.GetItemNotNullable<T>(identity);

    public T? GetItemNullableByUid<T>(Guid? uid) where T : SqlTableBase, new() => 
        DataAccess.GetItemNullableByUid<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : SqlTableBase, new() => 
        DataAccess.GetItemNotNullableByUid<T>(uid);

    public T? GetItemNullableById<T>(long? id) where T : SqlTableBase, new() => 
        DataAccess.GetItemNullableById<T>(id);

    public T GetItemNotNullableById<T>(long id) where T : SqlTableBase, new() => 
        DataAccess.GetItemNotNullableById<T>(id);

    /// <summary>
    /// List of tables models.
    /// </summary>
    /// <returns></returns>
    public List<SqlTableBase> GetTableModels() => new()
    {
        new AccessModel(),
        new AppModel(),
        new BarCodeModel(),
        new BoxModel(),
        new BrandModel(),
        new BundleModel(),
        new ClipModel(),
        new ContragentModel(),
        new DeviceModel(),
        new DeviceScaleFkModel(),
        new DeviceTypeFkModel(),
        new DeviceTypeModel(),
        new LogModel(),
        new LogTypeModel(),
        new LogWebModel(),
        new LogWebFkModel(),
        new OrderModel(),
        new OrderWeighingModel(),
        new OrganizationModel(),
        new PluBundleFkModel(),
        new PluCharacteristicModel(),
        new PluCharacteristicsFkModel(),
        new PluFkModel(),
        new PluGroupFkModel(),
        new PluGroupModel(),
        new PluLabelModel(),
        new PluModel(),
        new PluScaleModel(),
        new PluTemplateFkModel(),
        new PluWeighingModel(),
        new PrinterModel(),
        new PrinterResourceFkModel(),
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
        typeof(AccessModel),
        typeof(AppModel),
        typeof(BarCodeModel),
        typeof(BoxModel),
        typeof(BrandModel),
        typeof(BundleModel),
        typeof(ContragentModel),
        typeof(DeviceModel),
        typeof(DeviceScaleFkModel),
        typeof(DeviceTypeFkModel),
        typeof(DeviceTypeModel),
        typeof(LogModel),
        typeof(LogTypeModel),
        typeof(LogWebModel),
        typeof(LogWebFkModel),
        typeof(OrderModel),
        typeof(OrderWeighingModel),
        typeof(OrganizationModel),
        typeof(PluBundleFkModel),
        typeof(PluCharacteristicModel),
        typeof(PluCharacteristicsFkModel),
        typeof(PluFkModel),
        typeof(PluGroupFkModel),
        typeof(PluGroupModel),
        typeof(PluLabelModel),
        typeof(PluModel),
        typeof(PluScaleModel),
        typeof(PluTemplateFkModel),
        typeof(PluWeighingModel),
        typeof(PrinterModel),
        typeof(PrinterResourceFkModel),
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
        typeof(AccessMap),
        typeof(AppMap),
        typeof(BarCodeMap),
        typeof(BoxMap),
        typeof(BrandMap),
        typeof(BundleMap),
        typeof(ClipMap),
        typeof(ContragentMap),
        typeof(DeviceMap),
        typeof(DeviceScaleFkMap),
        typeof(DeviceTypeFkMap),
        typeof(DeviceTypeMap),
        typeof(LogMap),
        typeof(LogTypeMap),
        typeof(LogWebMap),
        typeof(LogWebFkMap),
        typeof(OrderMap),
        typeof(OrderWeighingMap),
        typeof(OrganizationMap),
        typeof(PluBrandFkMap),
        typeof(PluBundleFkMap),
        typeof(PluCharacteristicMap),
        typeof(PluCharacteristicsFkMap),
        typeof(PluFkMap),
        typeof(PluGroupFkMap),
        typeof(PluGroupMap),
        typeof(PluLabelMap),
        typeof(PluMap),
        typeof(PluScaleMap),
        typeof(PluTemplateFkMap),
        typeof(PluWeighingMap),
        typeof(PrinterMap),
        typeof(PrinterResourceFkMap),
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
        typeof(AccessValidator),
        typeof(AppValidator),
        typeof(BarCodeValidator),
        typeof(BoxValidator),
        typeof(BrandValidator),
        typeof(BundleValidator),
        typeof(ClipValidator),
        typeof(ContragentValidator),
        typeof(DeviceScaleFkValidator),
        typeof(DeviceTypeFkValidator),
        typeof(DeviceTypeValidator),
        typeof(DeviceValidator),
        typeof(LogValidator),
        typeof(LogTypeValidator),
        typeof(LogWebValidator),
        typeof(LogWebFkValidator),
        typeof(OrderValidator),
        typeof(OrderWeighingValidator),
        typeof(OrganizationValidator),
        typeof(PluBundleFkValidator),
        typeof(PluCharacteristicsFkValidator),
        typeof(PluCharacteristicValidator),
        typeof(PluFkValidator),
        typeof(PluGroupFkValidator),
        typeof(PluGroupValidator),
        typeof(PluLabelValidator),
        typeof(PluScaleValidator),
        typeof(PluTemplateFkValidator),
        typeof(PluValidator),
        typeof(PluWeighingValidator),
        typeof(PrinterResourceFkValidator),
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

    public string GetTableModelName<T>() where T : SqlTableBase, new()
    {
        return typeof(T) switch
        {
            var cls when cls == typeof(AccessModel) => nameof(AccessModel),
            var cls when cls == typeof(AppModel) => nameof(AppModel),
            var cls when cls == typeof(BarCodeModel) => nameof(BarCodeModel),
            var cls when cls == typeof(BoxModel) => nameof(BoxModel),
            var cls when cls == typeof(BrandModel) => nameof(BrandModel),
            var cls when cls == typeof(BundleModel) => nameof(BundleModel),
            var cls when cls == typeof(ClipModel) => nameof(ClipModel),
            var cls when cls == typeof(ContragentModel) => nameof(ContragentModel),
            var cls when cls == typeof(DeviceModel) => nameof(DeviceModel),
            var cls when cls == typeof(DeviceScaleFkModel) => nameof(DeviceScaleFkModel),
            var cls when cls == typeof(DeviceTypeFkModel) => nameof(DeviceTypeFkModel),
            var cls when cls == typeof(DeviceTypeModel) => nameof(DeviceTypeModel),
            var cls when cls == typeof(LogModel) => nameof(LogModel),
            var cls when cls == typeof(LogTypeModel) => nameof(LogTypeModel),
            var cls when cls == typeof(LogWebModel) => nameof(LogWebModel),
            var cls when cls == typeof(LogWebFkModel) => nameof(LogWebFkModel),
            var cls when cls == typeof(OrderModel) => nameof(OrderModel),
            var cls when cls == typeof(OrderWeighingModel) => nameof(OrderWeighingModel),
            var cls when cls == typeof(OrganizationModel) => nameof(OrganizationModel),
            var cls when cls == typeof(PluBundleFkModel) => nameof(PluBundleFkModel),
            var cls when cls == typeof(PluCharacteristicModel) => nameof(PluCharacteristicModel),
            var cls when cls == typeof(PluCharacteristicsFkModel) => nameof(PluCharacteristicsFkModel),
            var cls when cls == typeof(PluFkModel) => nameof(PluFkModel),
            var cls when cls == typeof(PluGroupFkModel) => nameof(PluGroupFkModel),
            var cls when cls == typeof(PluGroupModel) => nameof(PluGroupModel),
            var cls when cls == typeof(PluLabelModel) => nameof(PluLabelModel),
            var cls when cls == typeof(PluModel) => nameof(PluModel),
            var cls when cls == typeof(PluScaleModel) => nameof(PluScaleModel),
            var cls when cls == typeof(PluTemplateFkModel) => nameof(PluTemplateFkModel),
            var cls when cls == typeof(PluWeighingModel) => nameof(PluWeighingModel),
            var cls when cls == typeof(PrinterModel) => nameof(PrinterModel),
            var cls when cls == typeof(PrinterResourceFkModel) => nameof(PrinterResourceFkModel),
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

    #endregion
}