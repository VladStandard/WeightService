// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Tasks;

namespace WsStorageCoreTests.TableScaleModels.Tasks;

[TestFixture]
public sealed class TaskValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        TaskModel item = WsTestsUtils.DataCore.CreateNewSubstitute<TaskModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        TaskModel item = WsTestsUtils.DataCore.CreateNewSubstitute<TaskModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}