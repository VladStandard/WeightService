// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableDiagModels.Logs;

[TestFixture]
public sealed class LogModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlLogModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlLogModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlLogModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Table_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlLogModel>();
    }

    [Test]
    public void TableEqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlLogModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlLogModel>();
    }
}