// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;

namespace WsStorageCoreTests.TableScaleFkModels.PrinterResourceFks;

[TestFixture]
public sealed class PrinterResourceFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterResourceFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PrinterResourceFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterResourceFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<PrinterResourceFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}