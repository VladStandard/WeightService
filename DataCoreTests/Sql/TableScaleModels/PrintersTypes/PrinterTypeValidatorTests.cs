// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersTypes;

namespace DataCoreTests.Sql.TableScaleModels.PrintersTypes;

[TestFixture]
internal class PrinterTypeValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterTypeModel item = DataCore.CreateNewSubstitute<PrinterTypeModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterTypeModel item = DataCore.CreateNewSubstitute<PrinterTypeModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
