// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Printers;

namespace DataCoreTests.Sql.TableScaleModels.Printers;

[TestFixture]
internal class PrinterValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PrinterModel item = DataCore.CreateNewSubstitute<PrinterModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PrinterModel item = DataCore.CreateNewSubstitute<PrinterModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
