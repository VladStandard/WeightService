// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersResources;

namespace DataCoreTests.Sql.TableScaleModels.PrintersResources;

[TestFixture]
internal class PrinterResourceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterResourceModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterResourceModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterResourceModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<PrinterResourceModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}