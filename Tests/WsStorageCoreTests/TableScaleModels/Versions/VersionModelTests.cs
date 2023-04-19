// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<VersionModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<VersionModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<VersionModel>(nameof(WsSqlTableBase.Description));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<VersionModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<VersionModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<VersionModel>();
    }
}