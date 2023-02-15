// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.PrintersTypes;

[TestFixture]
internal class PrinterTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterTypeModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterTypeModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}