// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Tasks;

namespace DataCoreTests.Sql.TableScaleModels.Tasks;

[TestFixture]
internal class TaskValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        TaskModel item = DataCore.CreateNewSubstitute<TaskModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TaskModel item = DataCore.CreateNewSubstitute<TaskModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
