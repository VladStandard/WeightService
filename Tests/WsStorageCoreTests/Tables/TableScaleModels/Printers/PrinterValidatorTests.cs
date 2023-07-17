// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Printers;

[TestFixture]
public sealed class PrinterValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlPrinterModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPrinterModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlPrinterModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlPrinterModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}