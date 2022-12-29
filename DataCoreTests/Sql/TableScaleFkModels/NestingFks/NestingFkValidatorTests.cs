// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NestingFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NestingFks;

[TestFixture]
internal class NestingFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        NestingFkModel item = DataCore.CreateNewSubstitute<NestingFkModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        NestingFkModel item = DataCore.CreateNewSubstitute<NestingFkModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
