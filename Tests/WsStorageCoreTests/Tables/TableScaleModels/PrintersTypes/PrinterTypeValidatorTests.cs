// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.PrintersTypes;

[TestFixture]
public sealed class PrinterTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPrinterTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPrinterTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPrinterTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPrinterTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}