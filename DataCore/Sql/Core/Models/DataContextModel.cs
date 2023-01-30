// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Helpers;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
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
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace DataCore.Sql.Core.Models;

public class DataContextModel
{
    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public List<AccessModel> Accesses { get; set; }
    public List<AppModel> Apps { get; set; }
    public List<BarCodeModel> BarCodes { get; set; }
    public List<BoxModel> Boxes { get; set; }
    public List<BrandModel> Brands { get; set; }
    public List<BundleModel> Bundles { get; set; }
    public List<ClipModel> Clips { get; set; }
    public List<ContragentModel> Contragents { get; set; }
    public List<DeviceModel> Devices { get; set; }
    public List<DeviceTypeModel> DeviceTypes { get; set; }
    public List<DeviceTypeFkModel> DeviceTypeFks { get; set; }
    public List<DeviceScaleFkModel> DeviceScaleFks { get; set; }
    public List<LogModel> Logs { get; set; }
    public List<LogTypeModel> LogTypes { get; set; }
    public List<NomenclatureModel> NomenclatureDeprecated { get; set; }
    public List<NomenclatureV2Model> NomenclaturesV2 { get; set; }
    public List<NomenclatureGroupModel> NomenclaturesGroups { get; set; }
    public List<NomenclaturesGroupFkModel> NomenclaturesGroupsFk { get; set; }
    public List<NomenclaturesCharacteristicsModel> NomenclaturesCharacteristics { get; set; }
    public List<NomenclaturesCharacteristicsFkModel> NomenclaturesCharacteristicsFk { get; set; }
    public List<OrderModel> Orders { get; set; }
    public List<OrderWeighingModel> OrderWeighings { get; set; }
    public List<OrganizationModel> Organizations { get; set; }
    public List<PluLabelModel> PluLabels { get; set; }
    public List<PluModel> Plus { get; set; }
    public List<PluBundleFkModel> PluBundleFks { get; set; }
    public List<PluClipFkModel> PluClipFks { get; set; }
    public List<PluScaleModel> PluScales { get; set; }
    public List<PluTemplateFkModel> PluTemplateFks { get; set; }
    public List<PluWeighingModel> PluWeighings { get; set; }
    public List<PluNestingFkModel> PluNestingFks { get; set; }
    public List<PrinterModel> Printers { get; set; }
    public List<PrinterResourceModel> PrinterResources { get; set; }
    public List<PrinterTypeModel> PrinterTypes { get; set; }
    public List<ProductionFacilityModel> ProductionFacilities { get; set; }
    public List<ProductSeriesModel> ProductSeries { get; set; }
    public List<ScaleModel> Scales { get; set; }
    public List<ScaleScreenShotModel> ScaleScreenShots { get; set; }
    public List<TaskModel> Tasks { get; set; }
    public List<TaskTypeModel> TaskTypes { get; set; }
    public List<TemplateModel> Templates { get; set; }
    public List<TemplateResourceModel> TemplateResources { get; set; }
    public List<VersionModel> Versions { get; set; }
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
        NomenclatureDeprecated = new();
        NomenclaturesV2 = new();
        NomenclaturesGroups = new();
        NomenclaturesGroupsFk = new();
        NomenclaturesCharacteristics = new();
        NomenclaturesCharacteristicsFk = new();
        Orders = new();
        OrderWeighings = new();
        Organizations = new();
        PluLabels = new();
        Plus = new();
        PluBundleFks = new();
        PluClipFks = new();
        PluScales = new();
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

    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new() => typeof(T) switch
    {
        var cls when cls == typeof(AccessModel) => GetListNotNullableAccesses<T>(sqlCrudConfig),
        var cls when cls == typeof(AppModel) => GetListNotNullableApps<T>(sqlCrudConfig),
        var cls when cls == typeof(BarCodeModel) => GetListNotNullableBarCodes<T>(sqlCrudConfig),
        var cls when cls == typeof(BoxModel) => GetListNotNullableBoxes<T>(sqlCrudConfig),
        var cls when cls == typeof(BundleModel) => GetListNotNullableBundles<T>(sqlCrudConfig),
        var cls when cls == typeof(BrandModel) => GetListNotNullableBrands<T>(sqlCrudConfig),
        var cls when cls == typeof(ClipModel) => GetListNotNullableClips<T>(sqlCrudConfig),
        var cls when cls == typeof(ContragentModel) => GetListNotNullableContragents<T>(sqlCrudConfig),
        var cls when cls == typeof(DeviceModel) => GetListNotNullableDevices<T>(sqlCrudConfig),
        var cls when cls == typeof(DeviceScaleFkModel) => GetListNotNullableDeviceScalesFks<T>(sqlCrudConfig),
        var cls when cls == typeof(DeviceTypeFkModel) => GetListNotNullableDeviceTypeFks<T>(sqlCrudConfig),
        var cls when cls == typeof(DeviceTypeModel) => GetListNotNullableDeviceTypes<T>(sqlCrudConfig),
        var cls when cls == typeof(LogModel) => GetListNotNullableLogs<T>(sqlCrudConfig),
        var cls when cls == typeof(LogTypeModel) => GetListNotNullableLogTypes<T>(sqlCrudConfig),
        var cls when cls == typeof(NomenclatureGroupModel) => GetListNotNullableNomenclatureGroups<T>(sqlCrudConfig),
        var cls when cls == typeof(NomenclatureModel) => GetListNotNullableNomenclatures<T>(sqlCrudConfig),
        var cls when cls == typeof(NomenclaturesCharacteristicsFkModel) => GetListNotNullableNomenclatureCharacteristicFks<T>(sqlCrudConfig),
        var cls when cls == typeof(NomenclaturesCharacteristicsModel) => GetListNotNullableNomenclatureCharacteristics<T>(sqlCrudConfig),
        var cls when cls == typeof(NomenclaturesGroupFkModel) => GetListNotNullableNomenclatureGroupFks<T>(sqlCrudConfig),
        var cls when cls == typeof(NomenclatureV2Model) => GetListNotNullableNomenclaturesV2<T>(sqlCrudConfig),
        var cls when cls == typeof(OrderModel) => GetListNotNullableOrders<T>(sqlCrudConfig),
        var cls when cls == typeof(OrderWeighingModel) => GetListNotNullableOrderWeighings<T>(sqlCrudConfig),
        var cls when cls == typeof(OrganizationModel) => GetListNotNullableOrganizations<T>(sqlCrudConfig),
        var cls when cls == typeof(PluBundleFkModel) => GetListNotNullablePluBundleFks<T>(sqlCrudConfig),
        var cls when cls == typeof(PluClipFkModel) => GetListNotNullablePluClipFks<T>(sqlCrudConfig),
        var cls when cls == typeof(PluLabelModel) => GetListNotNullablePluLabels<T>(sqlCrudConfig),
        var cls when cls == typeof(PluModel) => GetListNotNullablePlus<T>(sqlCrudConfig),
        var cls when cls == typeof(PluNestingFkModel) => GetListNotNullablePluNestingFks<T>(sqlCrudConfig),
        var cls when cls == typeof(PluScaleModel) => GetListNotNullablePluScales<T>(sqlCrudConfig),
        var cls when cls == typeof(PluTemplateFkModel) => GetListNotNullablePluTemplateFks<T>(sqlCrudConfig),
        var cls when cls == typeof(PluWeighingModel) => GetListNotNullablePluWeighings<T>(sqlCrudConfig),
        var cls when cls == typeof(PrinterModel) => GetListNotNullablePrinters<T>(sqlCrudConfig),
        var cls when cls == typeof(PrinterResourceModel) => GetListNotNullablePrinterResources<T>(sqlCrudConfig),
        var cls when cls == typeof(PrinterTypeModel) => GetListNotNullablePrinterTypes<T>(sqlCrudConfig),
        var cls when cls == typeof(ProductionFacilityModel) => GetListNotNullableProductionFacilities<T>(sqlCrudConfig),
        var cls when cls == typeof(ProductSeriesModel) => GetListNotNullableProductSeries<T>(sqlCrudConfig),
        var cls when cls == typeof(ScaleModel) => GetListNotNullableScales<T>(sqlCrudConfig),
        var cls when cls == typeof(ScaleScreenShotModel) => GetListNotNullableScaleScreenShots<T>(sqlCrudConfig),
        var cls when cls == typeof(TaskModel) => GetListNotNullableTasks<T>(sqlCrudConfig),
        var cls when cls == typeof(TaskTypeModel) => GetListNotNullableTaskTypes<T>(sqlCrudConfig),
        var cls when cls == typeof(TemplateModel) => GetListNotNullableTemplates<T>(sqlCrudConfig),
        var cls when cls == typeof(TemplateResourceModel) => GetListNotNullableTemplateResources<T>(sqlCrudConfig),
        var cls when cls == typeof(VersionModel) => GetListNotNullableVersions<T>(sqlCrudConfig),
        var cls when cls == typeof(WorkShopModel) => GetListNotNullableWorkShops<T>(sqlCrudConfig),
        _ => new()
    };

