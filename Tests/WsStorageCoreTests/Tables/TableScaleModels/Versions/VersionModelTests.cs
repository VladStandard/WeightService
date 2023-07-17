// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlVersionModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlVersionModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlVersionModel>(nameof(WsSqlVersionModel.ReleaseDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<WsSqlVersionModel>(nameof(WsSqlTableBase.Description));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<WsSqlVersionModel>(nameof(WsSqlVersionModel.Version));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlVersionModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlVersionModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlVersionModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlVersionModel>();
    }
}