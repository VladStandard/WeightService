// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableDiagModels.LogsWebs;
using WsStorage.Tables;

namespace WsStorageCoreTests.TableDiagModels.LogsWebs;

[TestFixture]
internal class LogWebModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<LogWebModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<LogWebModel>(nameof(WsSqlTableBase.ChangeDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckBool<LogWebModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertToString<LogWebModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertEqualsNew<LogWebModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertSerialize<LogWebModel>();
    }
}