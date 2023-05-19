// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests;

[TestFixture]
public sealed class TableScaleFkModelsTests
{
    [Test]
    public void Validate_list_of_device_scale_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlDeviceScaleFkModel>();
    }

    [Test]
    public void Validate_list_of_device_type_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlDeviceTypeFkModel>();
    }

    [Test]
    public void Validate_list_of_log_web_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlLogWebFkModel>(WsSqlIsMarked.ShowAll);
    }

    [Test]
    public void Validate_list_of_plu_brand_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluBrandFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_bundle_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluBundleFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_characteristics_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluCharacteristicsFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_clip_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluClipFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_group_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluGroupFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_nesting_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluNestingFkModel>();
    }

    [Test]
    public void Validate_list_of_plu_storage_method_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluStorageMethodFkModel>(WsSqlIsMarked.ShowAll);
    }

    [Test]
    public void Validate_list_of_plu_template_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPluTemplateFkModel>();
    }

    [Test]
    public void Validate_list_of_printers_resources_fk()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPrinterResourceFkModel>();
    }
}