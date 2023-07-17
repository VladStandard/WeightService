// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.TasksTypes;

[TestFixture]
public sealed class TaskTypeValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlTaskTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTaskTypeModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlTaskTypeModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlTaskTypeModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}