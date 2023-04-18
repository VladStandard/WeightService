// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Printers;

namespace WsStorageCoreTests.TableScaleModels.Printers;

[TestFixture]
public sealed class PrinterValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PrinterModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PrinterModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}