    private List<T> GetListNotNullableAccesses<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Accesses = DataAccess.GetListNotNullable<AccessModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Accesses.Count > 1)
            Accesses = Accesses.OrderByDescending(item => item.RightsEnum).ThenByDescending(item => item.LoginDt).ToList();
        return Accesses.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableApps<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Apps = DataAccess.GetListNotNullable<AppModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Apps.Count > 1)
            Apps = Apps.OrderBy(item => item.Name).ToList();
        return Apps.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableBarCodes<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        BarCodes = DataAccess.GetListNotNullable<BarCodeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && BarCodes.Count > 1)
            BarCodes = BarCodes.OrderByDescending(item => item.ChangeDt).ToList();
        return BarCodes.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableBoxes<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Boxes = DataAccess.GetListNotNullable<BoxModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Boxes.Count > 1)
            Boxes = Boxes.OrderBy(item => item.Name).ToList();
        return Boxes.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableBrands<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Brands = DataAccess.GetListNotNullable<BrandModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Brands.Count > 1)
            Brands = Brands.OrderBy(item => item.Name).ToList();
        return Brands.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableBundles<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Bundles = DataAccess.GetListNotNullable<BundleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Bundles.Count > 1)
            Bundles = Bundles.OrderBy(item => item.Name).ToList();
        return Bundles.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableClips<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Clips = DataAccess.GetListNotNullable<ClipModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Bundles.Count > 1)
            Clips = Clips.OrderBy(item => item.Name).ToList();
        return Clips.Cast<T>().ToList();
    }
    
    private List<T> GetListNotNullableContragents<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Contragents = DataAccess.GetListNotNullable<ContragentModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Contragents.Count > 1)
            Contragents = Contragents.OrderBy(item => item.Name).ToList();
        return Contragents.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableDevices<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Devices = DataAccess.GetListNotNullable<DeviceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Devices.Count > 1)
            Devices = Devices.OrderBy(item => item.Name).ToList();
        return Devices.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableDeviceTypes<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        DeviceTypes = DataAccess.GetListNotNullable<DeviceTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && DeviceTypes.Count > 1)
            DeviceTypes = DeviceTypes.OrderBy(item => item.Name).ToList();
        return DeviceTypes.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableDeviceTypeFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        DeviceTypeFks = DataAccess.GetListNotNullable<DeviceTypeFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && DeviceTypeFks.Count > 1)
            DeviceTypeFks = DeviceTypeFks
                .OrderBy(item => item.Type.Name).ToList()
                .OrderBy(item => item.Device.Name).ToList();
        return DeviceTypeFks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableDeviceScalesFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        DeviceScaleFks = DataAccess.GetListNotNullable<DeviceScaleFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && DeviceScaleFks.Count > 1)
            DeviceScaleFks = DeviceScaleFks
                .OrderBy(item => item.Device.Name).ToList()
                .OrderBy(item => item.Scale.Name).ToList();
        return DeviceScaleFks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableLogs<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Logs = DataAccess.GetListNotNullable<LogModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Logs.Count > 1)
            Logs = Logs.OrderByDescending(item => item.ChangeDt).ToList();
        return Logs.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableLogTypes<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        LogTypes = DataAccess.GetListNotNullable<LogTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && LogTypes.Count > 1)
            LogTypes = LogTypes.OrderBy(item => item.Name).ToList();
        return LogTypes.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableNomenclatures<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        NomenclatureDeprecated = DataAccess.GetListNotNullable<NomenclatureModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclatureDeprecated.Count > 1)
            NomenclatureDeprecated = NomenclatureDeprecated.OrderBy(item => item.Name).ToList();
        return NomenclatureDeprecated.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableNomenclaturesV2<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        NomenclaturesV2 = DataAccess.GetListNotNullable<NomenclatureV2Model>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesV2.Count > 1)
            NomenclaturesV2 = NomenclaturesV2.OrderBy(item => item.Name).ToList();
        return NomenclaturesV2.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableNomenclatureCharacteristics<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        NomenclaturesCharacteristics = DataAccess.GetListNotNullable<NomenclaturesCharacteristicsModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesCharacteristics.Count > 1)
            NomenclaturesCharacteristics = NomenclaturesCharacteristics.OrderBy(item => item.Name).ToList();
        return NomenclaturesCharacteristics.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableNomenclatureCharacteristicFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        return DataAccess.GetListNotNullable<NomenclaturesCharacteristicsFkModel>(sqlCrudConfig).Cast<T>().ToList();
    }

    private List<T> GetListNotNullableNomenclatureGroups<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        NomenclaturesGroups = DataAccess.GetListNotNullable<NomenclatureGroupModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesGroups.Count > 1)
            NomenclaturesGroups = NomenclaturesGroups.OrderBy(item => item.Name).ToList();
        return NomenclaturesGroups.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableNomenclatureGroupFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        NomenclaturesGroupsFk = DataAccess.GetListNotNullable<NomenclaturesGroupFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && NomenclaturesGroupsFk.Count > 1)
            NomenclaturesGroupsFk = NomenclaturesGroupsFk
                .OrderBy(item => item.NomenclatureGroup.Name).ToList()
                .OrderBy(item => item.NomenclatureGroupParent.Name).ToList();
        return NomenclaturesGroupsFk.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableOrders<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Orders = DataAccess.GetListNotNullable<OrderModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Orders.Count > 1)
            Orders = Orders.OrderByDescending(item => item.ChangeDt).ToList();
        return Orders.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableOrderWeighings<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        OrderWeighings = DataAccess.GetListNotNullable<OrderWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && OrderWeighings.Count > 1)
            OrderWeighings = OrderWeighings.OrderByDescending(item => item.ChangeDt).ToList();
        return OrderWeighings.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableOrganizations<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Organizations = DataAccess.GetListNotNullable<OrganizationModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Organizations.Count > 1)
            Organizations = Organizations.OrderBy(item => item.Name).ToList();
        return Organizations.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluLabels<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluLabels = DataAccess.GetListNotNullable<PluLabelModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluLabels.Count > 1)
            PluLabels = PluLabels.OrderByDescending(item => item.ChangeDt).ToList();
        return PluLabels.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePlus<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Plus = DataAccess.GetListNotNullable<PluModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Plus.Count > 1)
            Plus = Plus.OrderBy(item => item.Number).ToList();
        return Plus.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluBundleFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluBundleFks = DataAccess.GetListNotNullable<PluBundleFkModel>(sqlCrudConfig);
        if (PluBundleFks.Count > 0)
        {
            PluBundleFkModel bundleFk = PluBundleFks.First();
            if (bundleFk.Plu.IsNew)
                bundleFk.Plu = DataAccess.GetItemNewEmpty<PluModel>();
            if (bundleFk.Bundle.IsNew)
                bundleFk.Bundle = DataAccess.GetItemNewEmpty<BundleModel>();
        }

        if (sqlCrudConfig.IsResultOrder && PluBundleFks.Count > 1)
            PluBundleFks = PluBundleFks.OrderBy(item => item.Bundle.Name).ToList();
        return PluBundleFks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluClipFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluClipFks = DataAccess.GetListNotNullable<PluClipFkModel>(sqlCrudConfig);
        if (PluClipFks.Count > 0)
        {
            PluClipFkModel pluClipFk = PluClipFks.First();
            if (pluClipFk.Plu.IsNew)
                pluClipFk.Plu = DataAccess.GetItemNewEmpty<PluModel>();
            if (pluClipFk.Clip.IsNew)
                pluClipFk.Clip = DataAccess.GetItemNewEmpty<ClipModel>();
        }

        if (sqlCrudConfig.IsResultOrder && PluClipFks.Count > 1)
            PluClipFks = PluClipFks.OrderBy(item => item.Clip.Name).ToList();
        return PluClipFks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluScales<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluScales = DataAccess.GetListNotNullable<PluScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluScales.Count > 1)
            PluScales = PluScales
                .OrderBy(item => item.Plu.Number).ToList();
        return PluScales.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluTemplateFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluTemplateFks = DataAccess.GetListNotNullable<PluTemplateFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluTemplateFks.Count > 1)
            PluTemplateFks = PluTemplateFks
                .OrderBy(item => item.Template.Title).ToList()
                .OrderBy(item => item.Plu.Name).ToList();
        return PluTemplateFks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluWeighings<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluWeighings = DataAccess.GetListNotNullable<PluWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PluWeighings.Count > 1)
            PluWeighings = PluWeighings.OrderByDescending(item => item.ChangeDt).ToList();
        return PluWeighings.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePluNestingFks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PluNestingFks = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
        {
            PluNestingFks.Add(DataAccess.GetItemNewEmpty<PluNestingFkModel>());
        }
        object[] objects = DataAccess.GetArrayObjectsNotNullable(sqlCrudConfig);
        foreach (object obj in objects)
        {
            if (obj is object[] { Length: 43 } item)
            {
                if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                {
                    PluBundleFkModel pluBundle = new();
                    if (Guid.TryParse(Convert.ToString(item[11]), out Guid pluBundleUid))
                    {
                        pluBundle.IdentityValueUid = pluBundleUid;
                        pluBundle.CreateDt = Convert.ToDateTime(item[12]);
                        pluBundle.ChangeDt = Convert.ToDateTime(item[13]);
                        pluBundle.IsMarked = Convert.ToBoolean(item[14]);
                    }
                    if (Guid.TryParse(Convert.ToString(item[17]), out Guid pluUid))
                    {
                        pluBundle.Plu.IdentityValueUid = pluUid;
                        pluBundle.Plu.CreateDt = Convert.ToDateTime(item[18]);
                        pluBundle.Plu.ChangeDt = Convert.ToDateTime(item[19]);
                        pluBundle.Plu.IsMarked = Convert.ToBoolean(item[20]);
                        pluBundle.Plu.Number = Convert.ToUInt16(item[21]);
                        pluBundle.Plu.Name = Convert.ToString(item[22]);
                        pluBundle.Plu.FullName = Convert.ToString(item[23]);
                        pluBundle.Plu.Description = Convert.ToString(item[24]);
                        pluBundle.Plu.ShelfLifeDays = Convert.ToUInt16(item[25]);
                        pluBundle.Plu.Gtin = Convert.ToString(item[26]);
                        pluBundle.Plu.Ean13 = Convert.ToString(item[27]);
                        pluBundle.Plu.Itf14 = Convert.ToString(item[28]);
                        pluBundle.Plu.IsCheckWeight = Convert.ToBoolean(item[29]);
                    }
                    if (Guid.TryParse(Convert.ToString(item[31]), out Guid bundleUid))
                    {
                        pluBundle.Bundle.IdentityValueUid = bundleUid;
                        pluBundle.Bundle.CreateDt = Convert.ToDateTime(item[32]);
                        pluBundle.Bundle.ChangeDt = Convert.ToDateTime(item[33]);
                        pluBundle.Bundle.IsMarked = Convert.ToBoolean(item[34]);
                        pluBundle.Bundle.Name = Convert.ToString(item[35]);
                        pluBundle.Bundle.Weight = Convert.ToDecimal(item[36]);
                    }
                    BoxModel box = new();
                    if (Guid.TryParse(Convert.ToString(item[37]), out Guid boxUid))
                    {
                        box.IdentityValueUid = boxUid;
                        box.CreateDt = Convert.ToDateTime(item[38]);
                        box.ChangeDt = Convert.ToDateTime(item[39]);
                        box.IsMarked = Convert.ToBoolean(item[40]);
                        box.Name = Convert.ToString(item[41]);
                        box.Weight = Convert.ToDecimal(item[42]);
                    }
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
                        Box = box,
                    });
                }
            }
        }
        return PluNestingFks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePrinters<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Printers = DataAccess.GetListNotNullable<PrinterModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Printers.Count > 1)
            Printers = Printers.OrderBy(item => item.Name).ToList();
        return Printers.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePrinterResources<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PrinterResources = DataAccess.GetListNotNullable<PrinterResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PrinterResources.Count > 1)
            PrinterResources = PrinterResources
                .OrderBy(item => item.Printer.Name).ToList()
                .OrderBy(item => item.TemplateResource.Name).ToList();
        return PrinterResources.Cast<T>().ToList();
    }

    private List<T> GetListNotNullablePrinterTypes<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        PrinterTypes = DataAccess.GetListNotNullable<PrinterTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && PrinterTypes.Count > 1)
            PrinterTypes = PrinterTypes.OrderBy(item => item.Name).ToList();
        return PrinterTypes.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableProductionFacilities<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ProductionFacilities = DataAccess.GetListNotNullable<ProductionFacilityModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && ProductionFacilities.Count > 1)
            ProductionFacilities = ProductionFacilities.OrderBy(item => item.Name).ToList();
        return ProductionFacilities.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableProductSeries<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ProductSeries = DataAccess.GetListNotNullable<ProductSeriesModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && ProductSeries.Count > 1)
            ProductSeries = ProductSeries.OrderByDescending(item => item.ChangeDt).ToList();
        return ProductSeries.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableScales<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Scales = DataAccess.GetListNotNullable<ScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Scales.Count > 1)
            Scales = Scales.OrderBy(item => item.Name).ToList();
        return Scales.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableScaleScreenShots<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ScaleScreenShots = DataAccess.GetListNotNullable<ScaleScreenShotModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && ScaleScreenShots.Count > 1)
            ScaleScreenShots = ScaleScreenShots.OrderByDescending(item => item.ChangeDt).ToList();
        return ScaleScreenShots.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableTasks<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Tasks = DataAccess.GetListNotNullable<TaskModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Tasks.Count > 1)
            Tasks = Tasks.OrderBy(item => item.Name).ToList();
        return Tasks.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableTaskTypes<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        TaskTypes = DataAccess.GetListNotNullable<TaskTypeModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && TaskTypes.Count > 1)
            TaskTypes = TaskTypes.OrderBy(item => item.Name).ToList();
        return TaskTypes.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableTemplates<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Templates = DataAccess.GetListNotNullable<TemplateModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Templates.Count > 1)
            Templates = Templates.OrderBy(item => item.Name).ToList();
        return Templates.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableTemplateResources<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        TemplateResources = DataAccess.GetListNotNullable<TemplateResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && TemplateResources.Count > 1)
            TemplateResources = TemplateResources.OrderBy(item => item.Name).ToList();
        return TemplateResources.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableVersions<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        Versions = DataAccess.GetListNotNullable<VersionModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && Versions.Count > 1)
            Versions = Versions.OrderByDescending(item => item.Version).ToList();
        return Versions.Cast<T>().ToList();
    }

    private List<T> GetListNotNullableWorkShops<T>(SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        WorkShops = DataAccess.GetListNotNullable<WorkShopModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && WorkShops.Count > 1)
            WorkShops = WorkShops.OrderBy(item => item.Name).ToList();
        return WorkShops.Cast<T>().ToList();
    }

    #endregion

    #region Public and private methods

    public T? GetItemNullable<T>(object? value) where T : class, new() => DataAccess.GetItemNullable<T>(value);

    public T GetItemNotNullable<T>(object? value) where T : class, new() => DataAccess.GetItemNotNullable<T>(value);

    /// <summary>
    /// List of models SqlTableBase.
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
        new NomenclatureGroupModel(),
        new NomenclatureModel(),
        new NomenclaturesCharacteristicsFkModel(),
        new NomenclaturesCharacteristicsModel(),
        new NomenclaturesGroupFkModel(),
        new NomenclatureV2Model(),
        new OrderModel(),
        new OrderWeighingModel(),
        new OrganizationModel(),
        new PluBundleFkModel(),
        new PluLabelModel(),
        new PluModel(),
        new PluScaleModel(),
        new PluTemplateFkModel(),
        new PluWeighingModel(),
        new PrinterModel(),
        new PrinterResourceModel(),
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
    /// List of types SqlTableBase.
    /// </summary>
    /// <returns></returns>
    public List<Type> GetTableTypes() => new()
    {
        typeof(BoxModel),
        typeof(BrandModel),
        typeof(ContragentModel),
        typeof(NomenclaturesCharacteristicsFkModel),
        typeof(OrderModel),
        typeof(PluBundleFkModel),
        typeof(AccessModel),
        typeof(AppModel),
        typeof(BarCodeModel),
        typeof(BundleModel),
        typeof(DeviceModel),
        typeof(DeviceScaleFkModel),
        typeof(DeviceTypeFkModel),
        typeof(DeviceTypeModel),
        typeof(LogModel),
        typeof(LogTypeModel),
        typeof(NomenclatureGroupModel),
        typeof(NomenclatureModel),
        typeof(NomenclaturesCharacteristicsModel),
        typeof(NomenclaturesGroupFkModel),
        typeof(NomenclatureV2Model),
        typeof(OrderWeighingModel),
        typeof(OrganizationModel),
        typeof(PluLabelModel),
        typeof(PluModel),
        typeof(PluScaleModel),
        typeof(PluTemplateFkModel),
        typeof(PluWeighingModel),
        typeof(PrinterModel),
        typeof(PrinterResourceModel),
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

    public string GetTableModelName<T>() where T : class, new()
    {
        return typeof(T) switch
        {
            var cls when cls == typeof(BoxModel) => nameof(BoxModel),
            var cls when cls == typeof(BrandModel) => nameof(BrandModel),
            var cls when cls == typeof(BundleModel) => nameof(BundleModel),
            var cls when cls == typeof(ClipModel) => nameof(ClipModel),
            var cls when cls == typeof(ContragentModel) => nameof(ContragentModel),
            var cls when cls == typeof(NomenclaturesCharacteristicsFkModel) => nameof(NomenclaturesCharacteristicsFkModel),
            var cls when cls == typeof(OrderModel) => nameof(OrderModel),
            var cls when cls == typeof(PluBundleFkModel) => nameof(PluBundleFkModel),
            var cls when cls == typeof(AccessModel) => nameof(AccessModel),
            var cls when cls == typeof(AppModel) => nameof(AppModel),
            var cls when cls == typeof(BarCodeModel) => nameof(BarCodeModel),
            var cls when cls == typeof(DeviceModel) => nameof(DeviceModel),
            var cls when cls == typeof(DeviceScaleFkModel) => nameof(DeviceScaleFkModel),
            var cls when cls == typeof(DeviceTypeFkModel) => nameof(DeviceTypeFkModel),
            var cls when cls == typeof(DeviceTypeModel) => nameof(DeviceTypeModel),
            var cls when cls == typeof(LogModel) => nameof(LogModel),
            var cls when cls == typeof(LogTypeModel) => nameof(LogTypeModel),
            var cls when cls == typeof(NomenclatureGroupModel) => nameof(NomenclatureGroupModel),
            var cls when cls == typeof(NomenclatureModel) => nameof(NomenclatureModel),
            var cls when cls == typeof(NomenclaturesCharacteristicsModel) => nameof(NomenclaturesCharacteristicsModel),
            var cls when cls == typeof(NomenclaturesGroupFkModel) => nameof(NomenclaturesGroupFkModel),
            var cls when cls == typeof(NomenclatureV2Model) => nameof(NomenclatureV2Model),
            var cls when cls == typeof(OrderWeighingModel) => nameof(OrderWeighingModel),
            var cls when cls == typeof(OrganizationModel) => nameof(OrganizationModel),
            var cls when cls == typeof(PluLabelModel) => nameof(PluLabelModel),
            var cls when cls == typeof(PluModel) => nameof(PluModel),
            var cls when cls == typeof(PluScaleModel) => nameof(PluScaleModel),
            var cls when cls == typeof(PluTemplateFkModel) => nameof(PluTemplateFkModel),
            var cls when cls == typeof(PluWeighingModel) => nameof(PluWeighingModel),
            var cls when cls == typeof(PrinterModel) => nameof(PrinterModel),
            var cls when cls == typeof(PrinterResourceModel) => nameof(PrinterResourceModel),
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