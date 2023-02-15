// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.TasksTypes;

[TestFixture]
internal class TaskTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TaskTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TaskTypeModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TaskTypeModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TaskTypeModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}