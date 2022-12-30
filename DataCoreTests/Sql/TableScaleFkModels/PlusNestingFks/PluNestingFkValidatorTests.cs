// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusNestingFks;

namespace DataCoreTests.Sql.TableScaleFkModels.PlusNestingFks;

[TestFixture]
internal class PluNestingFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        PluNestingFkModel item = DataCore.CreateNewSubstitute<PluNestingFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluNestingFkModel item = DataCore.CreateNewSubstitute<PluNestingFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}