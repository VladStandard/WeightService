// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Versions;

namespace DataCoreTests.Sql.TableScaleModels.Versions;

[TestFixture]
internal class VersionValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        VersionModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<VersionModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        VersionModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<VersionModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}