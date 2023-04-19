// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.TasksTypes;

namespace WsStorageCoreTests.TableScaleModels.TasksTypes;

[TestFixture]
public sealed class TaskTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TaskTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<TaskTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TaskTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<TaskTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}