// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class GetListTests
{
    private static SqlCrudConfigModel SqlCrudConfig => new(false, true, false, true, false);
    private static SqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true, false);
    private static List<WsConfiguration> Configurations => new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS };
    private static readonly List<WsConfiguration> ConfigurationsDev = new() { WsConfiguration.DevelopVS };

    [Test]
    public void DataContext_AssertGetList_AccessModel()
    {
        WsTestsUtils.DataCore.AssertGetList<AccessModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_AppModel()
    {
        WsTestsUtils.DataCore.AssertGetList<AppModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BarCodeModel()
    {
        WsTestsUtils.DataCore.AssertGetList<BarCodeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BoxModel()
    {
        WsTestsUtils.DataCore.AssertGetList<BoxModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BrandModel()
    {
        WsTestsUtils.DataCore.AssertGetList<BrandModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BundleModel()
    {
        WsTestsUtils.DataCore.AssertGetList<BundleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ClipModel()
    {
        WsTestsUtils.DataCore.AssertGetList<ClipModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ContragentModel()
    {
        WsTestsUtils.DataCore.AssertGetList<ContragentModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceModel()
    {
        WsTestsUtils.DataCore.AssertGetList<DeviceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceScaleFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<DeviceScaleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeModel()
    {
        WsTestsUtils.DataCore.AssertGetList<DeviceTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<DeviceTypeFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogTypeModel()
    {
        WsTestsUtils.DataCore.AssertGetList<LogTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogModel()
    {
        WsTestsUtils.DataCore.AssertGetList<LogModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogMemoryModel()
    {
        WsTestsUtils.DataCore.AssertGetList<LogMemoryModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebModel()
    {
        WsTestsUtils.DataCore.AssertGetList<LogWebModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<LogWebFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_OrderModel()
    {
        WsTestsUtils.DataCore.AssertGetList<OrderModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrderWeighingModel()
    {
        WsTestsUtils.DataCore.AssertGetList<OrderWeighingModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrganizationModel()
    {
        WsTestsUtils.DataCore.AssertGetList<OrganizationModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluBrandFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluBrandFkModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluBundleFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluBundleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluCharacteristicModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicsFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluCharacteristicsFkModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluClipFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluClipFkModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluFkModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluGroupModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluGroupFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluLabelModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluLabelModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluNestingFkModel()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
        sqlCrudConfig.NativeQuery = WsSqlQueriesScales.Tables.PluNestingFks.GetList(true);
        sqlCrudConfig.NativeParameters = new() { new("P_UID", new Guid("5B24E604-C550-43C9-91DD-74989A5E9D6C")), };
        WsTestsUtils.DataCore.AssertGetList<PluNestingFkModel>(sqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluScaleModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluStorageMethodModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluStorageMethodFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluTemplateFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluTemplateFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluWeighingModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PluWeighingModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductionFacilityModel()
    {
        WsTestsUtils.DataCore.AssertGetList<ProductionFacilityModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductSeriesModel()
    {
        WsTestsUtils.DataCore.AssertGetList<ProductSeriesModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleModel()
    {
        WsTestsUtils.DataCore.AssertGetList<ScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleScreenShotModel()
    {
        WsTestsUtils.DataCore.AssertGetList<ScaleScreenShotModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskModel()
    {
        WsTestsUtils.DataCore.AssertGetList<TaskModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskTypeModel()
    {
        WsTestsUtils.DataCore.AssertGetList<TaskTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateModel()
    {
        WsTestsUtils.DataCore.AssertGetList<TemplateModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateResourceModel()
    {
        WsTestsUtils.DataCore.AssertGetList<TemplateResourceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_WorkShopModel()
    {
        WsTestsUtils.DataCore.AssertGetList<WorkShopModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PrinterModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterResourceFkModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PrinterResourceFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterTypeModel()
    {
        WsTestsUtils.DataCore.AssertGetList<PrinterTypeModel>(SqlCrudConfig, Configurations);
    }
}