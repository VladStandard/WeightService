using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionModelTests : TableModelTests<WsSqlVersionModel>
{
    [Test]
    public override void Model_AssertSqlFields_Check()
    {
        base.Model_AssertSqlFields_Check();
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlVersionModel>(nameof(WsSqlVersionModel.ReleaseDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<WsSqlVersionModel>(nameof(WsSqlTableBase.Description));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<WsSqlVersionModel>(nameof(WsSqlVersionModel.Version));
    }
}