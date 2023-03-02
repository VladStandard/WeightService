// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;

namespace WsStorageCoreTests.TableScaleFkModels.PrinterResourceFks;

[TestFixture]
internal class PrinterResourceFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterResourceFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterResourceFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterResourceFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterResourceFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}