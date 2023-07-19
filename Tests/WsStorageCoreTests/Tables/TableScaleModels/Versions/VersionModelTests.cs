// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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