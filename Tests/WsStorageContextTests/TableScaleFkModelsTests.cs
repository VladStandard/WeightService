// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests;

[TestFixture]
public sealed class TableScaleFkModelsTests
{
    [Test]
    public void Validate_list_of_device_scale_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<DeviceScaleFkModel>();
    }

    [Test]
    public void Validate_list_of_device_type_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<DeviceTypeFkModel>();
    }

    [Test]
    public void Validate_list_of_log_web_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<LogWebFkModel>(true);
    }

    [Test]
    public void Validate_list_of_plu_brand_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluBrandFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_bundle_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluBundleFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_characteristics_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluCharacteristicsFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_clip_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluClipFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_group_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluGroupFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_nesting_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluNestingFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_storage_method_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluStorageMethodFkModel>(true);
    }

    [Test]
    public void Validate_list_of_plu_template_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluTemplateFkModel>();
    }

    [Test]
    public void Validate_list_of_printers_resources_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<PrinterResourceFkModel>();
    }
}