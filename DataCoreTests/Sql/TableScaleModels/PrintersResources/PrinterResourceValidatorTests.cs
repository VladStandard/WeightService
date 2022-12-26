// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersResources;

namespace DataCoreTests.Sql.TableScaleModels.PrintersResources;

[TestFixture]
internal class PrinterResourceValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterResourceModel item = DataCore.CreateNewSubstitute<PrinterResourceModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterResourceModel item = DataCore.CreateNewSubstitute<PrinterResourceModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
