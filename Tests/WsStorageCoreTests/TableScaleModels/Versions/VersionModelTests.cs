// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Versions;

namespace WsStorageCoreTests.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(WsSqlTableBase.Description));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckBool<VersionModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertEqualsNew<VersionModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertSerialize<VersionModel>();
    }
}