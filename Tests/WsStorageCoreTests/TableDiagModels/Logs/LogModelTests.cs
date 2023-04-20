// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableDiagModels.Logs;

[TestFixture]
public sealed class LogModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<LogModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<LogModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<LogModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Table_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<LogModel>();
    }

    [Test]
    public void TableEqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<LogModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<LogModel>();
    }
}