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
        WsTestsUtils.DataTests.AssertGetList<WsSqlAccessModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_AppModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlAppModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BarCodeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<BarCodeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BoxModel()
    {
        WsTestsUtils.DataTests.AssertGetList<BoxModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BrandModel()
    {
        WsTestsUtils.DataTests.AssertGetList<BrandModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BundleModel()
    {
        WsTestsUtils.DataTests.AssertGetList<BundleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ClipModel()
    {
        WsTestsUtils.DataTests.AssertGetList<ClipModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ContragentModel()
    {
        WsTestsUtils.DataTests.AssertGetList<ContragentModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceModel()
    {
        WsTestsUtils.DataTests.AssertGetList<DeviceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceScaleFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlDeviceScaleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<DeviceTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlDeviceTypeFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<LogTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogModel()
    {
        WsTestsUtils.DataTests.AssertGetList<LogModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogMemoryModel()
    {
        WsTestsUtils.DataTests.AssertGetList<LogMemoryModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebModel()
    {
        WsTestsUtils.DataTests.AssertGetList<LogWebModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<LogWebFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_OrderModel()
    {
        WsTestsUtils.DataTests.AssertGetList<OrderModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrderWeighingModel()
    {
        WsTestsUtils.DataTests.AssertGetList<OrderWeighingModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrganizationModel()
    {
        WsTestsUtils.DataTests.AssertGetList<OrganizationModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluBrandFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluBrandFkModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluBundleFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluBundleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicModel()
    {
        WsTestsUtils.DataTests.AssertGetList<PluCharacteristicModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicsFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluCharacteristicsFkModel>(SqlCrudConfig, ConfigurationsDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluClipFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluClipFkModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluFkModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupModel()
    {
        WsTestsUtils.DataTests.AssertGetList<PluGroupModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluGroupFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluLabelModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluLabelModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluNestingFkModel()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
        sqlCrudConfig.NativeQuery = WsSqlQueriesScales.Tables.PluNestingFks.GetList(true);
        sqlCrudConfig.NativeParameters = new() { new("P_UID", new Guid("5B24E604-C550-43C9-91DD-74989A5E9D6C")), };
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluNestingFkModel>(sqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluScaleModel()
    {
        WsTestsUtils.DataTests.AssertGetList<PluScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodModel()
    {
        WsTestsUtils.DataTests.AssertGetList<PluStorageMethodModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluStorageMethodFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluTemplateFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluTemplateFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluWeighingModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluWeighingModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductionFacilityModel()
    {
        WsTestsUtils.DataTests.AssertGetList<ProductionFacilityModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductSeriesModel()
    {
        WsTestsUtils.DataTests.AssertGetList<ProductSeriesModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleModel()
    {
        WsTestsUtils.DataTests.AssertGetList<ScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleScreenShotModel()
    {
        WsTestsUtils.DataTests.AssertGetList<ScaleScreenShotModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskModel()
    {
        WsTestsUtils.DataTests.AssertGetList<TaskModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<TaskTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateModel()
    {
        WsTestsUtils.DataTests.AssertGetList<TemplateModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateResourceModel()
    {
        WsTestsUtils.DataTests.AssertGetList<TemplateResourceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_WorkShopModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WorkShopModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterModel()
    {
        WsTestsUtils.DataTests.AssertGetList<PrinterModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterResourceFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPrinterResourceFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<PrinterTypeModel>(SqlCrudConfig, Configurations);
    }
}