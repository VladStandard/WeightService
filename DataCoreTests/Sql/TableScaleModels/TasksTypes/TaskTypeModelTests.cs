// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.TasksTypes;

namespace DataCoreTests.Sql.TableScaleModels.TasksTypes;

[TestFixture]
internal class TaskTypeModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(TaskTypeModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(TaskTypeModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<TaskTypeModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<TaskTypeModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<TaskTypeModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<TaskTypeModel>();
    }
}