// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Versions;

namespace WsStorageContextTests;

[TestFixture]
public sealed class TableScaleModelsTests
{
    [Test]
    public void Validate_list_of_accesses()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlAccessModel>();
    }

    [Test]
    public void Validate_list_of_apps()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlAppModel>();
    }

    [Test]
    public void Validate_list_of_bar_codes()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlBarCodeModel>();
    }

    [Test]
    public void Validate_list_of_boxes()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlBoxModel>();
    }

    [Test]
    public void Validate_list_of_brands()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlBrandModel>();
    }

    [Test]
    public void Validate_list_of_bundles()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlBundleModel>();
    }

    [Test]
    public void Validate_list_of_clips()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlClipModel>();
    }

    [Test]
    public void Validate_list_of_contragents()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlContragentModel>();
    }

    [Test]
    public void Validate_list_of_devices()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlDeviceModel>();
    }

    [Test]
    public void Validate_list_of_devices_types()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlDeviceTypeModel>();
    }

    [Test]
    public void Validate_list_of_logs()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlLogModel>();
    }

    [Test]
    public void Validate_list_of_logs_types()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlLogTypeModel>();
    }

    [Test]
    public void Validate_list_of_logs_webs()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlLogWebModel>();
    }

    //[Test]
    //public void Validate_list_of_orders()
    //{
    //    WsTestsUtils.DataTests.AssertSqlDbContentValidate<OrderModel>();
    //}

    //[Test]
    //public void Validate_list_of_orders_weighing()
    //{
    //    WsTestsUtils.DataTests.AssertSqlDbContentValidate<OrderWeighingModel>();
    //}

    [Test]
    public void Validate_list_of_organizations()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlOrganizationModel>();
    }

    [Test]
    public void Validate_list_of_plus_characteristics()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluCharacteristicModel>();
    }

    [Test]
    public void Validate_list_of_plus()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluModel>();
    }

    [Test]
    public void Validate_list_of_plus_groups()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluGroupModel>();
    }

    [Test]
    public void Validate_list_of_plus_labels()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluLabelModel>();
    }

    [Test]
    public void Validate_list_of_plus_scales()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluScaleModel>();
    }

    [Test]
    public void Validate_list_of_plus_storage_methods()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluStorageMethodModel>();
    }

    [Test]
    public void Validate_list_of_plus_Weighing()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluWeighingModel>();
    }

    [Test]
    public void Validate_list_of_printers()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPrinterModel>();
    }

    [Test]
    public void Validate_list_of_printers_types()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPrinterTypeModel>();
    }

    [Test]
    public void Validate_list_of_production_facilities()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlProductionFacilityModel>();
    }

    [Test]
    public void Validate_list_of_product_series()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlProductSeriesModel>();
    }

    [Test]
    public void Validate_list_of_scales()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlScaleModel>();
    }

    [Test]
    public void Validate_list_of_tasks()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlTaskModel>();
    }

    [Test]
    public void Validate_list_of_tasks_types()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlTaskTypeModel>();
    }

    [Test]
    public void Validate_list_of_templates()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlTemplateModel>();
    }

    [Test]
    public void Validate_list_of_templates_resources()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlTemplateResourceModel>();
    }

    [Test]
    public void Validate_list_of_versions()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlVersionModel>();
    }

    [Test]
    public void Validate_list_of_works_shops()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlWorkShopModel>();
    }
}