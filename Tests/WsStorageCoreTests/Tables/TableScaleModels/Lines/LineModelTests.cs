// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineModelTests : TableModelTests<WsSqlScaleModel>
{
    [Test]
    public override void Model_AssertSqlFields_Check()
    {
        base.Model_AssertSqlFields_Check();
        //WsTestsUtils.DataCore.AssertSqlFieldStringCheck<ScaleModel>(nameof(ScaleModel.Host.Name));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckString<WsSqlScaleModel>(nameof(WsSqlTableBase.Description));
    }
}