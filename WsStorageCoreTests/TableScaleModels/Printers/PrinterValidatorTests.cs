// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Printers;

[TestFixture]
internal class PrinterValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}