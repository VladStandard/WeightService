// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Printers;

[TestFixture]
public sealed class PrinterValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PrinterModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PrinterModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}