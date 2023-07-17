// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusGroupFks;

[TestFixture]
public sealed class PluGroupFkModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlPluGroupFkModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlPluGroupFkModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlPluGroupFkModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlPluGroupFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlPluGroupFkModel>();
    }
    
    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlPluGroupFkModel>();
    }
}