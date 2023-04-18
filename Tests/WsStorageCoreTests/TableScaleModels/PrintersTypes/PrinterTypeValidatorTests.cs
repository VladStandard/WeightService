// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.PrintersTypes;

namespace WsStorageCoreTests.TableScaleModels.PrintersTypes;

[TestFixture]
public sealed class PrinterTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterTypeModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PrinterTypeModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterTypeModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PrinterTypeModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}