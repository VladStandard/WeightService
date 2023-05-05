// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusNestingFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkModelTests
{ 
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlPluNestingFkModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlPluNestingFkModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlPluNestingFkModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlPluNestingFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlPluNestingFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlPluNestingFkModel>();
    }
}