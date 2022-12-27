// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Apps;

namespace DataCoreTests.Sql.TableScaleModels.Apps;

[TestFixture]
internal class AppValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        AppModel item = DataCore.CreateNewSubstitute<AppModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        AppModel item = DataCore.CreateNewSubstitute<AppModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}