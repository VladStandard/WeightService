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
        PrinterResourceFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PrinterResourceFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterResourceFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PrinterResourceFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}