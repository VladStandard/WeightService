// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PrintersTypes;

[TestFixture]
public sealed class PrinterTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PrinterTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PrinterTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}