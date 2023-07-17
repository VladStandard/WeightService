// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluStorageMethods;

[TestFixture]
public sealed class PluStorageMethodModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlPluStorageMethodModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlPluStorageMethodModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlPluStorageMethodModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlPluStorageMethodModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlPluStorageMethodModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlPluStorageMethodModel>();
    }
}