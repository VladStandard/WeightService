// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorage.Models;
using WsStorage.TableDiagModels.Logs;
using WsStorage.TableDiagModels.LogsMemories;
using WsStorage.TableDiagModels.LogsTypes;
using WsStorage.TableDiagModels.LogsWebs;
using WsStorage.TableDiagModels.LogsWebsFks;
using WsStorage.TableDiagModels.ScalesScreenshots;
using WsStorage.TableScaleFkModels.DeviceScalesFks;
using WsStorage.TableScaleFkModels.DeviceTypesFks;
using WsStorage.TableScaleFkModels.PlusBrandsFks;
using WsStorage.TableScaleFkModels.PlusBundlesFks;
using WsStorage.TableScaleFkModels.PlusCharacteristicsFks;
using WsStorage.TableScaleFkModels.PlusClipsFks;
using WsStorage.TableScaleFkModels.PlusFks;
using WsStorage.TableScaleFkModels.PlusGroupsFks;
using WsStorage.TableScaleFkModels.PlusLabels;
using WsStorage.TableScaleFkModels.PlusNestingFks;
using WsStorage.TableScaleFkModels.PlusStorageMethodsFks;
using WsStorage.TableScaleFkModels.PlusTemplatesFks;
using WsStorage.TableScaleFkModels.PlusWeighingsFks;
using WsStorage.TableScaleFkModels.PrintersResourcesFks;
using WsStorage.TableScaleModels.Access;
using WsStorage.TableScaleModels.Apps;
using WsStorage.TableScaleModels.BarCodes;
using WsStorage.TableScaleModels.Boxes;
using WsStorage.TableScaleModels.Brands;
using WsStorage.TableScaleModels.Bundles;
using WsStorage.TableScaleModels.Clips;
using WsStorage.TableScaleModels.Contragents;
using WsStorage.TableScaleModels.Devices;
using WsStorage.TableScaleModels.DeviceTypes;
using WsStorage.TableScaleModels.Orders;
using WsStorage.TableScaleModels.OrdersWeighings;
using WsStorage.TableScaleModels.Organizations;
using WsStorage.TableScaleModels.Plus;
using WsStorage.TableScaleModels.PlusCharacteristics;
using WsStorage.TableScaleModels.PlusGroups;
using WsStorage.TableScaleModels.PlusScales;
using WsStorage.TableScaleModels.PlusStorageMethods;
using WsStorage.TableScaleModels.Printers;
using WsStorage.TableScaleModels.PrintersTypes;
using WsStorage.TableScaleModels.ProductionFacilities;
using WsStorage.TableScaleModels.ProductSeries;
using WsStorage.TableScaleModels.Scales;
using WsStorage.TableScaleModels.Tasks;
using WsStorage.TableScaleModels.TasksTypes;
using WsStorage.TableScaleModels.Templates;
using WsStorage.TableScaleModels.TemplatesResources;
using WsStorage.TableScaleModels.WorkShops;
using WsStorage.Utils;

namespace WsStorageContextTests.DataContext;

[TestFixture]
internal class GetListTests
{
    private static SqlCrudConfigModel SqlCrudConfig => new(false, true, false, true, false);
    private static SqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true, false);
    private static List<WsConfiguration> Configurations => new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS };
    private static readonly List<WsConfiguration> ConfigurationsDev = new() { WsConfiguration.DevelopVS };

    [Test]
    public void DataContext_AssertGetList_AccessModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<AccessModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_AppModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<AppModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BarCodeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BarCodeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BoxModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BoxModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BrandModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BrandModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BundleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BundleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ClipModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ClipModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ContragentModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ContragentModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceScaleFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceScaleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceTypeFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogMemoryModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogMemoryModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogWebModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogWebFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_OrderModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrderModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrderWeighingModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrderWeighingModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrganizationModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrganizationModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluBrandFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluBrandFkModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluBundleFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluBundleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluCharacteristicModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicsFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluCharacteristicsFkModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluClipFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluClipFkModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluFkModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluGroupModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluGroupFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluLabelModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluLabelModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluNestingFkModel()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
        sqlCrudConfig.NativeQuery = WsSqlQueriesScales.Tables.PluNestingFks.GetList(true);
        sqlCrudConfig.NativeParameters = new() { new("P_UID", new Guid("5B24E604-C550-43C9-91DD-74989A5E9D6C")), };
        DataCoreTestsUtils.DataCore.AssertGetList<PluNestingFkModel>(sqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluScaleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluStorageMethodModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluStorageMethodFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluTemplateFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluTemplateFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluWeighingModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluWeighingModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductionFacilityModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ProductionFacilityModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductSeriesModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ProductSeriesModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleScreenShotModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ScaleScreenShotModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TaskModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TaskTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TemplateModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateResourceModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TemplateResourceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_WorkShopModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<WorkShopModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterResourceFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterResourceFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterTypeModel>(SqlCrudConfig, Configurations);
    }
}