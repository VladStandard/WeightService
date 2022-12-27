// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.TasksTypes;

namespace DataCoreTests.Sql.TableScaleModels.TasksTypes;

[TestFixture]
internal class TaskTypeValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        TaskTypeModel item = DataCore.CreateNewSubstitute<TaskTypeModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TaskTypeModel item = DataCore.CreateNewSubstitute<TaskTypeModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
