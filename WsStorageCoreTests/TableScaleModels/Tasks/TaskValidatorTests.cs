// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Tasks;

[TestFixture]
internal class TaskValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TaskModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TaskModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TaskModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<TaskModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}