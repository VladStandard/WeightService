// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.DataContext;

[TestFixture]
public sealed class GetListTests
{
    private static WsSqlCrudConfigModel SqlCrudConfig => new(false, true, false, true, false);
    private static WsSqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true, false);
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
        WsTestsUtils.DataTests.AssertGetList<WsSqlBarCodeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BoxModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlBoxModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BrandModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlBrandModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_BundleModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlBundleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ClipModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlClipModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ContragentModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlContragentModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlDeviceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceScaleFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlDeviceScaleFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlDeviceTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlDeviceTypeFkModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlLogTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlLogModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogMemoryModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlLogMemoryModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlLogWebModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlLogWebFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_OrderModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlOrderModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrderWeighingModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlOrderWeighingModel>(SqlCrudConfig, Configurations, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrganizationModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlOrganizationModel>(SqlCrudConfig, Configurations);
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
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluCharacteristicModel>(SqlCrudConfig, ConfigurationsDev);
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
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluGroupModel>(SqlCrudConfig, Configurations);
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
        WsSqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
        sqlCrudConfig.NativeQuery = WsSqlQueriesScales.Tables.PluNestingFks.GetList(true);
        sqlCrudConfig.NativeParameters = new() { new("P_UID", new Guid("5B24E604-C550-43C9-91DD-74989A5E9D6C")), };
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluNestingFkModel>(sqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluScaleModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPluStorageMethodModel>(SqlCrudConfig, Configurations);
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
        WsTestsUtils.DataTests.AssertGetList<WsSqlProductionFacilityModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ProductSeriesModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlProductSeriesModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlScaleModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleScreenShotModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlScaleScreenShotModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlTaskModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TaskTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlTaskTypeModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlTemplateModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateResourceModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlTemplateResourceModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_WorkShopModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlWorkShopModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPrinterModel>(SqlCrudConfig, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterResourceFkModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPrinterResourceFkModel>(SqlCrudConfigFk, Configurations);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterTypeModel()
    {
        WsTestsUtils.DataTests.AssertGetList<WsSqlPrinterTypeModel>(SqlCrudConfig, Configurations);
    }
}