// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Logs;

namespace DataCoreTests.Sql.TableScaleModels.Logs;

[TestFixture]
internal class LogModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<LogModel>(nameof(LogModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<LogModel>(nameof(LogModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<LogModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Table_ToString()
    {
        DataCore.TableBaseModelAssertToString<LogModel>();
    }

    [Test]
    public void TableEqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<LogModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<LogModel>();
    }
}